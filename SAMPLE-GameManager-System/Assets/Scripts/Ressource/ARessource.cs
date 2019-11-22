using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class ARessource : MonoBehaviour
{
    public string Name { get; private set; }
    public string Description { get; private set; }
    
    // Identifier to retrieve infos from MGR_Ressources (if unset, takes the GameObject's tag)
    [SerializeField] private string m_uniqueIdentifier;
    public string UniqueIdentifier
    {
        get { return m_uniqueIdentifier; }
        private set { m_uniqueIdentifier = value; }
    }
    
    [SerializeField] private uint m_unitNumber;
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
    
    [SerializeField] private bool m_isPickableByObjectPicker = true;
    public bool IsPickableByObjectPicker
    {
        get { return m_isPickableByObjectPicker; }
        private set { m_isPickableByObjectPicker = value; }
    }
    
//  TODO: Define a static var that represents the number of the class's instances for the inventory in ressource's child classes
    public abstract bool Number
    {
        get;
        protected set;
//  TODO: Getter and setter should be defined like that in child classes
    }
    
//  TODO: You MUST use this part of declaration in child classes (type base.Awake())
    protected virtual void Awake()
    {
        if (UniqueIdentifier == null)
            UniqueIdentifier = gameObject.tag;

    }
    
    public void Pick()
    {
        OnPick();
        
        MGR_Ressource.Instance.AddItem(this);
    }

    public abstract void Add(int number);

    protected virtual void OnPick()
    {
        
    }

}
