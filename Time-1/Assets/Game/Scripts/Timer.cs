using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Timer : MonoBehaviour
{
	[Tooltip("Tempo em segundos")]
	public float timeRemaining = 10;

	public TextMeshProUGUI TimerTextTMP;
	public static bool timeStopped;

	private bool finishedTime;
	private playerStats playerStats;
	public int raise;
	public int type;
	private Draw draw;


	private void Start()
	{
		playerStats = GameObject.FindGameObjectWithTag("persistentData").GetComponent<playerStats>();
        draw = gameObject.GetComponent<Draw>();
		if(draw!=null)
			print("deu bom");
	    TimerTextTMP.gameObject.SetActive(true);
		timeStopped = false;
		finishedTime = false;
	}

	void Update()
	{
		if (timeRemaining > 0)
			timeRemaining -= Time.deltaTime;
		else if(finishedTime==false)
		{
			finishedTime = true;
			timeRemaining = 0;
			playerStats.raiseStats(type, raise);
			timeStopped = true;
		}

		DisplayTime(timeRemaining);
	}

	void DisplayTime(float time)
	{
		if (!timeStopped)
			time += 1;

		float min = Mathf.FloorToInt(time / 60);
		float sec = Mathf.FloorToInt(time % 60);

		TimerTextTMP.text = string.Format("{00:00}:{1:00}", min, sec);
	}
}
