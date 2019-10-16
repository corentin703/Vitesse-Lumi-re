using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_PickObjectList : INTERACTION_CLICK_AND_PICK
{
    [SerializeField]
    private List<GameObject> lGameObjectsToPick = new List<GameObject>();

    [SerializeField]
    private List<GameObject> lGameObjectsToDelete = new List<GameObject>();

    override public void Object_Picked(GameObject GO)
    {
        if (lGameObjectsToPick.Contains(GO))
        {
            lGameObjectsToPick.Remove(GO);
            Destroy(GO);

            if (lGameObjectsToPick.Count == 0)
                Declencher_Etape_Suivante_Du_Scenario();
        }
    }

    override public void Declencher_Etape_Suivante_Du_Scenario()
    {
        foreach (GameObject element in lGameObjectsToDelete)
            Destroy(element);

        base.Declencher_Etape_Suivante_Du_Scenario();
    }
}
