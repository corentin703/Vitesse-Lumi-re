using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ATLEvent : ITLEvent
{
    public float StartTime { get; private set; }
    public float EndTime { get; private set; }
    public bool IsPeriodic { get; private set; }
    public float Period { get; private set; }
    public float Duration { get; private set; }

    public ATLEvent(float startTime, float endTime, bool isPeriodic = false, float period = 0, float duration = 15)
    {
        StartTime = startTime;
        EndTime = endTime;
        IsPeriodic = isPeriodic;
        Period = period;
        Duration = duration;
    }
    
    public abstract void OnEventStart();
    
    public abstract void OnEventPause();
    
    public abstract void OnEventResume();
    
    public abstract void OnEventStop();
}
