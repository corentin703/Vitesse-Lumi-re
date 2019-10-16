using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INTERACTION_RIGHT_CLICK :  INTERACTION_CLICK_AND_PICK 
{
  // 2ème rédéfinition : on garde la fonction initiale et ne change que le nécessaire 
    override  public void Start()
    {
        base.Start();
        boutton = BOUTTONS.RIGHT;

    }


}
