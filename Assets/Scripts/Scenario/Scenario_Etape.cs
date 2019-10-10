using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Scenario_Etape {

    public enum Interactions {  UNDEFINED ,
                                PICK_LEFT_CLICK,
                                PICK_RIGHT_CLICK,
                                COLLISION_WITH_PLAYER,
                                KEYBOARD,
                                AUTRE
                              }
    
    public string       nomEtape;
    public GameObject   interactiveObject;      // pour l'instant un seul objet par etape du scénario
                                                // peut être empty GO mais pas null car heberge les scripts Action_Scenario_Etape
    public bool         supprimer_dans_etape_suivante;
    public Interactions interaction;
    public string       messageDescription;

    [HideInInspector]                   // non affiché dans l'IDE 
    public Action_Scenario_Etape[] actions;            // CAR sera complété automatiquement : recherches des components
                                        // dérivés de Action
}
