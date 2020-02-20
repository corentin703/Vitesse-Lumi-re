using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class wrongWayManager : MonoBehaviour
{

    [SerializeField] GameObject player;
    [SerializeField] GameObject start;
    Vector2 vectZeroPlayer;
    Vector2 vectZeroStart;
    float lastAngle = 0;
    float newAngle = 0;

    [SerializeField] GameObject warningCanvas;
    
    void Update()
    {
        vectZeroStart = new Vector2((start.transform.position.x - transform.position.x), (start.transform.position.z - transform.position.z));
        vectZeroPlayer = new Vector2((player.transform.position.x - transform.position.x), (player.transform.position.z - transform.position.z));
        newAngle = Vector2.SignedAngle(vectZeroStart, vectZeroPlayer);
        if (newAngle < lastAngle - 5) warningCanvas.SetActive(true);
        else warningCanvas.SetActive(false);
        if (warningCanvas.activeInHierarchy && newAngle < lastAngle - 10) lastAngle = newAngle + 6;
        if (newAngle > lastAngle || newAngle > 178 || newAngle < -178) lastAngle = newAngle;
    }

    private float AngleBetweenVector2(Vector2 vec1, Vector2 vec2)
    {
        Vector2 diference = vec2 - vec1;
        float sign = (vec2.y < vec1.y) ? -1.0f : 1.0f;
        return Vector2.Angle(Vector2.right, diference) * sign;
    }

}
