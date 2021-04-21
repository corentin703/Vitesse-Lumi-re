using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class CheckPoint : MonoBehaviour
{
    public bool isTrigger = false;

    void OnTriggerEnter(Collider collid)
    {
        if (collid.tag == "Playermesh") isTrigger = true;
    }

    public void disableTrigger()
    {
        isTrigger = false;
    }
}
