using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EvtLog : ATLEvent
{
    public EvtLog(float startTime, float endTime, bool isPeriodic = false, float period = 0, float duration = 15) 
        : base(startTime, endTime, isPeriodic, period, duration)
    {
        
    }

    public override void OnEventStart()
    {
        Debug.Log("LOOOOG");
        
        return;
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
