using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider))]
public class Dalle : MonoBehaviour
{ 
    public enum DalleState
    {
        PRESSED,
        UNPRESSED
    };

    [SerializeField]
    private Material mtl_dallePressed;
    [SerializeField]
    private Material mtl_dalleUnpressed;

    private MeshRenderer m_MeshRenderDalle;
    private DalleState m_currentState;


    public DalleState GetCurrentState()
    {
        return m_currentState;
    }

    public void SetCurrentState(DalleState state)
    {
        if (state == DalleState.PRESSED)
            m_MeshRenderDalle.material = mtl_dallePressed;
        else
            m_MeshRenderDalle.material = mtl_dalleUnpressed;

        m_currentState = state;
    }


    void Start()
    {
        m_MeshRenderDalle = GetComponent<MeshRenderer>();
        m_MeshRenderDalle.material = mtl_dalleUnpressed;
    }

    private void OnTriggerEnter(Collider Collision)
    {
        if (Collision.tag == "Player")
        {
            EnigmeDalle.Instance.Verif(this);
        }
    }

}
