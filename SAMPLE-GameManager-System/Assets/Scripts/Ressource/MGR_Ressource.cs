using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MGR_Ressource : Singleton<MGR_Ressource>
{
    [System.Serializable]
    public struct SRessourceInfo
    {
        public string UniqueIdentifier;
        public string Name;
        public string Description;

        public SRessourceInfo(string uniqueIdentifier, string name, string description)
        {
            UniqueIdentifier = uniqueIdentifier;
            Name = name;
            Description = description;
        }
    }

    private List<ARessource> m_ressources;
    private Dictionary<string, SRessourceInfo> m_dictRessourceInfos;

    private readonly SRessourceInfo _defaultRessourceInfo = new SRessourceInfo("default", "Default", "Default");
    
    public bool IsSetUp { get; private set; } = false;
    public void SetUp(SRessourceInfo[] ressourceInfos)
    {
        m_dictRessourceInfos = new Dictionary<string, SRessourceInfo>();
        m_ressources = new List<ARessource>();
        
        IsSetUp = true;
    }

    public bool IsBelonged(ARessource ressource)
    {
        return (m_ressources.Contains(ressource));
    }

    public void AddItem(ARessource ressource)
    {
        if (IsBelonged(ressource))
            ressource.Add(ressource.UnitNumber);
        else
            m_ressources.Add(ressource);
    }

    public void RemoveItem(ARessource ressource)
    {
        if (IsBelonged(ressource))
            m_ressources.Remove(ressource);
        else
            Debug.LogError("[MGR_Ressource] Trying to remove an non belonged object");
    }

    public SRessourceInfo GetRessourceInfos(ARessource ressource)
    {
        if (!IsSetUp)
            throw new Exception("[MGR_Ressource] Manager not set up correctly");

        if (m_dictRessourceInfos.ContainsKey(ressource.UniqueIdentifier))
            return m_dictRessourceInfos[ressource.UniqueIdentifier];
        
        Debug.LogWarning("[MGR_Ressource] Corresponding ressource didn't found: returning default informations");
        return _defaultRessourceInfo;
    }
    
    public void Notify(GameManager.EManagerNotif managerNotif)
    {
        if (managerNotif == GameManager.EManagerNotif.SceneChanged)
            IsSetUp = false;
    }
    
}
