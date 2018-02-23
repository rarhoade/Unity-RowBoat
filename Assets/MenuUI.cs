using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuUI : MonoBehaviour {

    public GameObject menu;
    public GameObject instructions;
    public GameObject finish;
    public GameObject win;
    public GameObject lose; 

    private bool isHighScore = false;

	// Use this for initialization
	void Start () {
		
	}
	
	// Update is called once per frame
	void Update () {
		if (!GameManager.instance.IsStarted())
        {
            menu.SetActive(true);
            instructions.SetActive(true);
            finish.SetActive(false);
        }
        else if (GameManager.instance.IsFinished())
        {
            menu.SetActive(true);
            instructions.SetActive(false);
            finish.SetActive(true);
            if (isHighScore)
            {
                win.SetActive(true);
                lose.SetActive(false);
            }
            else
            {
                win.SetActive(false);
                lose.SetActive(true);
            }
        }
        else
        {
            menu.SetActive(false);
            isHighScore = false;
        }
	}

    public void HighScoreSet()
    {
        isHighScore = true;
    }

}
