using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisePlayerStats : MonoBehaviour
{
    private playerStats playerStats;

    private bool raised;

    public enum MinigameType {Minigame01,Minigame02,Minigame03};
    public MinigameType minigame;
    [Tooltip("1-Velocidade  2-Defesa  3-Ataque")]
    public int statType;
    public int raise;

    // Start is called before the first frame update
    void Start()
    {
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
                float performacePercentage = minigame01Performance();
                print("Performance = " + performacePercentage);
                print(raise);
                print(raise * performacePercentage);
                playerStats.raiseStats(statType, raise * performacePercentage);
                raised = true;
            }
        }else if(minigame == MinigameType.Minigame02 && !raised)
        {
            if (Spawner.allWavesFinished == true)
            {
                float performacePercentage = minigame02Performance();
                print("Performance = "+performacePercentage);
                print(raise);
                print(raise * performacePercentage);
                playerStats.raiseStats(statType, raise * performacePercentage);
                raised = true;
            }
        }
    }

    private float minigame01Performance()
    {
        Draw draw = GetComponent<Draw>();
        int score = draw.point;
        int totalDraws = draw.totalDraws;
        return (float)score/totalDraws;
    }

    private float minigame02Performance()
    {
        Spawner spawner = GetComponent<Spawner>();
        float ballImpacts = spawner.ballImpacts * 13;
        float totalBallsSpawned = spawner.totalBallsSpawned;
        return 1-(ballImpacts / totalBallsSpawned);
    }
}
