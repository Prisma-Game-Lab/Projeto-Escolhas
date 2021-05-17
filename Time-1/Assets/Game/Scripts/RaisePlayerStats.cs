using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RaisePlayerStats : MonoBehaviour
{
    private playerStats playerStats;

    private bool raised;

    public enum MinigameType {Minigame01,Minigame02,Minigame03};
    public MinigameType minigame;

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
                playerStats.raiseStats(statType, raise);
                raised = true;
            }
        }else if(minigame == MinigameType.Minigame02 && !raised)
        {
            if (Spawner.allWavesFinished == true)
            {
                playerStats.raiseStats(statType, raise);
                raised = true;
            }
        }
    }
}
