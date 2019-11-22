using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Init_MGR_UI : MonoBehaviour
{
    // TODO: Your UI properties to define here and to pass to the MGR
    void Awake()
    {
        if (MGR_UI.Instance)
            MGR_UI.Instance.SetUp();
        
        Destroy(this);
    }
}
