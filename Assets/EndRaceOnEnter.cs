using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndRaceOnEnter : MonoBehaviour {

    public GameManager endGameCall;
	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnCollisionEnter(Collision collision)
    {
        if(collision.gameObject.tag == "Boat")
        {
            Debug.Log("beinghit");
            endGameCall.GetComponent<GameManager>().Finish();
        }
    }
}
