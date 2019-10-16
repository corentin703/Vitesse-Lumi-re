using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(_MGR_DayNight_Cycle))]
public class TL_DayNight_Cycle : MonoBehaviour, Interface_TL_Events
{
    public float LaunchCycleTime = 10;
    public float Periode = 3;

    [HideInInspector]
    public bool Periodique = true;
    [HideInInspector]
    public float MaxDureeEventTL = _MGR_TimeLine.DURE_MAX_PAR_DEFAUT;

    public void stop_TL_Event()
    {
        Destroy(this);
    }

    public float getDuration_TL_Event()
    {
        return 1f;
    }

    public float getStartTime_TL_Event()
    {
        return LaunchCycleTime;
    }
    public float getStopTime_TL_Event()
    {
        return LaunchCycleTime + MaxDureeEventTL;
    }


    public bool isPausable_TL_Event()
    {
        return false;
    }
    public bool isPerdiodic_TL_Event(out float period)
    {
        period = Periode;
        return Periodique;
    }

    public bool isRandomizable_TL_Event()
    {
        return false;// rien à faire ou pas prévu
    }


    public void pause_TL_Event()
    {
        return; // rien à faire ou pas prévu
    }

    public void randomize_TL_Event()
    {
        return; // rien à faire ou pas prévu
    }

    public void restart_TL_Event()
    {
        return; // rien à faire ou pas prévu
    }

    public void start_TL_Event()
    {
        _MGR_DayNight_Cycle.Instance.UpdateCycle();
    }

    public void TL_ChronoArrete()
    {
        return; // rien à faire ou pas prévu
    }

    public void TL_ChronoDemarre()
    {
        return; // rien à faire ou pas prévu
    }

    public void TL_ChronoEnPause()
    {
        return; // rien à faire ou pas prévu
    }
    public void TL_ChronoReprise()
    {
        return; // rien à faire ou pas prévu
    }
}
