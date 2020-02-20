using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bonus : MonoBehaviour
{
    void OnTriggerEnter(Collider collid)
    {
        if(collid.tag == "Playermesh")
        {
            gameObject.SetActive(false);
            collid.transform.GetChild(0).gameObject.GetComponent<GameState>().PlayerSpeed += 20;
        }
    }
}
