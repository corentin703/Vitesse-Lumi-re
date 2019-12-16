using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets.Characters.FirstPerson;

public class Init_MGR_Gameplay : MonoBehaviour
{
    public GameObject Player;
    public MGR_Gameplay.SBonus[] Bonus;
    
    void Awake()
    {
        if (MGR_Gameplay.Instance)
            MGR_Gameplay.Instance.SetUp(Player, Bonus);
        
        Destroy(this);
    }
}
