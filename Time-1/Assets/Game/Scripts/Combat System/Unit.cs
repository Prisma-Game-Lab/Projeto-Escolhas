using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public CharacterBase cBase;
    [HideInInspector]
    public float curHealth, curEnergy, maxHealth, maxEnergy, attack, defense, velocity, damage;
	[HideInInspector]
	public int currentShieldHits, shieldsAvailable;

    private void Awake()
    {
		if (gameObject.CompareTag("Player"))
		{
			playerStats playerStats = GameObject.FindGameObjectWithTag("persistentData").GetComponent<playerStats>();
			attack = playerStats.attack;
			defense = playerStats.defense;
			velocity = playerStats.velocity;
            maxEnergy = playerStats.maxEnergy + Mathf.Floor(velocity / 35);
		}
		else
		{
			TinderData tinderData=GameObject.FindGameObjectWithTag("persistentData").GetComponent<TinderData>();
			cBase = tinderData.combatCharacter;
			print(cBase.race);
			int curDay = tinderData.getCharacterDay(cBase);
			attack = cBase.attack + curDay * 30;
			defense = cBase.defense + curDay * 25;
            velocity = cBase.velocity + curDay * 20;
			print("Dia : " + curDay);
			print("ini atk : " + attack);
			print("ini def : " + defense);
			print("ini vel : " + velocity);
			maxEnergy = cBase.maxEnergy + Mathf.Floor(velocity/35);
		}
		maxHealth = attack + defense + 2*velocity;
		shieldsAvailable = 3 + Mathf.FloorToInt(defense / 175);
		curHealth = maxHealth;
		curEnergy = maxEnergy;
	}

    public bool TakeDamage(float dmg)
	{
		curHealth -= dmg;

		if (curHealth <= 0)
			return true;
		else
			return false;
	}

	public bool TakeEnergy(int attackType)
	{
		int energyLoss=0;
		if (attackType == 1)
			energyLoss = 2;
		else if (attackType == 2)
			energyLoss = 4;
		else if (attackType == 3)
			energyLoss = 3;
		if ((curEnergy - energyLoss >= 0))
		{
			curEnergy -= energyLoss;
			return true;
		}
		else
			return false;
	}

	public void Heal(int amount)
    {
        curHealth += amount;
        if (curHealth > maxHealth)
            curHealth = maxHealth;
    }
	public void GiveEnergy(int type)
	{
        int amount;
		if (type == 0)
			amount = 2;
		else if (type == 1)
			amount = 2;
		else if (type == 2)
			amount = 4;
		else if (type == 3)
			amount = 3;
		else
			amount = 5;
		curEnergy += amount;
		if (curEnergy > maxEnergy)
			curEnergy = maxEnergy;
	}

}
