using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCanRow : MonoBehaviour {

    private PlayerStatus ps;
	// Use this for initialization
	void Start () {
        ps = GetComponentInParent<PlayerStatus>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void OnCollisionEnter(Collision collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
            ps.canRow = true;
    }
}
