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
        public ITLEvent Event;

        public STLEvent(float actionTime, ETLEventType tlEventType, ITLEvent evt)
        {
            ActionTime = actionTime;
            TLEventType = tlEventType;
            Event = evt;
        }
    }

    private List<STLEvent> m_events;
    private List<ITLEvent> m_runningEvents;
    
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
                throw new SystemException("[" + GetType().Name + "] You can't define MaxGameDuration during game's execution");
        }
    }

    protected override void Awake()
    {
        base.Awake();
        
        m_events = new List<STLEvent>();
        m_runningEvents = new List<ITLEvent>();
    }

    public bool IsSetUp { get; private set; } = false;
        
    public void SetUp(ATLEventGO[] events)
    {
        m_events = new List<STLEvent>();
        m_runningEvents = new List<ITLEvent>();
        
        buildTimeLine(events);
        
        IsSetUp = true;
    }
    
    public void Notify(GameManager.EManagerNotif managerNotif)
    {
        if (managerNotif == GameManager.EManagerNotif.SceneChanged)
            IsSetUp = false;
        else if (managerNotif == GameManager.EManagerNotif.GameStart)
            ChronoStart();
        else if (managerNotif == GameManager.EManagerNotif.GamePaused)
            ChronoPause();
        else if (managerNotif == GameManager.EManagerNotif.GameResumed)
            ChronoResume();
        else if (managerNotif == GameManager.EManagerNotif.GameEnded)
            ChronoStop();
    }

    private void buildTimeLine(ATLEventGO[] events)
    {
        foreach (ATLEventGO evt in events)
        {
            AddEvent(evt, false);
        }
    }
    
    public void AddEvent(ITLEvent tlEvent, bool addElapsedTime = true)
    {
        if (tlEvent.IsPeriodic)
        {
            float startTime = tlEvent.StartTime + ((addElapsedTime) ? Chrono : 0);
            float endTime = tlEvent.StartTime + tlEvent.Duration + ((addElapsedTime) ? Chrono : 0);

            while (endTime < tlEvent.EndTime + ((addElapsedTime) ? Chrono : 0))
            {
                buildTLEventPair(startTime, endTime, tlEvent);

                startTime = endTime + ((tlEvent.Period == 0) ? UnityEngine.Random.Range(2.0f, tlEvent.EndTime + ((addElapsedTime) ? Chrono : 0)) : tlEvent.Period);
                endTime = startTime + tlEvent.Duration;
            }
        }
        else
            buildTLEventPair(tlEvent.StartTime + ((addElapsedTime) ? Chrono : 0), tlEvent.EndTime+ ((addElapsedTime) ? Chrono : 0), tlEvent);
        
        Debug.Log("[" + GetType().Name + "] Event added");
    }

    public void RemoveEvent(ITLEvent tlEvent)
    {
        bool log = false;
        
        foreach (STLEvent evt in m_events.ToArray())
        {
            if (evt.Event.GetType() == tlEvent.GetType())
            {
                if (m_runningEvents.Contains(tlEvent))
                {
                    tlEvent.OnEventStop();
                    m_runningEvents.Remove(tlEvent);
                }

                m_events.Remove(evt);

                if (tlEvent is ATLEventGO)
                    Destroy((ATLEventGO)tlEvent);

                log = true;
            }
        }
        
        if (log)
            Debug.Log("[" + GetType().Name + "] Event deleted");
    }

    private void buildTLEventPair(float startTime, float endTime, ITLEvent evt)
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
                GameManager.Instance.EndGame(GameManager.EndWay.Loose);

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
        foreach (ITLEvent evt in m_runningEvents)
        {
            evt.OnEventStart();
        }

        IsChronoStarted = true;
        
        Debug.Log("[" + GetType().Name + "] Chrono started");
    }

    public void ChronoPause()
    {
        foreach (ITLEvent evt in m_runningEvents)
        {
            evt.OnEventPause();
        }

        Time.timeScale = 0;
        
        IsChronoPaused = true;
        
        Debug.Log("[" + GetType().Name + "] Chrono paused");
    }

    public void ChronoResume()
    {
        foreach (ITLEvent evt in m_runningEvents)
        {
            evt.OnEventResume();
        }
        
        Time.timeScale = 1;
        
        IsChronoPaused = false;
        
        Debug.Log("[" + GetType().Name + "] Chrono resumed");
    }

    public void ChronoStop()
    {
        foreach (ITLEvent evt in m_runningEvents)
        {
            evt.OnEventStop();
        }
        
        IsChronoStarted = false;
        
        Debug.Log("[" + GetType().Name + "] Chrono stopped");
    }
    
}
