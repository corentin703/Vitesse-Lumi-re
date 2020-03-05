using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class jaugeVitesse : MonoBehaviour
{
    float minAngle = -50;
    float maxAngle = 50;
    float angle = -50;
    [SerializeField] GameObject player;
    [SerializeField] GameObject curseur;
    float playerSpeed;

    void Update()
    {
        playerSpeed = System.Math.Abs(player.GetComponent<Rigidbody>().velocity.x) + System.Math.Abs(player.GetComponent<Rigidbody>().velocity.z);
        angle = (playerSpeed / 5000.0f * 50.0f) - 50;
        if (angle > maxAngle) angle = maxAngle;
        else if (angle < minAngle) angle = minAngle;
        curseur.transform.rotation = Quaternion.Euler(0, 0, -angle);
        print(playerSpeed);

    }
}
