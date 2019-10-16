using System.Collections;
using System.Collections.Generic;
using UnityEngine;


// la classe est abstraite car une des méthodes est abstraite , donc non définie , donc classe non instanciable
public abstract class Action_Scenario_Etape : MonoBehaviour {

    private bool etape_suivante_déjà_déclenchée;
    public string descriptionAction;

    [HideInInspector]
    public bool isPartOf_Scenario = false;

    // cette fonction est définie ici à minima et peut être suffisante
    // ou peu nécessiter d'être redéfinie ("overrided") dans la classe fille
    // possibilité de compléter ce code en commencant la méthodes redéfinie par base.Start(); ....
    virtual public void Start ()
    {
        etape_suivante_déjà_déclenchée = false;
    }

    virtual protected void Declencher_PickableSound()
    {
        // Ajoute le son de mise en inventaire
        GameObject pickingSound = new GameObject();
        pickingSound.AddComponent<AudioSource>();
        pickingSound.GetComponent<AudioSource>().clip = _MGR_ScenarioManager.Instance.GetPickingSound();
        pickingSound.GetComponent<AudioSource>().Play();
        pickingSound.name = "PickingSound";

        Destroy(pickingSound, pickingSound.GetComponent<AudioSource>().clip.length);
    }


    // cette fonction ne peut être définie dan cette class mère trop générale
    // elle doit obligatoirement être définie dans les classes filles
    abstract public void Update();


    // cette fonction est définie ici à minima et peut être suffisante
    // ou peu nécessiter d'être redéfinie ("overrided") dans la classe fille
    virtual public void Declencher_Etape_Suivante_Du_Scenario()
    {
        Declencher_PickableSound();

        if (isPartOf_Scenario)
        {
            if (!etape_suivante_déjà_déclenchée)
                _MGR_ScenarioManager.Instance.EtapeSuivante();
            etape_suivante_déjà_déclenchée = true;
        }
    
        //  méthode à dériver dans classes filles si nécessaire de stopper ces comportement par defaut
        // lors des étapes suivantes
        this.enabled = false;   
    }
}
