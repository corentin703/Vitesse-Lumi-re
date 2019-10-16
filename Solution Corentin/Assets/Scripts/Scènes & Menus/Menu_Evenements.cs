using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Menu_Evenements : MonoBehaviour {



    public void PressPlay() {
        Debug.Log("Pressed");
        _MGR_SceneManager.Instance.LoadScene("Scene_Play");
    }
    public void PressQuit() {
        CommonDevTools.QUIT_APP("! fin demandée de l'application!");
    }

}
