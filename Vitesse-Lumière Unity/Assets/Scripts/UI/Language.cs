using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Language : MonoBehaviour
{
    [SerializeField]
    string FRtext;

    [SerializeField]
    string ENtext;

    void Start()
    {
        ChangeLanguage();
    }

    void OnEnable()
    {
        ChangeLanguage();
    }

    public void ChangeLanguage()
    {
        if (PlayerPrefs.HasKey("language") && PlayerPrefs.GetString("language") == "FR")
        {
            GetComponent<Text>().text = FRtext;
        }
        else
        {
            GetComponent<Text>().text = ENtext;
        }
    }
}
