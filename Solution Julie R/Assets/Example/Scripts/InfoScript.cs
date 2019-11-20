using UnityEngine;
using System.Collections;
///Written by user tyoc213
public class InfoScript : MonoBehaviour {
	GameObject infoPanel;
	//Gamestate reference for quick access
	GameState state;
	// Use this for initialization
	void Start () {
		GameObject canvas = GameObject.Find ("Canvas");
		if (canvas != null) {
			GameObject go = Resources.Load<GameObject>("GameObjects/InfoPanel");
			if(go != null){
				infoPanel = Instantiate(go);
				infoPanel.GetComponent<Transform>().SetParent(canvas.GetComponent<Transform>(), false);
			}
		}
		state = GetComponent<GameState>();
	}

	//just print out a bunch of information onto the screen.
	void Update(){
		string msg = "C : " + state.SpeedOfLight
			+ "\nVitesse Actuelle : " + state.PlayerVelocity
			+ "\n\nTouches :"
            + "\nZ / Q / S / D : déplacements"
            + "\nB : enlever et mettre les couleurs"
            + "\nN/M : Augmenter/Réduire la vitesse";
		UnityEngine.UI.Text text = infoPanel.GetComponentInChildren<UnityEngine.UI.Text> ();
		text.text = msg;
	}

}
