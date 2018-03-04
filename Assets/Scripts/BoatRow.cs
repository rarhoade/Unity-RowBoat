using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;

public class BoatRow : MonoBehaviour {
    public GameObject lolz;
    public GameObject rightBagette;
    public GameObject leftBagette;
    public GameObject body;
    public GameObject boat;

    public float raise_speed;
    public float feather_speed;

    public float min_RO;
    private float range_RO;
    
    public float min_LO;
    private float range_LO;

    private Vector3 right_push = new Vector3(-1, 0, -0.75f);
    private Vector3 left_push = new Vector3(-1, 0, 0.75f);
    private float right_vel = 0f;
    private float left_vel = 0f;
    public float max_vel = 15;
    public float max_turn = 10;
    public float speed_mult = 5;
    private float decay = 2f;

    private PlayerStatus ps;
    

	// Use this for initialization
	void Start () {
        ps = GetComponentInParent<PlayerStatus>();

        range_RO = -2 * min_RO;
        range_LO = -2 * min_LO;
        GetComponentInParent<PlayerInput>().rightTriggerClicked += RightOarAnimation;
        GetComponentInParent<PlayerInput>().leftTriggerClicked += LeftOarAnimation;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (boat.transform.position.y < -3)
        {
            GameManager.instance.ResetRace();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
    }

    void RightOarAnimation(float diff, float right)
    {
        if (!ps.recovering && ps.canRow)
        {
            Time.timeScale = 1;
            Vector3 local = rightBagette.transform.localEulerAngles;
            float locX = local.x;
            if (Mathf.Abs(locX - 360) < locX)
            {
                locX -= 360;
            }
            float locZ = local.z;
            if (diff >= 0)
            {
                rightBagette.transform.localEulerAngles = new Vector3(Mathf.Min(locX + right * raise_speed * 2, 15), min_RO + range_RO * right, Mathf.Max(locZ - right * feather_speed * 2, 0));
            }
            else
            {
                rightBagette.transform.localEulerAngles = new Vector3(Mathf.Max(locX - (1 - right) * raise_speed, -5), min_RO + range_RO * right, Mathf.Min(locZ + (1 - right) * feather_speed, 70));
            }
        }
    }

    void LeftOarAnimation(float diff, float left)
    {
        if (!ps.recovering && ps.canRow)
        {
            Time.timeScale = 1;
            Vector3 local = leftBagette.transform.localEulerAngles;
            float locX = local.x;
            if (Mathf.Abs(locX - 360) < locX)
            {
                locX -= 360;
            }
            float locZ = local.z;

            if (diff >= 0)
            {
                leftBagette.transform.localEulerAngles = new Vector3(Mathf.Max(locX - left * raise_speed * 2, -15), min_LO + range_LO * left, Mathf.Max(locZ - left * feather_speed * 2, 0));
            }
            else
            {
                leftBagette.transform.localEulerAngles = new Vector3(Mathf.Min(locX + (1 - left) * raise_speed * 2, 5), min_LO + range_LO * left, Mathf.Min(locZ + (1 - left) * feather_speed, 70));
            }
        }
    }


//    if (!recovering && canRow)
//            {
//                Time.timeScale = 1;
//                if (Mathf.Abs(right - lastRight) > 0.01)// && rightArm != null)
//                {
//                    //rightOar.transform.localEulerAngles = new Vector3(rightOar.transform.localEulerAngles.x, min_RO + range_RO * right, rightOar.transform.localEulerAngles.z);
//                    Vector3 local = rightBagette.transform.localEulerAngles;
//    float locX = local.x;
//                    if (Mathf.Abs(locX - 360) < locX)
//                    {
//                        locX -= 360;
//                    }
//float locZ = local.z;
//                    if (right > lastRight)
//                    {
//                        rightBagette.transform.localEulerAngles = new Vector3(Mathf.Min(locX + right* raise_speed * 2, 15), min_RO + range_RO* right, Mathf.Max(locZ - right* feather_speed * 2, 0));
//                    }
//                    else
//                    {
//                        rightBagette.transform.localEulerAngles = new Vector3(Mathf.Max(locX - (1 - right) * raise_speed, -5), min_RO + range_RO* right, Mathf.Min(locZ + (1 - right) * feather_speed, 70));
//                    }
//                    if (right - lastRight > 0)
//                    {
//                        stamina -= Mathf.Min((right - lastRight) * speed_mult, max_vel);
//                        right_vel = Mathf.Min(right_vel + (right - lastRight) * speed_mult, max_vel);
//                        pulledThisFrame = true;
//                    }
//                }
//                if (Mathf.Abs(left - lastLeft) > 0.01)// && leftArm != null)
//                {
//                    //leftOar.transform.localEulerAngles = new Vector3(leftOar.transform.localEulerAngles.x, min_LO + range_LO * left, leftOar.transform.localEulerAngles.z);
//                    Vector3 local = leftBagette.transform.localEulerAngles;
//float locX = local.x;
//                    if (Mathf.Abs(locX - 360) < locX)
//                    {
//                        locX -= 360;
//                    }
//                    float locZ = local.z;

//                    if (left > lastLeft)
//                    {
//                        leftBagette.transform.localEulerAngles = new Vector3(Mathf.Max(locX - left* raise_speed * 2, -15), min_LO + range_LO* left, Mathf.Max(locZ - left* feather_speed * 2, 0));
//                    }
//                    else
//                    {
//                        leftBagette.transform.localEulerAngles = new Vector3(Mathf.Min(locX + (1 - left) * raise_speed * 2, 5), min_LO + range_LO* left, Mathf.Min(locZ + (1 - left) * feather_speed, 70));
//                    }
//                    if (left - lastLeft > 0)
//                    {
//                        stamina -= Mathf.Min((left - lastLeft) * speed_mult, max_vel);
//                        left_vel = Mathf.Min(left_vel + (left - lastLeft) * speed_mult, max_vel);
//                        pulledThisFrame = true;
//                    }
//                }
//                lastRight = right;
//                lastLeft = left;
//                if (stamina <= 0)
//                {
//                    recovering = true;
//                    stamina = 0;
//                }
//            }
//            else if (stamina == maxStamina) 
//            {
//                recovering = false;
//            }



    //void FixedUpdate()
    //{
    //    InputDevice device = InputManager.ActiveDevice;
    //    InputControl rightTrigger = device.GetControl(InputControlType.RightTrigger);
    //    InputControl rightJoystickX = device.GetControl(InputControlType.RightStickX);
    //    InputControl rightJoystickY = device.GetControl(InputControlType.RightStickY);
    //    InputControl leftTrigger = device.GetControl(InputControlType.LeftTrigger);
    //    InputControl restart = device.GetControl(InputControlType.Action1);

    //    if (restart.IsPressed || boat.transform.position.y < -3)
    //    {
    //        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    //    }

    //    float rightX = rightJoystickX.Value;
    //    float rightY = rightJoystickY.Value;
    //    float left = leftTrigger.LastValue;
    //    if (true)// && rightArm != null)
    //    {
    //        //rightOar.transform.localEulerAngles = new Vector3(rightOar.transform.localEulerAngles.x, min_RO + range_RO * right, rightOar.transform.localEulerAngles.z);
    //        float targetX = rightBagette.transform.localEulerAngles.x;
    //        float targetY = rightBagette.transform.localEulerAngles.y;
    //        if (Mathf.Abs(rightX) > 0.2f )
    //        {
    //            if (rightX > 0)
    //            {
    //                targetX = 16f;
    //            }
    //            else if (rightX < 0)
    //            {
    //                targetX = -5;
    //            }
    //            else
    //            {
    //                targetX = 0;
    //            }
    //        }
    //        if (Mathf.Abs(rightY) < 0.5f && Mathf.Abs(rightY - lastRightY) > 0.2f)
    //        {
    //            targetY = -1 * rightY / 0.5f * 35f;
    //            //Debug.Log(Mathf.Pow(rightX, 2) + Mathf.Pow(rightY, 2) + " " + targetX);
    //            if (targetX > 0 && (Mathf.Pow(rightX, 2) + Mathf.Pow(rightY, 2) >= .99f))
    //            {
    //                //Debug.Log("In?");
    //                int multt = -1;
    //                float temp = right_vel + (rightY - lastRightY) * speed_mult;
    //                if (temp < 0)
    //                {
    //                    multt = 1;
    //                }
    //                right_vel = Mathf.Min(Mathf.Abs(temp), max_vel) * multt;
    //            }
    //        }

    //        rightBagette.transform.localEulerAngles = new Vector3(targetX, targetY, rightBagette.transform.localEulerAngles.z);



    //        //if (right - lastRight > 0)
    //        //{
    //        //    right_vel = Mathf.Min(right_vel + (right - lastRight) * speed_mult, max_vel);
    //        //}
    //    }
    //    if (Mathf.Abs(left - lastLeft) > 0.01)// && leftArm != null)
    //    {
    //        leftOar.transform.localEulerAngles = new Vector3(leftOar.transform.localEulerAngles.x, min_LO + range_LO * left, leftOar.transform.localEulerAngles.z);
    //        if (left - lastLeft > 0)
    //        {
    //            left_vel = Mathf.Min(left_vel + (left - lastLeft) * speed_mult, max_vel);
    //        }
    //    }
    //    lastRightX = rightX;
    //    lastRightY = rightY;
    //    lastLeft = left;
    //}

    //IEnumerator startMove()
    //{
    //    Vector3 rp = right_push;
    //    Vector3 lp = left_push;
    //    Vector3 before = lolz.transform.right;//Vector3.Cross(lolz.transform.right, lolz.transform.up);
    //    Vector3 after = lolz.transform.right;//Vector3.Cross(lolz.transform.right, lolz.transform.up);
    //    while (true)
    //    {
    //        //before = lolz.transform.right;//Vector3.Cross(lolz.transform.right, lolz.transform.up);
    //        //Debug.Log("WHAT THE FUCK IS VEL " + right_vel);
    //        Vector3 vel = (rp * right_vel + lp * left_vel);
    //        Debug.DrawRay(lolz.transform.position, vel * 10, Color.green, 0.1f);
    //        //Debug.Log("Angle " + Vector3.Angle(-1 * lolz.transform.right, vel));
    //        if (Mathf.Abs(right_vel) + Mathf.Abs(left_vel) > 0.01f)
    //        {
    //            if (!GameManager.instance.IsStarted())
    //            {
    //                GameManager.instance.StartRace();
    //            }
    //            lolz.transform.position = Vector3.Lerp(lolz.transform.position, lolz.transform.position + vel, Time.fixedDeltaTime*3);

    //            float angle = Vector3.Angle(vel, -1 * lolz.transform.right) * Vector3.Magnitude(vel) * Time.fixedDeltaTime / 2;
    //            if (angle > .1)
    //            {
    //                int mult = 1;
    //                if (right_vel > left_vel)
    //                {
    //                    mult = -1;
    //                }
    //                lolz.transform.Rotate(lolz.transform.up,angle * mult);
    //            }
    //        }
    //        if (right_vel != 0)
    //        {
    //            int r_mult = 1;
    //            if (right_vel < 0)
    //            {
    //                r_mult = -1;
    //            }
    //            //Debug.Log("Eval " + (right_vel * decay * Time.fixedDeltaTime));
    //            right_vel = r_mult * Mathf.Max(Mathf.Abs(right_vel - (right_vel * decay * Time.fixedDeltaTime)), 0);
    //            //Debug.Log("Result " + right_vel);
    //        }
    //        if (left_vel != 0)
    //        {
    //            float l_mult = left_vel / Mathf.Abs(left_vel);
    //            left_vel = l_mult * Mathf.Max(Mathf.Abs(left_vel - left_vel * decay * Time.fixedDeltaTime), 0);
    //        }
    //        if (recovering)
    //        {
    //            stamina = Mathf.Min(stamina + 10 * Time.fixedDeltaTime, maxStamina);
    //        }
    //        else if (!pulledThisFrame)
    //        {
    //            stamina = Mathf.Min(stamina + 20 * Time.fixedDeltaTime, maxStamina);
    //        }
    //        else
    //        {
    //            stamina = Mathf.Min(stamina + 10 * Time.fixedDeltaTime, maxStamina);
    //            pulledThisFrame = false;
    //        }
    //        //Debug.Log("Right Vel " + right_vel);
    //        //Debug.Log("Left Vel " + left_vel);
    //        Debug.DrawRay(lolz.transform.position, rp*10, Color.red, 0.1f);
    //        Debug.DrawRay(lolz.transform.position, lp * 10, Color.blue, 0.1f);
    //        yield return new WaitForFixedUpdate();
    //        //Debug.Log("After " + after);
    //        //Debug.Log("Before " + before);
    //        after = lolz.transform.right;//Vector3.Cross(lolz.transform.right, lolz.transform.up);
    //        rp = Quaternion.FromToRotation(before, after) * right_push;
    //        lp = Quaternion.FromToRotation(before, after) * left_push;
    //    }
    //    //yield break;
    //}
}
