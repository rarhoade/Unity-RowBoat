﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour {

    public Slider slider;
    public Image bg;
    public PlayerInput pi;

	// Use this for initialization
	void Start () {
        slider.maxValue = pi.maxStamina;
        slider.value = pi.maxStamina;
	}
	
	// Update is called once per frame
	void Update () {
        slider.value = pi.GetStamina();
        if (pi.recovering)
        {
            bg.color = Color.red;
        }
        else
        {
            bg.color = Color.green;
        }
	}
}
