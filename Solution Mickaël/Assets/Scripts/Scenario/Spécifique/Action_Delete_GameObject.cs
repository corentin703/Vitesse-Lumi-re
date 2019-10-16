using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_Delete_GameObject : Action_Scenario_Etape
{
    public List<GameObject> lGameObjectsToDelete = new List<GameObject>();

    public override void Update()
    {
        foreach (GameObject p_element in lGameObjectsToDelete)
        {
            Destroy(p_element);
        }

        Declencher_Etape_Suivante_Du_Scenario();
    }
}
