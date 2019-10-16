using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// tous les évenements qui seront gérés par la TimeLine du scénario devront pouvoir répondre à ces fonctions
// il peut s'agir de sons, de lumières ou d'objets, de caméras, etc ....
// leur fonctionnement est autonome mais ils sont pilotés par la timeline du scénario
// s'il est nécessaire de toutes les définir {} , il n'est pas oligatoire de fournir du code pour chacune 
public interface Interface_TL_Events   {

    float getStartTime_TL_Event();   // retourne à quel moment doit se déclencher l'évenement
    float getStopTime_TL_Event();   // retourne à quel moment doit se déclencher l'évenement
    float getDuration_TL_Event();    // retourne la durée de l'évenement
    bool  isPerdiodic_TL_Event(out float period);    // retourne si événement périodique et la période


    void start_TL_Event();                              // démarer l'évenement
    void stop_TL_Event();                                // arrêter l'événement
    void restart_TL_Event();                            // ré initialiser et redémarer  l'évenement

    void pause_TL_Event();                              // mettre ne pause l'évenement
    void randomize_TL_Event();

    bool isRandomizable_TL_Event();                     // l'événement peut il être déclenché de façon aléatoire ?
    bool isPausable_TL_Event();                         // l'événement peut il être mis en pause ?

    void TL_ChronoDemarre();                            // le manager indique aux objets TL Event les actions sur la TL
    void TL_ChronoEnPause();
    void TL_ChronoReprise();
    void TL_ChronoArrete();

}
