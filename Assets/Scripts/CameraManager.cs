using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {
    private Camera[] Cameras = new Camera[4];

	// Use this for initialization
	void Start () {
        
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    public void splitScreen()
    {
        //Find all player objects
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("Player Count: " + players.Length);
        int count = 0;
        //Get all cameras
        for (int i = 0; i < players.Length; i++)
        {
            Cameras[i] = players[i].GetComponentInChildren<Camera>();
            if (Cameras[i].isActiveAndEnabled)
            {
                count++;
            }
        }
        Debug.Log("Camera Count: " + count);
        //split screen
        //new Rect(X origin, Y origin, width, height) normalized to BL = (0,0) | TR = (1,1)
        switch (count)
        {
            case 2:
                Cameras[0].rect = new Rect(0, .5f, 1, .5f);
                Cameras[1].rect = new Rect(0, 0, 1, .5f);
                break;
            case 3:
                Cameras[0].rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                Cameras[1].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                Cameras[2].rect = new Rect(0, 0, 0.5f, 0.5f);
                break;
            case 4:
                Cameras[0].rect = new Rect(0, 0.5f, 0.5f, 0.5f);
                Cameras[1].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                Cameras[2].rect = new Rect(0, 0, 0.5f, 0.5f);
                Cameras[3].rect = new Rect(0.5f, 0.5f, 0.5f, 0.5f);
                break;

        }
    }
}
