using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AnchorSlider : MonoBehaviour {
    public Transform lolz;
    private RectTransform rt;
    public Vector3 offset = new Vector3(0, -65, 0);
	// Use this for initialization
	void Start () {
		if (lolz == null)
        {
            Destroy(gameObject);
        }
        rt = GetComponent<RectTransform>();
	}
	
	// Update is called once per frame
	void Update () {
        Vector3 screenPos = Camera.main.WorldToScreenPoint(lolz.position + offset);
        Vector2 desiredPos = new Vector2(screenPos.x, screenPos.y);
        rt.anchoredPosition = desiredPos;
	}
}
