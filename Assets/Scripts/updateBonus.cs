using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class updateBonus : MonoBehaviour
{
    GameObject child;

    float timerSetActive;

    void Start()
    {
        child = transform.GetChild(0).gameObject;
    }

    void Update()
    {
        if (!child.activeInHierarchy)
        {
            timerSetActive += Time.deltaTime;
            if (timerSetActive > 20)
            {
                child.SetActive(true);
                timerSetActive = 0;
            }
        }
    }
}
