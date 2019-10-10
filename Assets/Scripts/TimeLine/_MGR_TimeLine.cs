using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class _MGR_TimeLine : MonoBehaviour {

    private static _MGR_TimeLine p_instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public static _MGR_TimeLine Instance { get { return p_instance; } }     // READ ONLY

    public const float DURE_MAX_PAR_DEFAUT = 180; // temps maximum d'une partie (sans pause) par defaut , pour tests : 10 secondes ! 
    public float dureeMax { get; set; }     // temps maximum d'une partie (sans pause)
    private float p_debutApp;
    public float chrono { get; private set; }       // chrono partie
    public float dureeJeu { get; private set; }       // chrono partie
    public float dureeApp { get { return Time.time - p_debutApp; } }       // temps exécution application 

    public bool ChronoDemarre { get; private set; }
    public bool ChronoEnPause { get; private set; }

    private uint p_nb_TL_Events;
    List<Interface_TL_Events> p_Liste_TL_Events;

    // à faire : 
    //     Methode 1 : stocker tous les évenement dans une timeline d'évements à déclencher ou arrêter  .... 
    //     prétraitement plus difficile   mais ensuite plus rapide ;-)
    //     == construire une liste d'évenements à declencher à partir de l'analyse de p_Liste_TL_Events
    //     == ensuite à chaque frame teste s'il est temps de réaliser une opération sur l'évenement suivant
    // OU
    //     MAethode 2 : 
    //     test à chaque frame des évenements à déclencher ou arrêter ... : plus simple à coder  mais bcp plus lent !! 
    //     == parcourir p_Liste_TL_Events pour savoir si l'evenement doit être déclenché ou arrêté 


    void Awake()
    {        // ===>> Singleton Manager

        //Check if instance already exists
        if (p_instance == null)
            //if not, set instance to this
            p_instance = this;
        //If instance already exists and it's not this:
        else if (p_instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        //Sets this to not be destroyed when reloading scene
        // DontDestroyOnLoad(gameObject);   par nécessaire ici car déja fait par script __DDOL sur l'objet _EGO_app qui recueille tous les mgr


        p_debutApp = Time.time;
        dureeMax = DURE_MAX_PAR_DEFAUT;
        p_Liste_TL_Events = null;
        p_nb_TL_Events = 0;
        ChronoDemarre = false;

        TimeLine = new List<struct_evt_TL>();
    }


    /* à partir des objets passés en paramètres, rescencer tous les scripts représentants des évenements de la TL
       il s'agit des scripts (ou instances de classes) réalisant l'interface Interface_TL_Events , quelque soit la classe
       pour cela le code s'appuie essentiellement sur le méthode GetComponents<Interface_TL_Events>()
    */
    public void Configurer(Event_TL[] __Events_TL)
    {
        // ====> récupérer et vérifier les évenements passés en paramètres
        if (p_Liste_TL_Events != null)
            p_Liste_TL_Events.Clear();   // vider la liste (si 2ème ou suivantes exécutions....
        else
            p_Liste_TL_Events = new List<Interface_TL_Events>();   // 1ere exécution : instantiation
        p_nb_TL_Events = 0;

        foreach (Event_TL _TL_event in __Events_TL)
        {
            // un objet valide ?
            if (_TL_event.GO == null)
            { 
                CommonDevTools.WARNING("objet event  non défini : NULL");
                break;
            }
            // activer GO si inactif ? 
            if (_TL_event.GO.activeSelf == false)
                if (_TL_event.activer_GO_si_inactif)
                    _TL_event.GO.SetActive(true);

            // recherche des scripts évenements  = components impémentant l'interface Interface_TL_Events  
            Interface_TL_Events[] __tabtle = _TL_event.GO.GetComponents<Interface_TL_Events>();
            if (__tabtle.Length==0)
                CommonDevTools.WARNING("object " + _TL_event.GO.name + " pas de TL_events associé: IGNORE !");
            else
                for (int i = 0; i < __tabtle.Length; i++)
                {
                    if (__tabtle[i].getDuration_TL_Event() <= 0f)
                    {
                        CommonDevTools.WARNING("object " + _TL_event.GO.name+ " event n° " + i + " durée invalide  : IGNORE !");
                        continue;       // on passe à l'évenement suivant
                    }
                    if (__tabtle[i].getStartTime_TL_Event() > dureeMax)
                    {
                        CommonDevTools.WARNING("object " + _TL_event.GO.name + " event n° " + i + " Debut apres fin partie (>durée max) : IGNORE !");
                        continue;       // on passe à l'évenement suivant
                    }
                    // evenement OK : ajouté 
                   print("object " + _TL_event.GO.name + " event n° " + i + " ajouté !");

                    p_Liste_TL_Events.Add(__tabtle[i]);
                    p_nb_TL_Events++;
                }
        }

        // Methode 1
        ConstruireTimeLine();
    }

    public void Update() {
        if (ChronoDemarre && !ChronoEnPause)
        {
            chrono += Time.deltaTime;
            TestStopChrono();
            Piloter_Event_TL();         // Methode 1 & 2
        }
    }

    //*******************************************************************************************************//
    // à faire : 
    //   Methode 1 : [A faire ]
    //     stocker tous les évenement dans une timeline (liste) d'évements à déclencher ou arrêter  .... 
    //     prétraitement plus difficile   mais ensuite plus rapide ;-)
    //     == construire une liste d'évenements à delcncher à partir de l'analyse de p_Liste_TL_Events
    //     == ensuite à chaque frame teste s'il est temps de réaliser une opération sur l'évenement suivant
    //     à prilégier car la TimeLine pourraît être un outils utile pour d'autres aspects du projet ... 
    // OU
    //    Methode 2 :  [pour info ] 
    //     test à chaque frame des évenements à déclencher ou arrêter ... : plus simple et plus lent !! 
    //     == parcourir p_Liste_TL_Events pour savoir si l'evenement doit être déclenché ou arrêté 
    //     lent et peu généralisable mais peut répondre au pb immédiat 
    //*******************************************************************************************************//
    
    enum TypeCdeEvent { START, STOP }
    struct struct_evt_TL 
    {
        public float time_evt_TL;
        public TypeCdeEvent cdeEvt_TL;
        public Interface_TL_Events evt_TL;
        // constructeur
        public struct_evt_TL(float t, TypeCdeEvent o, Interface_TL_Events e) { time_evt_TL = t; cdeEvt_TL = o; evt_TL = e; }
    }

    // ci dessous sera utilisé pour trier la liste avec la méthode sort()
    static int Compare_Struct_Evt_TL(struct_evt_TL x, struct_evt_TL y)
    {
        return ((struct_evt_TL)x).time_evt_TL.CompareTo(((struct_evt_TL)y).time_evt_TL); 
    }

    private List<struct_evt_TL> TimeLine;           // la timeline = planification de toutes les commandes à exécuter pendant le temps du jeu, soit démarer soit arrêter n évenement
    private int indexTimeLine;                      // le prochain évenement à traiter 
    private int maxIndexTimeLine;                   // le dernier évenement à traiter 
    private struct_evt_TL prochaine_cde_event;

    /* sera appelée à chaque frame  depuis Update()
       y a t'il un évenement de la timeline à déclencher en fonction de la valeur actuelle du chrono  ?
    */
    private void Piloter_Event_TL()
    {    
        /*******************  ! ! ! A FAIRE ! ! !   *******************************/

        foreach (struct_evt_TL elem in TimeLine)
        {
            if ((Mathf.Round(chrono * 100)) == (Mathf.Round(elem.time_evt_TL * 100)))
            {
                if (elem.cdeEvt_TL == TypeCdeEvent.START)
                    elem.evt_TL.start_TL_Event();
                else if (elem.cdeEvt_TL == TypeCdeEvent.STOP)
                    elem.evt_TL.stop_TL_Event();
            }
        }
    }





    /* sera appelée une seule fois avant le départ du jeu  depuis la méthode Configurer ()
       y a t'il un évenement de la timeline à déclencher en fonction de la valeur actuelle du chrono  ?

        la liste p_Liste_TL_Events  rescence tous les évenements de TL i.e. les scripts (ou instances de classes) réalisant l'interface Interface_TL_Events

        il faut définir les commandes de démarrage et d'arrêt de ces scripts puis les ordonner en fonction du temps

        il faut remplir la liste List<struct_evt_TL> TimeLine;  qui sera interprétée par Piloter_Event_TL() pendant l'exécution
    */
    private void ConstruireTimeLine()
    {
        foreach (Interface_TL_Events evt in p_Liste_TL_Events)
        {
            float period = 0f;
            if (evt.isPerdiodic_TL_Event(out period))
            {
                float evtStart = evt.getStartTime_TL_Event();
                float evtStop = evt.getDuration_TL_Event();

                while (evtStop <= evt.getStopTime_TL_Event())
                {
                    AddEvtToTimeline(evt, evtStart, evtStop);

                    evtStart += period;
                    evtStop += period;
                }
            }
            else
                AddEvtToTimeline(evt, evt.getStartTime_TL_Event(), evt.getDuration_TL_Event());
        }

        TimeLine.Sort(Compare_Struct_Evt_TL);
    }
    //*******************************************************************************************************//

    private void AddEvtToTimeline(Interface_TL_Events evt, float evtStart, float evtStop)
    {
        TimeLine.Add(new struct_evt_TL(evtStart, TypeCdeEvent.START, evt));
        TimeLine.Add(new struct_evt_TL(evtStop, TypeCdeEvent.STOP, evt));
    }


    public void StartChrono() {
        chrono = 0;
        ChronoDemarre = true;
        foreach (Interface_TL_Events evTL in p_Liste_TL_Events)
            evTL.TL_ChronoDemarre();
        indexTimeLine = 0;
        //prochaine_cde_event = TimeLine[0];    // à décommenter une fois la structure timeline créée
                                                // commentée pour l'instant pour éviter des erreurs a l'exécution 
    }
    private void StartChrono(float __delay) { Invoke("StartChrono", __delay); }
    private void ReStartChrono() { dureeJeu += chrono; StartChrono(); }
    private void PauseChrono() {
        ChronoEnPause = true;
        foreach (Interface_TL_Events evTL in p_Liste_TL_Events) {
            if (evTL.isPausable_TL_Event()) evTL.pause_TL_Event();
            evTL.TL_ChronoEnPause();
        }
    }
    private void ReprendreChrono()              {
        ChronoEnPause = false;
        foreach (Interface_TL_Events evTL in p_Liste_TL_Events)
             evTL.TL_ChronoReprise();
    }

    private void TestStopChrono() {
        if (chrono > dureeMax)
        {
            FinDePartie();
            _MGR_SceneManager.Instance.FinDePartie(_MGR_SceneManager.FIN_DE_PARTIE.PERDU_CHRONO);
        }
    }
    
    public void Pause()
    {
        PauseChrono();
        Time.timeScale = 0;
    }

    public void Resume()
    {
        Time.timeScale = 1;
        ReprendreChrono();
    }

    public  void FinDePartie() {
        ChronoDemarre = false;
        dureeJeu += chrono;
        foreach (Interface_TL_Events evTL in p_Liste_TL_Events)
            evTL.TL_ChronoArrete();
    }
}
