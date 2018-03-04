using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : MonoBehaviour
{

    public float maxStamina = 30;
    public bool recovering = false;
    public bool canRow = true;

    public float stamina = 105;
    public bool pulledThisFrame = false;

    private BoatMove bm;

    // Use this for initialization
    void Start()
    {
        bm = GetComponent<BoatMove>();
        stamina = maxStamina;
        GetComponentInParent<PlayerInput>().leftTriggerClicked += OarPulled;
        GetComponentInParent<PlayerInput>().rightTriggerClicked += OarPulled;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void OarPulled(float diff, float lastVal)
    {
        if (!recovering && canRow)
        {

            if (diff > 0)
            {
                stamina -= Mathf.Min((diff) * bm.speed_mult, bm.max_vel);
                pulledThisFrame = true;
            }
            if (stamina <= 0)
            {
                recovering = true;
                stamina = 0;
            }
        }
        else if (stamina == maxStamina)
        {
            recovering = false;
        }
    }
}
