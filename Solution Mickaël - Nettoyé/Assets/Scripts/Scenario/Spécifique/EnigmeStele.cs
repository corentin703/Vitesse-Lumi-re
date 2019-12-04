using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmeStele : Action_Scenario_Etape
{
    [SerializeField]
    private GameObject GOToDeleteOnComplete;

    public override void Update()
    {
        return;
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Cube")
        {
            Declencher_Etape_Suivante_Du_Scenario();
        }
    }

    override public void Declencher_Etape_Suivante_Du_Scenario()
    {
        Destroy(GOToDeleteOnComplete);
        base.Declencher_Etape_Suivante_Du_Scenario();
    }






}
