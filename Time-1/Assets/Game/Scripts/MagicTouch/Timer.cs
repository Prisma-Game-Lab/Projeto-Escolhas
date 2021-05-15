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

	public CharacterBase cBase;
	public int raise;
	public int type;
    private bool complete;
	private Draw draw;


	private void Start()
	{
		draw = gameObject.GetComponent<Draw>();
	    TimerTextTMP.gameObject.SetActive(true);
		complete = false;
		timeStopped = false;
	}

	void Update()
	{
		if (timeRemaining > 0)
			timeRemaining -= Time.deltaTime;
		else
		{
            draw.StopAllCoroutines();
			draw.currentLineRenderer.positionCount = 0;
			timeRemaining = 0;
			timeStopped = true;
		}

		DisplayTime(timeRemaining);

		if(timeRemaining == 0)
        {
			RaiseStat();
		}
	}

	void RaiseStat()
    {
		if(complete == false)
        {
			complete = true;
			if(type == 1)
            {
				cBase.maxHp += raise;
            }
			else if(type == 2)
            {
				cBase.maxEnergy += raise;
            }
			else if(type == 3)
            {
				cBase.strength += raise;
            }
			else if(type == 4)
            {
				cBase.defense += raise;
            }
		}
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
