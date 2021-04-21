using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class changePlayerSpeed : MonoBehaviour
{
    [SerializeField] Slider slider;
    [SerializeField] GameObject player;

    void Update()
    {
        if(Input.mouseScrollDelta.y > 0) slider.value++;
        else if(Input.mouseScrollDelta.y < 0) slider.value--;
    }

    public void changeSpeed()
    {
        player.GetComponent<GameState>().PlayerSpeed = (int)slider.value;
    }
}
