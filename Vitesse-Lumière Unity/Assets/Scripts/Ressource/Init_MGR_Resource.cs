using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init_MGR_Resource : MonoBehaviour
{
    public MGR_Resource.SResourceInfo[] ResourceInfos;
    private void Awake()
    {
        if (MGR_Resource.Instance)
            MGR_Resource.Instance.SetUp(ResourceInfos);
        
        Destroy(this);
    }
}
