using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckPreloadDone : MonoBehaviour {
    // s'assurer que l'application a commancé par la scène _preload
    // et donc que les manager sont lancés !  (test existence EGO qui héberge tous les manager)
    // si ce n'est pas le cas : chargement de la scène PRE LOAD ...
    // ce qui permet d'exécuter proprement l'application depuis n'importe quelle scène dans l'IDE editor d'Unity.
    void Awake()
        {
            GameObject check = GameObject.Find("_EGO_preload_init");
            if (check == null)
                    UnityEngine.SceneManagement.SceneManager.LoadScene("_preload"); 
        }
}
