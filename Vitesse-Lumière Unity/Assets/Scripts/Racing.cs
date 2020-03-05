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
    Text textTimer;

    bool finishedTour = false;

    int nbTour;
    
    [SerializeField]
    int nbTourMax;

    float timer = 0;

    void Update()
    {
        checkIfFinished();
        timer += Time.deltaTime;
        int i = (int)timer;
        textTimer.text = i.ToString() + " s"; 
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
            if (nbTour > nbTourMax)
            {
                PlayerPrefs.SetInt("time", (int)timer);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                GameManager.Instance.LoadScene("ScoreSaving");
            }
            if (PlayerPrefs.HasKey("language") && PlayerPrefs.GetString("language") == "FR")
            {
                textNbTour.text = nbTour.ToString() + " tour";
            }
            else textNbTour.text = nbTour.ToString() + " lap";
            if (nbTour > 1) textNbTour.text += "s";
            foreach (CheckPoint CP in checkPoints)
            {
                CP.disableTrigger();
            }
        }
    }
}
