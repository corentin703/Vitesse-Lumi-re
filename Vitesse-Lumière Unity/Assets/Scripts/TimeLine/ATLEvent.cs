using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ATLEvent : MonoBehaviour
{
    [SerializeField] private float m_startTime;
    public float StartTime
    {
        get { return m_startTime; }
        private set { m_startTime = value; }
    }
    
    [SerializeField] private float m_endTime;
    public float EndTime
    {
        get { return m_endTime; }
        private set { m_endTime = value; }
    }

    [SerializeField] private bool m_isPeriodic;
    public bool IsPeriodic { 
        get { return m_isPeriodic; }
        private set { m_isPeriodic = value; } 
    }
    
    [ConditionalHide("m_isPeriodic", true)]
    [Tooltip("If set to 0, period will be random")]
    [SerializeField] private float m_period;
    public float Period
    {
        get { return m_period; }
        private set { m_period = value; }
    }
    
    [ConditionalHide("m_isPeriodic", true)]
    [Tooltip("Define the duration between each period")]
    [SerializeField] private float m_duration = 1f;
    public float Duration
    {
        get { return m_duration; }
        private set { m_duration = value; }
    }

    protected void Awake()
    {
        if (EndTime <= 0)
            throw new Exception("[" + this.GetType().Name + "] in Awake -> You can't set an end time <= 0: it's currently set to " + EndTime);
        
        if (IsPeriodic && m_duration <= 0f)
            throw new Exception("[" + this.GetType().Name + "] in Awake -> You can't set a duration <= 0: it's currently set to " + m_duration);
    }

    public abstract void OnEventStart();
    
    public abstract void OnEventPause();
    
    public abstract void OnEventResume();
    
    public abstract void OnEventStop();

}
