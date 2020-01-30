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

    bool finishedTour = false;

    int nbTour;
    
    void Update()
    {
        checkIfFinished();
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
}
