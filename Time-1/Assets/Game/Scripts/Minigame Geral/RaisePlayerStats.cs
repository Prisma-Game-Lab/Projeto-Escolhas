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

    [Tooltip("Modificador do minigame 2")]
    public float multiplier;

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
        if (!raised && (Timer.timeStopped || Spawner.allWavesFinished))
        {
            raiseStats();
            Timer.timeStopped = false;
            Spawner.allWavesFinished = false;
        }
    }
    private void raiseStats()
    {
        audioManager.Play("RaiseStats");

        if (minigame == MinigameType.Minigame01)
            performacePercentage = Mathf.Clamp(minigame01Performance(), 0.25f, 1f);
        else if (minigame == MinigameType.Minigame02)
            performacePercentage = Mathf.Clamp(minigame02Performance(), 0.25f, 1f);
        else
            performacePercentage = Mathf.Clamp(minigame03Performance(), 0.25f, 1f);

        print("Performance = " + performacePercentage);
        print("Raise = " + raise);
        print(raise * performacePercentage);
        playerStats.raiseStats(statType, raise * performacePercentage);
        raised = true;
        finishedMinigameUI.enabled = true;
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
        float ballImpacts = spawner.ballImpacts * multiplier;
        float totalBallsSpawned = spawner.totalBallsSpawned;
        Debug.Log(totalBallsSpawned);
        if ((1 - (ballImpacts / totalBallsSpawned)) < 0f)
            return 0f;
        return 1-(ballImpacts / totalBallsSpawned);
    }

    private float minigame03Performance()
    {
        HammerGame hGame = GetComponent<HammerGame>();
        float points = hGame.point;
        float totalPossiblePoints = hGame.totalPossiblePoints;
        return points/totalPossiblePoints;
    }
}
