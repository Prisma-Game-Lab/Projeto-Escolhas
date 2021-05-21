using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisePlayerStats : MonoBehaviour
{
    private playerStats playerStats;
    private FinishedMinigameUI finishedMinigameUI;
    private bool raised;

    
    public enum MinigameType {Minigame01,Minigame02,Minigame03};
    public MinigameType minigame;

    [HideInInspector] public float performacePercentage;
    [Tooltip("1-Velocidade  2-Defesa  3-Ataque")]
    public int statType;
    public int raise;

    private AudioManager audioManager;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        finishedMinigameUI = GetComponent<FinishedMinigameUI>();
        playerStats = GameObject.FindGameObjectWithTag("persistentData").GetComponent<playerStats>();
        raised = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (minigame == MinigameType.Minigame01 && !raised)
        {
            if (Timer.timeStopped)
            {
                audioManager.Play("RaiseStats");
                performacePercentage = minigame01Performance();
                print("Performance = " + performacePercentage);
                print("Raise = " + raise);
                print(raise * performacePercentage);
                playerStats.raiseStats(statType, raise * performacePercentage);
                raised = true;
                finishedMinigameUI.enabled = true;
            }
        }else if(minigame == MinigameType.Minigame02 && !raised)
        {
            if (Spawner.allWavesFinished == true)
            {
                audioManager.Play("RaiseStats");
                performacePercentage = minigame02Performance();
                print("Performance = "+performacePercentage);
                print("Raise = "+raise);
                print(raise * performacePercentage);
                playerStats.raiseStats(statType, raise * performacePercentage);
                raised = true;
                finishedMinigameUI.enabled = true;
            }
        }
    }

    private float minigame01Performance()
    {
        Draw draw = GetComponent<Draw>();
        int score = draw.point;
        int totalDraws = draw.totalDraws-1;
        return (float)score/totalDraws;
    }

    private float minigame02Performance()
    {
        Spawner spawner = GetComponent<Spawner>();
        float ballImpacts = spawner.ballImpacts * 13;
        float totalBallsSpawned = spawner.totalBallsSpawned;
        if ((1 - (ballImpacts / totalBallsSpawned)) < 0f)
            return 0f;
        return 1-(ballImpacts / totalBallsSpawned);
    }
}
