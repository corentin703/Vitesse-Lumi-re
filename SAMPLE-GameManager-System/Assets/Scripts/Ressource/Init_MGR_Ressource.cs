using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init_MGR_Ressource : MonoBehaviour
{
    public MGR_Ressource.SRessourceInfo[] RessourceInfos;
    private void Awake()
    {
        if (MGR_Ressource.Instance)
            MGR_Ressource.Instance.SetUp(RessourceInfos);
        
        Destroy(this);
    }
}
