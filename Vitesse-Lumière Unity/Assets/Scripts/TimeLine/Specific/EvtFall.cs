using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvtFall : ATLEvent
{
    public override void OnEventStart()
    {
        GameObject obj = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        obj.name = "Sphere";
        
        Debug.Log("[" + GetType().Name + "] Created");
    }

    public override void OnEventPause()
    {
        return;
    }

    public override void OnEventResume()
    {
        return;
    }

    public override void OnEventStop()
    {
        Destroy(GameObject.Find("Sphere"));
        Debug.Log("[" + GetType().Name + "] Destroyed");
    }
}
