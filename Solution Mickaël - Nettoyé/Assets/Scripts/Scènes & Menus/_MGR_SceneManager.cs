using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class _MGR_SceneManager : MonoBehaviour
{
    public string[] arr_SceneName;      // paramètre public visible dans l'IDE unity (Editor)

    private static _MGR_SceneManager p_instance = null  ;              //Static instance of GameManager which allows it to be accessed by any other script.
    public static _MGR_SceneManager Instance { get { return p_instance; } }


    private  uint                       p_nbScenes;
    public   uint                       NbScenes { get { return p_nbScenes; } }    // modificateur privé : n'apparaît pas dans l'IDE

    private string[]                    p_arr_Scenes ;                                   // pour stocker les noms de scènes 
    private List<string>                p_list_Scenes = new List<string>();              // une autre facon de stocker les scènes du jeu
    private Dictionary<string, string>  p_dict_Scenes = new Dictionary<string, string>();// encore une autre facon de stocker les scènes du jeu (peu intéressant ici)

    public enum FIN_DE_PARTIE { GAGNE, PERDU_CHRONO, PERDU_CHUTE, PERDU_BAD_CHOICE };  
        
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


        // ====> vérifier si les scènes passées en paramètres existent 
        // ====> les stocke dans un dictionnaire
        p_arr_Scenes = new string[arr_SceneName.Length];
        p_nbScenes = 0;

        foreach (string _scene_name in arr_SceneName) {
            p_arr_Scenes[p_nbScenes]= _scene_name;                 // la scène sera accessible avec son indice 
            p_nbScenes++;


            p_list_Scenes.Add(_scene_name);              // la scène sera accessible avec son indice 
            p_dict_Scenes.Add(_scene_name, _scene_name); // la scène sera accessible avec son NOM
        }

    }


    //Launch one scene   by index
    public void LoadScene(int __num_scene)
    {
        if (__num_scene >= p_nbScenes)
            CommonDevTools.QUIT_APP("! Erreur de référence de scène !");
        else
            SceneManager.LoadScene(p_arr_Scenes[__num_scene]);
       // SceneManager.LoadScene(p_list_Scenes[i]);
       // pour les dictionnaires  pas vraiment adapté  car indexé par clé  (ici chaine) 
       

    }

    //Launch one scene by name
    public void LoadScene(string __nom_scene)
    {
        // méthode non robuste : SceneManager.LoadScene(__scene_name);

        // méthode robuste : vérifier que le nom est correct    // le dictionnaire est ici approprié
        string _nom_scene_verifié;
        if (p_dict_Scenes.TryGetValue(__nom_scene, out _nom_scene_verifié))
            SceneManager.LoadScene(_nom_scene_verifié); 
        else
            CommonDevTools.QUIT_APP("! Erreur de référence de scène !");

        // SceneManager.LoadScene(p_list_Scenes[p_list_Scenes.IndexOf(__scene_name)]);
        //SceneManager.LoadScene(p_arr_Scenes[System.Array.IndexOf(p_arr_Scenes,__scene_name)]);
    }



    public void FinDePartie(FIN_DE_PARTIE __fin)
    {
        switch (__fin)
        {
            case FIN_DE_PARTIE.GAGNE:
                _MGR_SceneManager.Instance.LoadScene("Scene_YouWin");
                break;
            case FIN_DE_PARTIE.PERDU_CHRONO:
            case FIN_DE_PARTIE.PERDU_CHUTE:
            case FIN_DE_PARTIE.PERDU_BAD_CHOICE:
                _MGR_SceneManager.Instance.LoadScene("Scene_YouLose");
                break;

        }

    }
}
