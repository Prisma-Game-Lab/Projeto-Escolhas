using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public CharacterBase cBase;
	[HideInInspector]
	public int curHealth, curEnergy, maxHealth, maxEnergy, attack, defense, velocity;

    private void Awake()
    {
		if (gameObject.CompareTag("Player"))
		{
			playerStats playerStats = GameObject.FindGameObjectWithTag("persistentData").GetComponent<playerStats>();
			attack = playerStats.attack;
			defense = playerStats.defense;
			velocity = playerStats.velocity;
			maxHealth = playerStats.maxHealth;
			maxEnergy = playerStats.maxEnergy;
		}
		else
		{
			int curDay = GameObject.FindGameObjectWithTag("persistentData").GetComponent<TinderData>().curDay;
			attack = cBase.attack * (curDay + 1) / 2;
			defense = cBase.defense * (curDay + 1) / 2;
			velocity = cBase.velocity * (curDay + 1) / 2;
			maxHealth = cBase.maxHealth * (curDay + 1) / 2;
			maxEnergy = cBase.maxEnergy * (curDay + 1) / 2;
		}
		curHealth = maxHealth;
		curEnergy = maxEnergy;
	}

    public bool TakeDamage(int dmg)
	{
		curHealth -= dmg;

		if (curHealth <= 0)
			return true;
		else
			return false;
	}

	public bool TakeEnergy(int attackType)
	{
		int energyLoss;
		if (attackType == 1)
			energyLoss = 2;
		else if (attackType == 2)
			energyLoss = 4;
		else 
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
	public void GiveEnergy(int amount)
	{
		curEnergy += amount;
		if (curEnergy > maxEnergy)
			curEnergy = maxEnergy;
	}

}
