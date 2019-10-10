using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnigmePieces : Action_PickObjectList
{
    private static EnigmePieces p_instance = null;
    public static EnigmePieces Instance { get { return p_instance; } }

    public void Awake()
    {
        if (p_instance == null)
            p_instance = this;
        else if (p_instance != this)
            Destroy(gameObject);
    }
}
