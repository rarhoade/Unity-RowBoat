using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraManager : MonoBehaviour {

    private Camera[] Cameras = new Camera[4];
    private int count = 0;

	// Use this for initialization
	void Start () {
        GameObject[] players = GameObject.FindGameObjectsWithTag("Player");
        Debug.Log("Got em");
        for (int i = 0; i < players.Length; i++)
        {
            Cameras[i] = players[i].GetComponentInChildren<Camera>();
            if (Cameras[i].isActiveAndEnabled)
            {
                count++;
            }
        }
        Debug.Log("Count " + count);
        //split screen
        switch(count)
        {
            case 2:
                Cameras[0].rect = new Rect(0, .5f, 1, .5f);
                Cameras[1].rect = new Rect(0, 0, 1, .5f);
                break;
            
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}
}
