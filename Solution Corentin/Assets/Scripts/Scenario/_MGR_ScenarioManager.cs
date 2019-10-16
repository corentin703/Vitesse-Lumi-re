using System    .Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _MGR_ScenarioManager : MonoBehaviour
{
    private static  _MGR_ScenarioManager    p_instance = null  ;              //Static instance of GameManager which allows it to be accessed by any other script.
    public static   _MGR_ScenarioManager    Instance { get { return p_instance; } }    

    public   uint   nbEtapes { get; private set; }

    private  int   p_num_etapeEnCours;
    private Scenario_Etape p_etapeEnCours;
    private GameObject ojet_etape_en_cours;
    // private Scenario_Etape[]            p_etapes;                                    
    private List<Scenario_Etape> p_etapes ;         // pour stocker les étapes
    private AudioClip m_pickingSound;

    //Awake is always called before any Start functions
    void Awake()
    {
        // ===>> SingletonMAnager

        //Check if instance already exists
        if (p_instance == null)
            //if not, set instance to this
            p_instance = this;
        //If instance already exists and it's not this:
        else if (p_instance != this)
            //Then destroy this. This enforces our singleton pattern, meaning there can only ever be one instance of a GameManager.
            Destroy(gameObject);
        //Sets this to not be destroyed when reloading scene
        // DontDestroyOnLoad(gameObject);   par nécessaire ici car déja fait  par script __DDOL sur l'objet _EGO_app qui recueille tous les mgr



    }

    public void Configurer( Scenario_Etape[] __etapes_du_scenario, AudioClip pickingSound = null)
    {
        // méthode non robuste 
        //p_etapes.AddRange(__etapes_du_scenario);

        // méthode  robuste 
        // ====> vérifier si les étapes  passées en paramètres sont valides
        if (p_etapes != null)
            p_etapes.Clear(); // vider le tableau (si 2ème ou suivantes exécutions....
        else
            p_etapes = new List<Scenario_Etape>(); // 1ere exécution : instantiation
        nbEtapes = 0;

        foreach (Scenario_Etape _etape in __etapes_du_scenario)
        {
            // un objet valide ?
            if (_etape.interactiveObject == null)
                CommonDevTools.QUIT_APP("objet étape " + nbEtapes + " non défini");
            
            // un type d'intearction défini ? 
            if (_etape.interaction == Scenario_Etape.Interactions.UNDEFINED)
                CommonDevTools.ERROR("interaction étape " + nbEtapes + " non définie", this.gameObject);

            // recherches des components dérivés de Action : ! seront tous ajoutés, qu'il soient activés ou non !
            _etape.actions = _etape.interactiveObject.GetComponents<Action_Scenario_Etape>();
            
            // au moins une action définie ? 
            if (_etape.actions.Length==0)
                CommonDevTools.ERROR("pas d'actions pour l'étape " + nbEtapes + " et l'objet " + _etape.interactiveObject.name, this.gameObject);
            else
                CommonDevTools.DEBUG(_etape.actions.Length + " actions pour l'étape " + nbEtapes + " et l'objet "+ _etape.interactiveObject.name);

            // desactiver les actions et met isPartOf_Scenario à vrai
            foreach (Action_Scenario_Etape action in _etape.actions)
            {
                action.enabled = false;
                action.isPartOf_Scenario = true;
            }
            
            // Etape valide : ajouté au scénario 
            p_etapes.Add(_etape);
            nbEtapes++;
        }

        if (pickingSound)
            m_pickingSound = pickingSound;
    }

    public void Demarrer()
    {
        p_num_etapeEnCours = 0;
        ActiverEtapeEnCours();
    }

    public void EtapeSuivante()
    {
        // désactiver les actions
        foreach (Action_Scenario_Etape action in p_etapes[p_num_etapeEnCours].actions)
            action.enabled = false;

        // désactiver ou pas l'objet de l'étape 
        if (p_etapes[p_num_etapeEnCours].supprimer_dans_etape_suivante)
            ojet_etape_en_cours.SetActive(false);

        // étape suivante du scénario  ou fin du jeu ??
        p_num_etapeEnCours++;
        if (p_num_etapeEnCours >= nbEtapes)
        {
            _MGR_TimeLine.Instance.FinDePartie();

            _MGR_SceneManager.Instance.FinDePartie(_MGR_SceneManager.FIN_DE_PARTIE.GAGNE);
            //_MGR_SceneManager.Instance.LoadScene("Scene_YouLose");        // selon des critères à définir plus tard
        }
        else
            ActiverEtapeEnCours();
    }

    private void ActiverEtapeEnCours ()
    {
        p_etapeEnCours = p_etapes[p_num_etapeEnCours];
        ojet_etape_en_cours = p_etapeEnCours.interactiveObject;

        CommonDevTools.DEBUG(">>> Etape " + p_num_etapeEnCours + " :  l'OBJET " + p_etapeEnCours.interactiveObject.name + " et ses " + p_etapeEnCours.actions.Length + " actions sont activés.");
        CommonDevTools.DEBUG(p_etapeEnCours.messageDescription); // en attendant meilleure UI....

        ojet_etape_en_cours.SetActive(true);
        foreach (Action_Scenario_Etape action in p_etapeEnCours.actions)
            action.enabled = true;
    }

    public AudioClip GetPickingSound()
    {
        return m_pickingSound;
    }

}
