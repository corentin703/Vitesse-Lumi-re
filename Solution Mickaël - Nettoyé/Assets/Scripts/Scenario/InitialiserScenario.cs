using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InitialiserScenario : MonoBehaviour
{
    public Scenario_Etape[] etapes_du_scenario;
    public AudioClip pickingSound;

    //Awake is always called before any Start functions
    void Awake(){
        if (_MGR_ScenarioManager.Instance) {
            _MGR_ScenarioManager.Instance.Configurer(etapes_du_scenario, pickingSound);
            _MGR_ScenarioManager.Instance.Demarrer();
            // pour être robuste, il faudrait synchroniser les différentes intialisations avant de démarer ...
            // e.g. s'assurer que l'initialisations des TL Events est aussi terminée , etc ....
        }
     }

}

