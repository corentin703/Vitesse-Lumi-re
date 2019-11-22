using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Preload : MonoBehaviour
{
    void Awake()
    {
        if (MGR_Gameplay.Instance 
            && MGR_Ressource.Instance
            && MGR_Song.Instance
            && MGR_TimeLine.Instance
            && MGR_UI.Instance)
            GameManager.Instance.GameStart();

        Debug.developerConsoleVisible = true;
        
        Destroy(this);
    }
}
