using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMove : MonoBehaviour
{
    public float max_vel = 15;
    public float turnMult = .5f;
    public float speed_mult = 5;
    

    //private Vector3 right_push = new Vector3(-1, 0, -0.75f);
    //private Vector3 left_push = new Vector3(-1, 0, 0.75f);
    //private float right_vel = 0f;
    //private float left_vel = 0f;

    private PlayerStatus ps;
    private GameObject boat;
    private float decay = 2f;
    //Left is 0, Right is 1
    private Vector3[] vectors = new Vector3[10];
    private float[] vels = new float[10];
    private int count = 0;

    // Use this for initialization
    void Start()
    {
        ps = GetComponent<PlayerStatus>();
        boat = transform.GetChild(2).gameObject;

        vectors[0] = new Vector3(-1, 0, 0.75f);
        vels[0] = 0f;
        vectors[1] = new Vector3(-1, 0, -0.75f);
        vels[1] = 0f;

        GetComponentInParent<PlayerInput>().leftTriggerClicked += LeftPulled;
        GetComponentInParent<PlayerInput>().rightTriggerClicked += RightPulled;
        StartCoroutine(startMove());
    }

    // Update is called once per frame
    void Update()
    {
        transform.GetChild(0).position = boat.transform.position + boat.transform.right * 6 + boat.transform.up * 4.5f;
        transform.GetChild(0).forward = (boat.transform.position + new Vector3(0, 2, 0) - transform.GetChild(0).position).normalized;
    }

    void LeftPulled(float diff, float lastVal)
    {
        if (!ps.recovering && ps.canRow && diff > 0)
        {
            vels[0] = Mathf.Min(vels[0] + (diff) * speed_mult, max_vel);
        }
    }
    void RightPulled(float diff, float lastVal)
    {
        if (!ps.recovering && ps.canRow && diff > 0)
        {
            vels[1] = Mathf.Min(vels[1] + (diff) * speed_mult, max_vel);
        }
    }


    IEnumerator startMove()
    {
        while (true)
        {
            Vector3 rp = Quaternion.AngleAxis(30, boat.transform.up) * -boat.transform.right;
            Vector3 lp = Quaternion.AngleAxis(-30, boat.transform.up) * -boat.transform.right; ;
            boat.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            //before = lolz.transform.right;//Vector3.Cross(lolz.transform.right, lolz.transform.up);
            Vector3 vel = (rp * vels[1] + lp * vels[0]);
            Debug.DrawRay(boat.transform.position, vel * 10, Color.green, 0.1f);
            //Debug.Log("Angle " + Vector3.Angle(-1 * lolz.transform.right, vel));
            if (Mathf.Abs(vels[1]) + Mathf.Abs(vels[0]) > 0.01f)
            {
                if (!GameManager.instance.IsStarted())
                {
                    GameManager.instance.StartRace();
                }
                if (ps.canRow)
                    boat.GetComponent<Rigidbody>().velocity = vel;

                float angle = Vector3.Angle(vel, -1 * boat.transform.right) * Vector3.Magnitude(vel) * Time.fixedDeltaTime / 2 * turnMult;
                if (angle > .1)
                {
                    int mult = 1;
                    if (vels[1] > vels[0])
                    {
                        mult = -1;
                    }
                    if (ps.canRow)
                        boat.transform.Rotate(boat.transform.up, angle * mult);
                }
            }
            if (vels[1] != 0)
            {
                int r_mult = 1;
                if (vels[1] < 0)
                {
                    r_mult = -1;
                }
                //Debug.Log("Eval " + (vels[1] * decay * Time.fixedDeltaTime));
                vels[1] = r_mult * Mathf.Max(Mathf.Abs(vels[1] - (vels[1] * decay * Time.fixedDeltaTime)), 0);
                //Debug.Log("Result " + vels[1]);
            }
            if (vels[0] != 0)
            {
                float l_mult = vels[0] / Mathf.Abs(vels[0]);
                vels[0] = l_mult * Mathf.Max(Mathf.Abs(vels[0] - vels[0] * decay * Time.fixedDeltaTime), 0);
            }
            if (ps.recovering)
            {
                ps.stamina = Mathf.Min(ps.stamina + 10 * Time.fixedDeltaTime, ps.maxStamina);
            }
            else if (!ps.pulledThisFrame)
            {
                ps.stamina = Mathf.Min(ps.stamina + 20 * Time.fixedDeltaTime, ps.maxStamina);
            }
            else
            {
                ps.stamina = Mathf.Min(ps.stamina + 10 * Time.fixedDeltaTime, ps.maxStamina);
                ps.pulledThisFrame = false;
            }
            //Debug.Log("Right Vel " + vels[1]);
            //Debug.Log("Left Vel " + vels[0]);
            Debug.DrawRay(boat.transform.position, rp * 10, Color.red, 0.1f);
            Debug.DrawRay(boat.transform.position, lp * 10, Color.blue, 0.1f);
            yield return new WaitForFixedUpdate();
            //Debug.Log("After " + after);
            //Debug.Log("Before " + before);
        }
        yield break;
    }
}
