using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TimerUI : MonoBehaviour {
    public Text timer;
    public Text diff;

    private bool flashing = false;
    private bool finished = false;
	// Use this for initialization
	void Start () {
        diff.enabled = false;
        if (PlayerPrefs.HasKey(GameManager.instance.CourseKey + "_" + "1"))
        {
            diff.enabled = true;
            diff.color = Color.gray;
            diff.text = FloatToString(PlayerPrefs.GetFloat(GameManager.instance.CourseKey + "_" + "1"));
        }
        int i = 1;
        while(PlayerPrefs.HasKey(GameManager.instance.CourseKey + "_" + i))
        {
            //Debug.Log(i + " " + FloatToString(PlayerPrefs.GetFloat(GameManager.instance.CourseKey + "_" + i)));
            i++;
        }
	}
	
	// Update is called once per frame
	void Update () {
		if (GameManager.instance.IsStarted() && !flashing && !finished)
        {
            float timePassed = GameManager.instance.TimeFromStart();
            timer.text = FloatToString(timePassed);
        }
	}

    public void ResetGame()
    {
        finished = false;
    }

    public void Finished()
    {
        finished = true;
        DiffUpdate();
    }

    public void Checkpointed(int checkpoint_num)
    {
        StartCoroutine(Checkpoint(checkpoint_num));
    }

    private IEnumerator Checkpoint(int checkpoint_num)
    {
        DiffUpdate(checkpoint_num);
        flashing = true;
        for (int i = 0; i < 3; i++)
        {
            timer.enabled = false;
            yield return new WaitForSecondsRealtime(0.1f);
            timer.enabled = true;
            yield return new WaitForSecondsRealtime(0.1f);
        }
        flashing = false;
        DiffReset(checkpoint_num + 1);
    }

    private void DiffUpdate(int checkpoint_num = -1)
    {
        float timeDelt;
        string key = GameManager.instance.CourseKey + "_" + checkpoint_num.ToString();
        if (checkpoint_num == -1)
        {
            key = GameManager.instance.CourseKey + "_record";
        }
        if (PlayerPrefs.HasKey(key))
        {
            diff.enabled = true;
            timeDelt = PlayerPrefs.GetFloat(key) - GameManager.instance.GetTime(checkpoint_num);
            if (timeDelt < 0)
            {
                diff.text = "+";
                diff.color = Color.red;
            }
            else
            {
                diff.text = "-";
                diff.color = Color.green;
            }
            diff.text += FloatToString(Mathf.Abs(timeDelt));
        }
        else
        {
            diff.enabled = false;
        }
    }

    private void DiffReset(int checkpoint_target)
    {
        if (PlayerPrefs.HasKey(GameManager.instance.CourseKey + "_" + (checkpoint_target).ToString()))
        {
            diff.text = FloatToString(PlayerPrefs.GetFloat(GameManager.instance.CourseKey + "_" + (checkpoint_target).ToString()));
            diff.color = Color.gray;
        }
    }

    private string FloatToString(float timePassed)
    {
        string ret = "";

        if (timePassed / 60 >= 10)
            ret += ((int)timePassed / 60);
        else if (timePassed / 60 >= 1)
        {
            ret += "0" + ((int)timePassed / 60);
        }
        else
            ret += "00";
        ret += ":";

        if (timePassed % 60 >= 10)
            ret += ((int)timePassed % 60);
        else if (timePassed % 60 >= 1)
        {
            ret += "0" + ((int)timePassed % 60);
        }
        else
            ret += "00";
        ret += ":";

        int singles = (int)(timePassed * 100) % 100;
        if (singles < 10)
        {
            ret += "0";
        }
        ret += singles;
        //Debug.Log(singles);
        //Debug.Log(ret);
        return ret;
    }
}
