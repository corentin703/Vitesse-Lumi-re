using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class Event_TL   {

        
    public string       nom;
    public GameObject   GO;            // le GO qui heberge le script event (peut être empty GO mais pas null !)
    public bool         activer_GO_si_inactif;

    public string       message;

    [HideInInspector]                   // non affiché dans l'IDE 
    public Interface_TL_Events[] actionsEventTL;            // CAR sera complété automatiquement : recherches des components
                                          // classes quelconques qui implémentent l'interface Interface_TL_Events*/

}
