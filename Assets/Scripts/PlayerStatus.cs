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
    void LateUpdate()
    {
        if (recovering)
        {
            stamina = Mathf.Min(stamina + 10 * Time.fixedDeltaTime, maxStamina);
        }
        else if (!pulledThisFrame)
        {
            stamina = Mathf.Min(stamina + 20 * Time.fixedDeltaTime, maxStamina);
        }
        else
        {
            stamina = Mathf.Min(stamina + 10 * Time.fixedDeltaTime, maxStamina);
            pulledThisFrame = false;
        }
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

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            canRow = true;
    }

    /*public void OnCollisionStay(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            canRow = true;
    }*/

    /*public void OnCollisionExit(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            canRow = false;
    }*/
}
