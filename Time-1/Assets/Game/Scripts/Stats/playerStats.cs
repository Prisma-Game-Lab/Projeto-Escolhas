using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public CharacterBase playerBase;
    private AppSave appsave;
    [HideInInspector]
    public float attack, defense, velocity, maxEnergy, maxHealth;

    public float availableStatsPoints=100;

    private void Awake()
    {
        appsave = SaveSystem.GetInstance().appSave;
        if (appsave.playerAtk == 0)
        {
            attack = playerBase.attack;
            defense = playerBase.defense;
            velocity = playerBase.velocity;
            maxEnergy = 10 + Mathf.Floor(velocity / 35);
        }
        else
        {
            attack = appsave.playerAtk;
            defense = appsave.playerDef;
            velocity = appsave.playerVel;
            maxEnergy = 10 + Mathf.Floor(velocity / 35);
        }
        //maxHealth = playerBase.maxHealth;
    }

    public void raiseStats(int type, float value)
    {
        availableStatsPoints -= value;
        if (availableStatsPoints < 0)
        {
            value += availableStatsPoints;
            availableStatsPoints = 0;
        }

        if (type == 1)
        {
            velocity += value;
            maxEnergy = 10 + Mathf.Floor(velocity/35);
        }
        else if (type == 2)
        {
            defense += value;
            //maxHealth += value *3;
        }
        else if (type == 3)
        {
            attack += value;
        }
        appsave.playerAtk = attack;
        appsave.playerDef = defense;
        appsave.playerVel = velocity;
        SaveSystem.GetInstance().SaveState();
    }

}
