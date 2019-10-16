using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BackToMenu : MonoBehaviour
{

    void Start() { Invoke("_retour_menu", 5.0f); }

    public void Update()
    {
        if (Input.GetKey(KeyCode.Space))
            _retour_menu();
    }

    private void _retour_menu()
    {
        _MGR_SceneManager.Instance.LoadScene("Scene_Menu");
    }

}
