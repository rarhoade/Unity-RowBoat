using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckpointReporter : MonoBehaviour {

    RequireComponent Collider;

    public MeshRenderer right;
    public MeshRenderer left;
    public Material finish;

    private int checkpointNumber = 1;
    private bool isFinishLine = false;

	// Use this for initialization
	void Start () {
        checkpointNumber = transform.parent.transform.GetSiblingIndex()+1;
        isFinishLine = transform.parent.transform.parent.childCount == (checkpointNumber);
        if (isFinishLine)
        {
            right.material = finish;
            left.material = finish;
        }
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "WALL")
        {
            Debug.Log("ChkNum: " + checkpointNumber);
            Debug.Log("Parent's Child Count: " + transform.parent.transform.parent.childCount);
            if (!isFinishLine)
            {
                GameManager.instance.CheckedIn(checkpointNumber);
            }
            else
            {
                GameManager.instance.Finish();
            }
        }
    }
}
