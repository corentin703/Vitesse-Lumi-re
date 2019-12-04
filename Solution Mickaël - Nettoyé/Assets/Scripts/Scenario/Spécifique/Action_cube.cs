using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_cube : Action_Scenario_Etape
{

	// Use this for initialization
	override public void Start () {
        base.Start();
	}
	
	// Update is called once per frame
	override public void Update () {
		if(Input.GetKey(KeyCode.Space))
            Declencher_Etape_Suivante_Du_Scenario();
        if (Input.anyKey && (descriptionAction != ""))
            Debug.Log(descriptionAction);    // en attendant meilleure UI....
    }




    override public void Declencher_Etape_Suivante_Du_Scenario()
    {
        base.Declencher_Etape_Suivante_Du_Scenario();
        this.enabled = true;
    }

}
