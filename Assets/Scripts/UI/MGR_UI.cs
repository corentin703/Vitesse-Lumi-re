using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGR_UI : Singleton<MGR_UI>
{
    public bool IsSetUp { get; private set; } = false;

    public void SetUp()
    {
        IsSetUp = true;
    }
    
    public void Notify(GameManager.EManagerNotif managerNotif)
    {
        if (managerNotif == GameManager.EManagerNotif.SceneChanged)
            IsSetUp = false;
    }
    
    // TODO: Define your UI's fonctions here 

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            Debug.Log("[" + GetType().Name + "] Inventory listing:");
            foreach (var VARIABLE in MGR_Resource.Instance.Resources)
            {
                Debug.Log("Name: " + VARIABLE.Name);
                Debug.Log("Description: " + VARIABLE.Description);
                Debug.Log("Next");
            }
        
            Debug.Log("End");
        }
    }
}
