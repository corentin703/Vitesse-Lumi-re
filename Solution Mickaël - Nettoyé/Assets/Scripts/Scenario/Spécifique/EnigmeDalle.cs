using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmeDalle : Action_Scenario_Etape
{
    private static EnigmeDalle p_instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public static EnigmeDalle Instance { get { return p_instance; } }

    [SerializeField]
    private List<Dalle> lDallesToWalkOn = new List<Dalle>();
    private List<Dalle> lDallesTampon;

    [SerializeField]
    private GameObject GOToDeleteOnComplete;

    void Awake()
    {       
        if (p_instance == null)
            
            p_instance = this;
        else if (p_instance != this)
            Destroy(gameObject);

        lDallesTampon = new List<Dalle>(lDallesToWalkOn);
    }

    public override void Update()
    {
        return;
    }

    public void Verif(Dalle dalle)
    {
        if (lDallesTampon[0] == dalle)
        {
            lDallesTampon.Remove(dalle);
            Debug.Log("Yeah, great choice !");
            dalle.SetCurrentState(Dalle.DalleState.PRESSED);
            if (lDallesTampon.Count == 0)
            {
                Declencher_Etape_Suivante_Du_Scenario();
            }
        }
        else
        {
            Debug.Log("Nope, try again !  " + lDallesToWalkOn.Count.ToString());

            foreach (Dalle element in lDallesToWalkOn)
                element.SetCurrentState(Dalle.DalleState.UNPRESSED);

            lDallesTampon = new List<Dalle>(lDallesToWalkOn);
        }
    }

    public override void Declencher_Etape_Suivante_Du_Scenario()
    {
        //foreach (GameObject element in lGameObjectsToWalkOn)
        //    Destroy(element);

        Destroy(GOToDeleteOnComplete);

        base.Declencher_Etape_Suivante_Du_Scenario();
    }
}
