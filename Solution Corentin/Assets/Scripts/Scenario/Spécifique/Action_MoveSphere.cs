using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Action_MoveSphere : Action_Scenario_Etape
{

    private Rigidbody rb;
    private float p_force;
    private int h;

    // Use this for initialization
    override public void Start()
    {
        base.Start();
        rb = this.gameObject.GetComponent<Rigidbody>();
        if (rb == null) rb = this.gameObject.AddComponent<Rigidbody>();
        this.gameObject.GetComponent<Rigidbody>();

        p_force = 1f;
        h = 0;


    }

    // Update is called once per frame
    override public void Update()
    {
        if (h > 0) h--;
        else
        {
            int h = Random.Range(1, 300);
            if (h > 290)
                rb.AddForce(new Vector3(
     0f,
     Random.Range(0f, 3f),
     0f) * p_force, ForceMode.Impulse);

            else
                if (h > 250)
                rb.AddForce(new Vector3(
                    Random.Range(0f, 1f) - 0.5f,
                    0,
                    Random.Range(0f, 1f) - 0.5f) * p_force, ForceMode.Impulse);
        }

    }
}
