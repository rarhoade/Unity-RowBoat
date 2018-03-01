using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class BoatSpawner : MonoBehaviour {
    public GameObject prefabBoat;
    public Camera init;
    private CameraManager cameraManager;
    InputDevice[] devices = new InputDevice[4];
    private int count = 0;

	// Use this for initialization
	void Start ()
    {
        cameraManager = GetComponent<CameraManager>();
        //InputManager.OnActiveDeviceChanged += inputDevice => addNewController(inputDevice);
    }
	
	// Update is called once per frame
	void Update () {
        if (count != 0)
        {
            init.gameObject.SetActive(false);
        }
        InputDevice id = InputManager.ActiveDevice;
        Debug.Log(id.Name);
        if (id != null)
        {
            for (int i = 0; i < count; i++)
            {
                if (devices[i].Equals(id))
                {
                    return;
                }
            }
            addNewController(id);
        }
    }

    void addNewController(InputDevice id)
    {
        Debug.Log("Changed");
        
        GameObject go = Instantiate(prefabBoat);
        devices[count] = id;
        go.GetComponentsInChildren<PlayerInput>()[0].setController(devices[count]);
        count++;
        cameraManager.splitScreen();
    }
}
