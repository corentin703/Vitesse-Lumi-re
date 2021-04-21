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

    [SerializeField] bool multipleLines = false;

    [SerializeField]
    string[] FRmulttext;

    [SerializeField]
    string[] ENmulttext;

    void Start()
    {
        if (multipleLines)
        {
            foreach (var txt in FRmulttext)
            {
                FRtext += txt + System.Environment.NewLine;
            }
            foreach (var txt in ENmulttext)
            {
                ENtext += txt + System.Environment.NewLine;
            }
        }
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
