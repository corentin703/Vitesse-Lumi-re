using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Racing : MonoBehaviour
{
    [SerializeField]
    CheckPoint[] checkPoints;

    [SerializeField]
    Text textNbTour;

    [SerializeField]
    GameObject pauseMenu;

    bool finishedTour = false;

    int nbTour;
    
    void Update()
    {
        checkIfFinished();
        if (Input.GetButtonDown("Pause")) switchPause();
    }

    void checkIfFinished()
    {
        finishedTour = true;
        foreach (CheckPoint CP in checkPoints)
        {
            if (CP.isTrigger == false) finishedTour = false;
        }
        if (finishedTour == true)
        {
            nbTour++;
            textNbTour.text = nbTour.ToString();
            foreach (CheckPoint CP in checkPoints)
            {
                CP.disableTrigger();
            }
        }
    }

    void switchPause()
    {
        pauseMenu.SetActive(!pauseMenu.activeInHierarchy);
        if (Cursor.lockState == CursorLockMode.Locked)
        {
            Time.timeScale = 0;
            Cursor.lockState = CursorLockMode.None;
            Cursor.visible = true;
        }
        else
        {
            Time.timeScale = 1f;
            Cursor.lockState = CursorLockMode.Locked;
            Cursor.visible = false;
        }
    }
}
