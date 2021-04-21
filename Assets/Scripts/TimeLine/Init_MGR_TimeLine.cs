using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init_MGR_TimeLine : MonoBehaviour
{
    public ATLEventGO[] Events;
    void Awake()
    {
        if (MGR_TimeLine.Instance)
            MGR_TimeLine.Instance.SetUp(Events);
        
        Destroy(this);
    }
}
