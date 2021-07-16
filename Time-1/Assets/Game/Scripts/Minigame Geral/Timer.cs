using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
	[Tooltip("Tempo em segundos")]
	public float timeRemaining = 10;
	public static float totalTime;
	public TextMeshProUGUI TimerTextTMP;
	public static bool timeStopped;
	private bool finishedTime;


	private void Start()
	{
		totalTime = timeRemaining;
	    TimerTextTMP.gameObject.SetActive(true);
		timeStopped = false;
		finishedTime = false;
	}

	void Update()
	{
		if (timeRemaining > 0)
		{
            timeRemaining -= Time.deltaTime;
		}
		else if (finishedTime == false)
		{
			finishedTime = true;
			timeRemaining = 0;
			timeStopped = true;
		}
		DisplayTime(timeRemaining);
	}

	void DisplayTime(float time)
	{
		//if (!timeStopped)
		//	time += 1;
		float min;
		float sec;
		if (time > 0)
		{
			min = Mathf.FloorToInt(time / 60);
			sec = Mathf.FloorToInt(time % 60);
        }
        else
        {
            min = 0;
			sec = 0;
        }
		TimerTextTMP.text = string.Format("{00:00}:{1:00}", min, sec);
	}
}
