using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class CommonDevTools
{
    public static bool debugging = Debug.isDebugBuild;
    //In the Build Settings dialog there is a check box called "Development Build".
    // If it is checked isDebugBuild will be true. 
    // In the editor isDebugBuild always returns true. 
    // It is recommended to remove all calls to Debug.Log when deploying a game, this way you can easily deploy beta builds with debug prints and final builds without.

    // lors du déploiement final , facilité pour supprimer tous les appels à ces fonctions sauf QUIT_APP 

    public static void DEBUG(string __err)
    {
        if (debugging)
            Debug.Log(__err);

    }

    public static void WARNING(string __err, bool __force_pause = false)
    {
        if (debugging)
            Debug.Log(__err);
        if (__force_pause)
            Debug.Break();
    }
    public static void ERROR(string __err, GameObject __go)
    {
        if (debugging)
           // Debug.LogError(__err,__go);
            Debug.LogException(new System.Exception(__err));
        // astuce : LogError et LogException arrête (temporairement l'excution SI ErrorPause est coché dans la fenêtre console 
        // tous les deux apportent une information sur l'objet concerné (auto pour exception, argument pour LogError)

    }

    public static void QUIT_APP(string __err)
    {
        if (Application.isEditor) {
            // Only break in editor to allow examination of the current scene state.
            WARNING(__err);
            Debug.Break();
        }
        else {
            // There's no standard way to return an error code to the OS,
            // so just quit regularly.
            Application.Quit();
        }
    }  
}
