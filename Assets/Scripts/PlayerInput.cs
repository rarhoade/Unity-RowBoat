using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;
using UnityEngine.SceneManagement;

public class PlayerInput : MonoBehaviour {
    InputDevice device;
    public int deviceNum;

    public delegate void triggerClicked(float diff, float endLoc);
    public triggerClicked rightTriggerClicked;
    public triggerClicked leftTriggerClicked;

    private float lastRight = 0f;
    private float lastLeft = 0f;

    private PlayerStatus ps;


    public void setController(InputDevice d)
    {
        device = d;
        deviceNum = InputManager.Devices.IndexOf(d);
    }

	// Use this for initialization
	void Start () {
        ps = GetComponent<PlayerStatus>();
        if (deviceNum >= 0 && InputManager.Devices.Count >= deviceNum)
        {
            device = InputManager.Devices[deviceNum];
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (device == null && deviceNum < 0)
        {
            device = InputManager.ActiveDevice;
        }
        InputControl rightTrigger = device.GetControl(InputControlType.RightTrigger);
        InputControl leftTrigger = device.GetControl(InputControlType.LeftTrigger);
        InputControl restart = device.GetControl(InputControlType.Start);
        InputControl resetHighScores = device.GetControl(InputControlType.Back);

        if (resetHighScores.IsPressed)
        {
            PlayerPrefs.DeleteAll();
        }
        if (restart.IsPressed)
        {
            GameManager.instance.ResetRace();
            SceneManager.LoadScene(SceneManager.GetActiveScene().name);
        }
        if (!GameManager.instance.IsFinished())
        {
            float right = rightTrigger.LastValue;
            float left = leftTrigger.LastValue;
            if (Mathf.Abs(right - lastRight) > 0.0001)
            {
                rightTriggerClicked(right - lastRight, right);
            }
            if (Mathf.Abs(left - lastLeft) > 0.0001)
            {
                leftTriggerClicked(left - lastLeft, left);
            }
            lastRight = right;
            lastLeft = left;
        }

    }
}
