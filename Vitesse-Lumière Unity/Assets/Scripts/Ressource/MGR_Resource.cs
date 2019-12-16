using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGR_Resource : Singleton<MGR_Resource>
{
    [System.Serializable]
    public struct SResourceInfo
    {
        public string UniqueIdentifier;
        public string Name;
        public string Description;

        public SResourceInfo(string uniqueIdentifier, string name, string description)
        {
            UniqueIdentifier = uniqueIdentifier;
            Name = name;
            Description = description;
        }
    }

    public List<AResource> Resources { get; private set; }
    private Dictionary<string, SResourceInfo> m_dictResourceInfos;

    public bool IsSetUp { get; private set; } = false;
    public void SetUp(SResourceInfo[] resourceInfos)
    {
        m_dictResourceInfos = new Dictionary<string, SResourceInfo>();
        Resources = new List<AResource>();
        
        IsSetUp = true;
    }

    public bool IsBelonged(AResource resource)
    {
        return (Resources.Contains(resource));
    }

    public void AddItem(AResource resource)
    {
        if (IsBelonged(resource))
            resource.Add(resource.UnitNumber);
        else
            Resources.Add(resource);
    }

    public void RemoveItem(AResource resource)
    {
        if (IsBelonged(resource))
            Resources.Remove(resource);
        else
            Debug.LogError("[" + GetType().Name + "] Trying to remove an non belonged object");
    }

    public void Notify(GameManager.EManagerNotif managerNotif)
    {
        if (managerNotif == GameManager.EManagerNotif.SceneChanged)
            IsSetUp = false;
    }
    
}
