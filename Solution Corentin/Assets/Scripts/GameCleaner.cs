using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameCleaner : MonoBehaviour
{
    private static GameCleaner p_instance = null;

    void Awake()
    {
        // ===>> Singleton

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

    }

    private void OnTriggerEnter(Collider Collision)
    {
        if (Collision.tag == "Player")
        {
            _MGR_TimeLine.Instance.FinDePartie();
            _MGR_SceneManager.Instance.FinDePartie(_MGR_SceneManager.FIN_DE_PARTIE.PERDU_CHUTE);
        }

        Destroy(Collision.gameObject); 
    }


}
