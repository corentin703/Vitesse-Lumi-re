
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Initialiser_TL_Events : MonoBehaviour
{

    public Event_TL[] TL_Events;

    void Awake()
    {
        if (_MGR_TimeLine.Instance)
            _MGR_TimeLine.Instance.Configurer(TL_Events);
    }

}
