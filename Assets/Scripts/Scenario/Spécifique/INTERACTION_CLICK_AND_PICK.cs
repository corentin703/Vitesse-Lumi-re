using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class INTERACTION_CLICK_AND_PICK : Action_Scenario_Etape
{
    protected enum BOUTTONS { LEFT, RIGHT, MIDDLE }
    protected enum ACTIONS_BOUTTON { APPUI, RELACHE, MAINTENU }

    [SerializeField]
    protected BOUTTONS boutton;

    protected ACTIONS_BOUTTON action_boutton;
    protected bool action_detectee;

    [SerializeField]
    protected float pickDistance = 5f;   // max distance of ray cast

    protected int layer_mask;   // seuls les objets de ces layers seront testés pour l'intersection au raycast

    private GameObject temp_GO;

    // cette fonction est définie ici à minima et peut être suffisante
    // ou peu nécessiter d'être à nouveau redéfinie ("overrided") dans la classe fille
    // possibilité de compléter ce code en commencant la méthodes redéfinie par base.Start(); ....
    override  public void Start()
    {
        base.Start();
        //boutton = BOUTTONS.LEFT;
        action_boutton = ACTIONS_BOUTTON.APPUI;
        action_detectee = false;

        layer_mask = LayerMask.GetMask("Pickable");   // ou GetMask("L1", "L2", ...); si plusieurs layers
    }


    virtual public void Object_Picked(GameObject GO)
    {
        Declencher_Etape_Suivante_Du_Scenario();
    }


    virtual public bool  test_Mouse_Picking(out GameObject GO)
    {
        RaycastHit hitInfo = new RaycastHit();
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, pickDistance, layer_mask))
        {
            if (hitInfo.transform.parent.gameObject.name == this.gameObject.name)
            {
                GO = hitInfo.transform.gameObject;
                return true;
            }
        }
        GO = null;
        return false;
    }


    // Update is called once per frame
    override public void Update()
    {
        switch (action_boutton)
        {
            case ACTIONS_BOUTTON.APPUI:
                action_detectee = Input.GetMouseButtonDown((int)boutton);
                break;
            case ACTIONS_BOUTTON.RELACHE:
                action_detectee = Input.GetMouseButtonUp((int)boutton);
                break;
            case ACTIONS_BOUTTON.MAINTENU:
                action_detectee = Input.GetMouseButton((int)boutton);
                break;
        }

        if (action_detectee)
        {
            if (test_Mouse_Picking(out temp_GO))
            {
                Debug.Log("Clic sur " + temp_GO.name);
                Object_Picked(temp_GO);
            }
            else if (descriptionAction != "")
                CommonDevTools.DEBUG(descriptionAction);    // en attendant meilleure UI....
        }
    }
}

