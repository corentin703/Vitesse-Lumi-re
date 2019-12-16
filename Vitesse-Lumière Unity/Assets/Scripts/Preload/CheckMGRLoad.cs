

using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckMGRLoad : MonoBehaviour
{
    void Awake()
    {
        if (GameObject.Find("EGO_MGR") == null)
            UnityEngine.SceneManagement.SceneManager.LoadScene("_preload");

        Destroy(this);
    }
}
