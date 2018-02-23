using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Slap : MonoBehaviour {

    public bool slapping = false;
    public float vertSlap = 200;
    public float horSlap = 500;


	// Use this for initialization
	void Start () {
		
	}

    private void OnTriggerEnter(Collider collision)
    {
        if (collision.gameObject.CompareTag("Player") && slapping)
        {
            collision.gameObject.GetComponent<Rigidbody>().AddForce((gameObject.transform.right * horSlap + new Vector3(0, 1, 0) * vertSlap), ForceMode.Impulse);
        }
    }

    // Update is called once per frame
    void Update () {
		
	}
}
