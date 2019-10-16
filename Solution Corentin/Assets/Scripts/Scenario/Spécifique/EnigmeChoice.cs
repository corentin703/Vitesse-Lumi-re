using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//Attention, il reste l'ancien code commenté, je n'ai pas réussis & l'implémenter
public class ChoiceEnigme : Action_Scenario_Etape
{
    private static ChoiceEnigme p_instance = null;              //Static instance of GameManager which allows it to be accessed by any other script.
    public static ChoiceEnigme Instance { get { return p_instance; } }

    [SerializeField]
    private List<Choice> lChoices = new List<Choice>();


    void Awake()
    {
        if (p_instance == null)

            p_instance = this;
        else if (p_instance != this)
            Destroy(gameObject);
    }


    public override void Update()
    {
        return;
    }

    public void Verify(GameObject gameObject)
    {
        foreach (Choice element in lChoices)
        {
            if (element.gameObject == gameObject)
            {
                if (element.isRightChoice)
                {
                    Declencher_Etape_Suivante_Du_Scenario();
                    return;
                }
                else
                {
                    _MGR_TimeLine.Instance.FinDePartie();
                    _MGR_SceneManager.Instance.FinDePartie(_MGR_SceneManager.FIN_DE_PARTIE.PERDU_BAD_CHOICE);

                    return;
                }
            }
        }
    }
}
