using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoatMove : MonoBehaviour
{
    public GameObject lolz;
    public float max_vel = 15;
    public float max_turn = 10;
    public float speed_mult = 5;

    //private Vector3 right_push = new Vector3(-1, 0, -0.75f);
    //private Vector3 left_push = new Vector3(-1, 0, 0.75f);
    //private float right_vel = 0f;
    //private float left_vel = 0f;

    private PlayerStatus ps;
    private float decay = 2f;
    //Left is 0, Right is 1
    private Vector3[] vectors = new Vector3[10];
    private float[] vels = new float[10];
    private int count = 0;

    // Use this for initialization
    void Start()
    {
        ps = GetComponent<PlayerStatus>();

        vectors[0] = new Vector3(-1, 0, 0.75f);
        vels[0] = 0f;
        vectors[1] = new Vector3(-1, 0, -0.75f);
        vels[1] = 0f;

        GetComponentInParent<PlayerInput>().leftTriggerClicked += LeftPulled;
        GetComponentInParent<PlayerInput>().rightTriggerClicked += RightPulled;
    }

    // Update is called once per frame
    void Update()
    {

    }

    void LeftPulled(float diff, float lastVal)
    {
        if (!ps.recovering && ps.canRow)
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
    //IEnumerator startMove()
    //{
    //    Vector3 rp = vectors[1];
    //    Vector3 lp = vectors[0];
    //    Vector3 before = lolz.transform.right;//Vector3.Cross(lolz.transform.right, lolz.transform.up);
    //    Vector3 after = lolz.transform.right;//Vector3.Cross(lolz.transform.right, lolz.transform.up);
    //    while (true)
    //    {
    //        //before = lolz.transform.right;//Vector3.Cross(lolz.transform.right, lolz.transform.up);
    //        //Debug.Log("WHAT THE FUCK IS VEL " + vels[1]);
    //        Vector3 vel = (rp * vels[1] + lp * vels[0]);
    //        Debug.DrawRay(lolz.transform.position, vel * 10, Color.green, 0.1f);
    //        //Debug.Log("Angle " + Vector3.Angle(-1 * lolz.transform.right, vel));
    //        if (Mathf.Abs(vels[1]) + Mathf.Abs(vels[0]) > 0.01f)
    //        {
    //            if (!GameManager.instance.IsStarted())
    //            {
    //                GameManager.instance.StartRace();
    //            }
    //            lolz.transform.position = Vector3.Lerp(lolz.transform.position, lolz.transform.position + vel, Time.fixedDeltaTime * 3);

    //            float angle = Vector3.Angle(vel, -1 * lolz.transform.right) * Vector3.Magnitude(vel) * Time.fixedDeltaTime / 2;
    //            if (angle > .1)
    //            {
    //                int mult = 1;
    //                if (vels[1] > vels[0])
    //                {
    //                    mult = -1;
    //                }
    //                lolz.transform.Rotate(lolz.transform.up, angle * mult);
    //            }
    //        }
    //        if (vels[1] != 0)
    //        {
    //            int r_mult = 1;
    //            if (vels[1] < 0)
    //            {
    //                r_mult = -1;
    //            }
    //            //Debug.Log("Eval " + (vels[1] * decay * Time.fixedDeltaTime));
    //            vels[1] = r_mult * Mathf.Max(Mathf.Abs(vels[1] - (vels[1] * decay * Time.fixedDeltaTime)), 0);
    //            //Debug.Log("Result " + vels[1]);
    //        }
    //        if (vels[0] != 0)
    //        {
    //            float l_mult = vels[0] / Mathf.Abs(vels[0]);
    //            vels[0] = l_mult * Mathf.Max(Mathf.Abs(vels[0] - vels[0] * decay * Time.fixedDeltaTime), 0);
    //        }
    //        if (ps.recovering)
    //        {
    //            ps.stamina = Mathf.Min(ps.stamina + 10 * Time.fixedDeltaTime, ps.maxStamina);
    //        }
    //        else if (!ps.pulledThisFrame)
    //        {
    //            ps.stamina = Mathf.Min(ps.stamina + 20 * Time.fixedDeltaTime, ps.maxStamina);
    //        }
    //        else
    //        {
    //            ps.stamina = Mathf.Min(ps.stamina + 10 * Time.fixedDeltaTime, ps.maxStamina);
    //            ps.pulledThisFrame = false;
    //        }
    //        //Debug.Log("Right Vel " + vels[1]);
    //        //Debug.Log("Left Vel " + vels[0]);
    //        Debug.DrawRay(lolz.transform.position, rp * 10, Color.red, 0.1f);
    //        Debug.DrawRay(lolz.transform.position, lp * 10, Color.blue, 0.1f);
    //        yield return new WaitForFixedUpdate();
    //        //Debug.Log("After " + after);
    //        //Debug.Log("Before " + before);
    //        after = lolz.transform.right;//Vector3.Cross(lolz.transform.right, lolz.transform.up);
    //        rp = Quaternion.FromToRotation(before, after) * vectors[1];
    //        lp = Quaternion.FromToRotation(before, after) * vectors[0];
    //    }
    //    //yield break;
    //}
}
