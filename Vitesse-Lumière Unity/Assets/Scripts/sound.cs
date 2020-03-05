using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sound : MonoBehaviour
{
    [SerializeField] AudioSource music;
    [SerializeField] Rigidbody player;
    float playerSpeed;

    void Update()
    {
        playerSpeed = player.velocity.x + player.velocity.y;
        music.volume = 1 - playerSpeed/500;
    }
}
