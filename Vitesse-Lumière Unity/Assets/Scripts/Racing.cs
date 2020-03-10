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

    int nbTour = 1;
    
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
            if (nbTour - 1 > nbTourMax)
            {
                PlayerPrefs.SetInt("time", (int)timer);
                Cursor.lockState = CursorLockMode.None;
                Cursor.visible = true;
                GameManager.Instance.LoadScene("ScoreSaving");
            }
            if (PlayerPrefs.HasKey("language") && PlayerPrefs.GetString("language") == "FR")
            {
                textNbTour.text = nbTour.ToString() + " / 3 tours";
            }
            else textNbTour.text = nbTour.ToString() + " / 3 laps";
            foreach (CheckPoint CP in checkPoints)
            {
                CP.disableTrigger();
            }
        }
    }
}
