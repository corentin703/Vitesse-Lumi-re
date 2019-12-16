using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CapsuleLoose : AResource
{
    private static int m_Number = 0;

    public override int Number
    {
        get { return m_Number;}
        protected set { m_Number = value; }
    }
    public override void Add(int number)
    {
        Number++;
    }

    protected override void OnPick()
    {
        base.OnPick();
        Debug.Log("[" + GetType().Name + "] Capsule picked !");
        GameManager.Instance.EndGame(GameManager.EndWay.Loose);
    }
}
