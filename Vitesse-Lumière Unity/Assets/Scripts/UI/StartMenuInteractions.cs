using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenuInteractions : MonoBehaviour
{
    public void StartGame()
    {
        GameManager.Instance.GameStart();
    }
}
