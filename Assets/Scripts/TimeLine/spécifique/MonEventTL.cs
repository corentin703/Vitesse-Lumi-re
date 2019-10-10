using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonEventTL : MonoBehaviour, Interface_TL_Events
{
    private AudioSource p_as;
    public float DebutEventTL = 10;
    public bool Periodique = false;
    public float Periode = 3;
    public float DureeEventTL = 1;
    public float MaxDureeEventTL = 1;

    public void Awake()
    {
        p_as=this.gameObject.GetComponent<AudioSource>();
    }

    public void stop_TL_Event()
    {
        Destroy(this);
    }

    public float getDuration_TL_Event()
    {
        if (!p_as)
            throw new NotImplementedException();

        return DureeEventTL;
        //return p_as.clip.length;
    }

    public float getStartTime_TL_Event()
    {
        return DebutEventTL;
    }
    public float getStopTime_TL_Event()
    {
        return DebutEventTL + MaxDureeEventTL;
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
        if (!p_as) throw new NotImplementedException();
        p_as.Play();
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
