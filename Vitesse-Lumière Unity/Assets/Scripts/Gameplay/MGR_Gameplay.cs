using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;
using Object = UnityEngine.Object;

public class MGR_Gameplay : Singleton<MGR_Gameplay>
{
    [System.Serializable]
    public struct SBonus
    {
        public string name;
        public int bonus;
    }

    public bool IsSetUp { get; private set; } = false;

    private Dictionary<string, int> m_dictBonus;
    
    public uint Score { get; private set; }
    public GameObject Player { get; private set; }

    public void SetUp(GameObject player, SBonus[] bonus)
    {
        m_dictBonus = new Dictionary<string, int>();
        
        foreach (SBonus element in bonus)
        {
            m_dictBonus.Add(element.name, element.bonus);
        }    
        
        Player = player;
        
        MGR_Song.Instance.SetUpPlayerAudio(player);
        
        IsSetUp = true;
    }
    
    public uint IncreaseScore(int bonus)
    {
        return (Score = (Score + bonus > 0) ? (uint)(Score + bonus) : 0);
    }

    public uint IncreaseScore(string strBonus)
    {
        if (!m_dictBonus.ContainsKey(strBonus))
            throw new Exception("[" + GetType().Name + "] in IncreaseScore -> Undefined bonus type : " + strBonus);

        return IncreaseScore(m_dictBonus[strBonus]);
    }

    public void Notify(GameManager.EManagerNotif managerNotif)
    {
        if (managerNotif == GameManager.EManagerNotif.SceneChanged)
            IsSetUp = false;
    }
    
}

