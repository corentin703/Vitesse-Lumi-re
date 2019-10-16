using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Coin : MonoBehaviour
{
    private void OnTriggerEnter(Collider Collision)
    {
        if (Collision.tag == "Player")
        {
            EnigmePieces.Instance.Object_Picked(gameObject);
        }
    }
}
