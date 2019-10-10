
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class _MGR_UI : MonoBehaviour
{
    private static _MGR_UI p_instance = null;
    public static _MGR_UI Instance { get { return p_instance; } }

    [SerializeField]
    private Text UI_timeLeft;

    [SerializeField]
    private Canvas UI_PauseMenu;


    void Awake()
    {
        if (p_instance == null)
            p_instance = this;
        else if (p_instance != this)
            Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        updateTimeLeft();

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            UI_PauseMenu.gameObject.SetActive(true);
            _MGR_TimeLine.Instance.Pause();
        }
    }

    private void updateTimeLeft()
    {
        int timeLeft = (int)(_MGR_TimeLine.Instance.dureeMax - _MGR_TimeLine.Instance.chrono);

        UI_timeLeft.text = (timeLeft / 60).ToString() + ":" + (timeLeft % 60).ToString();
    }

    public void Resume()
    {
        _MGR_TimeLine.Instance.Resume();
        UI_PauseMenu.gameObject.SetActive(false);
    }



}
