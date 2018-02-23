using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    public static GameManager instance = null;
    private static bool isStarted = false;

    private List<float> times = new List<float>();
    private TimerUI timerUI;
    private float finishTime = -1f;

    public string CourseKey = "A";

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(this);
        }
    }
    // Use this for initialization
    void Start () {
        timerUI = GetComponentInChildren<TimerUI>();
	}
	
	// Update is called once per frame
	void Update () {
		
	}

    public void ResetRace()
    {
        times = new List<float>();
        isStarted = false;
        finishTime = -1f;
        timerUI.ResetGame();
    }

    public bool IsStarted()
    {
        return isStarted;
    }

    public bool IsFinished()
    {
        return finishTime != -1f;
    }

    public void StartRace()
    {
        if (times.Count == 0)
        {
            times.Add(Time.time);
            isStarted = true;
        }
    }

    public float TimeFromStart()
    {
        return Time.time - times[0];
    }

    public float GetTime(int checkpointNumber)
    {
        Debug.Log("Times " + times);
        if (times.Count > checkpointNumber && checkpointNumber > 0)
        {
            return times[checkpointNumber];
        }
        else if (checkpointNumber == -1)
        {
            return finishTime;
        }
        return 0;
    }

    public void CheckedIn(int checkpointNumber, int boatNumber = 0)
    {
        if (times.Count == checkpointNumber)
        {
            times.Add(TimeFromStart());
            timerUI.Checkpointed(checkpointNumber);
        }
    }

    public void Finish()
    {
        finishTime = TimeFromStart();
        if (!PlayerPrefs.HasKey(CourseKey + "_" + "record") || finishTime < PlayerPrefs.GetFloat(CourseKey + "_" + "record"))
        {
            for (int i = 1; i < times.Count; i++)
            {
                PlayerPrefs.SetFloat(CourseKey + "_" + i.ToString(), times[i]);
            }
            PlayerPrefs.SetFloat(CourseKey + "_" + times.Count.ToString(), finishTime);
            PlayerPrefs.Save();
            timerUI.Finished();
            PlayerPrefs.SetFloat(CourseKey + "_" + "record", finishTime);
            PlayerPrefs.Save();
            GetComponent<MenuUI>().HighScoreSet();
        }
        else
        {
            timerUI.Finished();
        }
    }
}
