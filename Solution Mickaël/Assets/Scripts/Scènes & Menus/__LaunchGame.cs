using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class __LaunchGame : MonoBehaviour
{


    void Start()        // est appelé apres les awake d'initialisation 
    {
        _MGR_SceneManager.Instance.LoadScene("Scene_Play");
        //_MGR_SceneManager.Instance.LoadScene(0);
    }
}


