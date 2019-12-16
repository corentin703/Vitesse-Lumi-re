using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvtGORing : ATLEventGO
{
    public override void OnEventStart()
    {
        MGR_Song.Instance.PlaySound("ring");
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
        return;
    }
}
