using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using InControl;

public class SlapAttack : MonoBehaviour {

    public GameObject leftOar;
    public GameObject rightOar;
    public GameObject leftSlapper;
    public GameObject rightSlapper;
    public float sizeMult = 2;
    public float slapTime = .5f;

    private bool rightSlap;
    private bool leftSlap;

	// Use this for initialization
	void Start () {
		
	}

    IEnumerator Slap(string side)
    {
        Vector3 startRot;
        Vector3 finalRot;
        GameObject oar;
        GameObject slapper;
        if (side == "left")
        {
            oar = leftOar;
            slapper = leftSlapper;
            startRot = new Vector3(0, 50, 0);
            finalRot = new Vector3(0, -50, 0);
        }
        else
        {
            oar = rightOar;
            slapper = rightSlapper;
            startRot = new Vector3(0, -50, 0);
            finalRot = new Vector3(0, 50, 0);
        }
        Vector3 origRot = oar.transform.localEulerAngles;
        Vector3 origScale = oar.transform.localScale;
        oar.transform.localScale *= sizeMult;
        slapper.GetComponent<Slap>().slapping = true;
        for (float t= 0; t < slapTime; t += Time.deltaTime)
        {
            oar.transform.localEulerAngles = Vector3.Lerp(startRot, finalRot, t / slapTime);
            yield return null;
        }
        oar.transform.localEulerAngles = origRot;
        oar.transform.localScale = origScale;
        if (side == "left")
            leftSlap = false;
        else
            rightSlap = false;
        slapper.GetComponent<Slap>().slapping = false;
        transform.Find("Person").GetComponent<PlayerStatus>().canRow = true;
    }
	
	// Update is called once per frame
	void Update () {
		if (InputManager.ActiveDevice.LeftBumper.WasPressed && !leftSlap)
        {
            leftSlap = true;
            StartCoroutine(Slap("left"));
            transform.Find("Person").GetComponent<PlayerStatus>().canRow = false;
        }
        else if (InputManager.ActiveDevice.RightBumper.WasPressed && !rightSlap)
        {
            rightSlap = true;
            transform.Find("Person").GetComponent<PlayerStatus>().canRow = false;
            StartCoroutine(Slap("right"));
        }
	}
}
