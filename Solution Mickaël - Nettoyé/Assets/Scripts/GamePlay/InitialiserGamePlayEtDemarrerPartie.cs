using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiserGamePlayEtDemarrerPartie : MonoBehaviour {

    //Awake is always called before any Start functions
    void Awake(){
        // _MGR_GamePlay....... initialisations nécessaires dans awake
     }

    void Start()
    {
        _MGR_GamePlay.Instance.StartPlay();
    }

}

