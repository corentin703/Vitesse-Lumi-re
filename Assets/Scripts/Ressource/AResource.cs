using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class AResource : MonoBehaviour
{
    // Create ressource info in your asset menu
    [SerializeField] private SOInfoResource InfoResource;
    
    public string Name { get; private set; }
    public string Description { get; private set; }

    [SerializeField] private uint m_unitNumber = 1;
    public int UnitNumber
    {
        get { return (int)m_unitNumber; }
        private set { m_unitNumber = (uint)value; }
    }

    [SerializeField] private bool m_isPickableByClick = true;
    public bool IsPickableByClick
    {
        get { return m_isPickableByClick; }
        private set { m_isPickableByClick = value; }
    }

    [ConditionalHide("m_isPickableByClick", true)]
    [SerializeField] private float m_pickingClicDistance = 5;
    
    [SerializeField] private bool m_isPickableByObjectPicker = true;
    public bool IsPickableByObjectPicker
    {
        get { return m_isPickableByObjectPicker; }
        private set { m_isPickableByObjectPicker = value; }
    }
    
//  TODO: Define a static var that represents the number of the class's instances for the inventory in ressource's child classes
    public abstract int Number
    {
        get;
        protected set;
    //  TODO: Getter and setter should be defined like that in child classes
    }

// TODO: Child classes MUST use base.Awake() if Awake if redefined
    protected virtual void Awake()
    {
        if (InfoResource == null)
        {
            Name = "Default";
            Description = "Default description";
            
            Debug.LogWarning("[" + GetType().Name + "] in Awake -> InfoResource have not been set, Default have been taken");
        }
        else
        {
            Name = InfoResource.Name;
            Description = InfoResource.Description;
        }

        gameObject.layer = LayerMask.NameToLayer("L_PickableObject");
    }

    private void OnMouseDown()
    {
        if (IsPickableByClick)
        {
            RaycastHit hitInfo = new RaycastHit();

            if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out hitInfo, m_pickingClicDistance, LayerMask.GetMask(LayerMask.LayerToName(gameObject.layer))))
            {
                if (hitInfo.transform.gameObject == gameObject)
                {
                    pick();
                }
            }
        }
    }

    public void PickByObjectPicker()
    {
        if (IsPickableByObjectPicker)
            pick();
    }
    
    private void pick()
    {
        OnPick();
        
        MGR_Resource.Instance.AddItem(this);
    }

    public abstract void Add(int number);

    protected virtual void OnPick()
    {
        gameObject.SetActive(false);
    }

}
