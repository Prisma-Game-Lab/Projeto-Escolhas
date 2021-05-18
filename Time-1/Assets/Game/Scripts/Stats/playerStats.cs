using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerStats : MonoBehaviour
{
    public CharacterBase playerBase;
    //[HideInInspector]
    public int attack, defense, velocity, maxEnergy, maxHealth;

    private void Awake()
    {
        attack = playerBase.attack;
        defense = playerBase.defense;
        velocity = playerBase.velocity;
        maxEnergy = playerBase.maxEnergy;
        maxHealth = playerBase.maxHealth;
    }

    public void raiseStats(int type, int value)
    {
        if (type == 1)
        {
            velocity += value;
            maxEnergy += value;
        }
        else if (type == 2)
        {
            defense += value;
            maxHealth += value*3;
        }
        else if (type == 3)
        {
            attack += value;
        }
    }

}
