using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = System.Random;

public class MGR_TimeLine : Singleton<MGR_TimeLine>
{
    private enum ETLEventType
    {
        Start,
        Stop,
    }
    
    private struct STLEvent
    {
        public float ActionTime;
        public ETLEventType TLEventType;
        public ATLEvent Event;

        public STLEvent(float actionTime, ETLEventType tlEventType, ATLEvent evt)
        {
            ActionTime = actionTime;
            TLEventType = tlEventType;
            Event = evt;
        }
    }

    private List<STLEvent> m_events;
    private List<ATLEvent> m_runningEvents;
    
    public float Chrono { get; private set; }
    public bool IsChronoStarted { get; private set; }
    public bool IsChronoPaused { get; private set; }

    [SerializeField]
    private float _maxGameDuration = 1 * 60;
    public float MaxGameDuration
    {
        get { return _maxGameDuration; } 
        private set
        {
            if (!IsSetUp)
                _maxGameDuration = value;
            else
                throw new SystemException("[MGR_TimeLine] You can't define MaxGameDuration during game's execution");
        }
    }

    protected override void Awake()
    {
        base.Awake();
        
        m_events = new List<STLEvent>();
        m_runningEvents = new List<ATLEvent>();
    }

    public bool IsSetUp { get; private set; } = false;
        
    public void SetUp(ATLEvent[] events)
    {
        m_events = new List<STLEvent>();
        m_runningEvents = new List<ATLEvent>();
        
        buildTimeLine(events);
        
        IsSetUp = true;
    }
    
    public void Notify(GameManager.EManagerNotif managerNotif)
    {
        if (managerNotif == GameManager.EManagerNotif.SceneChanged)
            IsSetUp = false;
        else if (managerNotif == GameManager.EManagerNotif.GamePaused)
            ChronoPause();
        else if (managerNotif == GameManager.EManagerNotif.GameResumed)
            ChronoResume();
    }

    private void buildTimeLine(ATLEvent[] events)
    {
        foreach (ATLEvent evt in events)
        {
            if (evt.IsPeriodic)
            {
                float startTime = evt.StartTime;
                float endTime = evt.StartTime + evt.Duration;
                
                while (endTime < evt.EndTime)
                {
                    buildTLEventPair(startTime, endTime, evt);

                    startTime = endTime + ((evt.Period == 0) ? UnityEngine.Random.Range(2.0f, _maxGameDuration) : evt.Period);
                    endTime = startTime + evt.Duration;
                }
            }
            else
                buildTLEventPair(evt.StartTime, evt.EndTime, evt);
        }
    }

    private void buildTLEventPair(float startTime, float endTime, ATLEvent evt)
    {
        m_events.Add(new STLEvent(startTime, ETLEventType.Start, evt));
        m_events.Add(new STLEvent(endTime, ETLEventType.Stop, evt));   
    }

    private void Update()
    {
        if (IsChronoStarted && !IsChronoPaused)
        {
            Chrono += Time.deltaTime;
  
            if (testChronoEnd())
            {
                ChronoStop();
                GameManager.Instance.EndGame(GameManager.EndWay.Loose);
            }
            
            manageTimeLine();       
        }
    }

    private void manageTimeLine()
    {
        foreach (STLEvent evt in m_events.ToArray())
        {
            if ((int)(Mathf.Round(Chrono * 10)) == (int)(Mathf.Round(evt.ActionTime * 10)))
            {
                if (evt.TLEventType == ETLEventType.Start)
                {
                    evt.Event.OnEventStart();
                    m_runningEvents.Add(evt.Event);
                }
                else if (evt.TLEventType == ETLEventType.Stop)
                {
                    evt.Event.OnEventStop();
                    m_runningEvents.Remove(evt.Event);
                }
                
                m_events.Remove(evt);
            }
        }
    }
    
    private bool testChronoEnd()
    {
        return Chrono >= MaxGameDuration;
    }
    
    public void ChronoStart()
    {
        foreach (ATLEvent evt in m_runningEvents)
        {
            evt.OnEventStart();
        }

        IsChronoStarted = true;
    }

    public void ChronoPause()
    {
        foreach (ATLEvent evt in m_runningEvents)
        {
            evt.OnEventPause();
        }

        Time.timeScale = 0;
        
        IsChronoPaused = true;
    }

    public void ChronoResume()
    {
        foreach (ATLEvent evt in m_runningEvents)
        {
            evt.OnEventResume();
        }
        
        Time.timeScale = 1;
        
        IsChronoPaused = false;
    }

    public void ChronoStop()
    {
        foreach (ATLEvent evt in m_runningEvents)
        {
            evt.OnEventStop();
        }
        
        IsChronoStarted = false;
    }
    
}
