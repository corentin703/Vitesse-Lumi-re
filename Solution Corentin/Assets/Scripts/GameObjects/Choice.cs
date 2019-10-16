using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Choice : MonoBehaviour
{
    public bool isRightChoice;

    void OnTriggerEnter(Collider other)
    {
        if (other.tag == "Player")
        {
            ChoiceEnigme.Instance.Verify(gameObject);
        }
    }
}
