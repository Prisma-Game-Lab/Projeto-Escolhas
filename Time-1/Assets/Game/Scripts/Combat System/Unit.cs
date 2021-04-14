using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Unit : MonoBehaviour
{
	public CharacterBase Cbase;
	/*public string unitName;
	public int unitLevel;

	public int damage;

	public int maxHP;
	public int currentHP;
	*/
	public bool TakeDamage(int dmg)
	{
		Cbase.hp -= dmg;

		if (Cbase.hp <= 0)
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
			energyLoss = 6;
		if ((Cbase.energy - energyLoss >= 0))
		{
			Cbase.energy -= energyLoss;
			return true;
		}
		else
			return false;
	}

	public void Heal(int amount)
    {
        Cbase.hp += amount;
        if (Cbase.hp > Cbase.maxHp)
            Cbase.hp = Cbase.maxHp;
    }
	public void GetEnergy(int amount)
	{
		Cbase.energy += amount;
		if (Cbase.energy > Cbase.maxEnergy)
			Cbase.energy = Cbase.maxEnergy;
	}

}
