using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

	public TextMeshProUGUI nameText;
	public TextMeshProUGUI healthText;
    public Image hpGreenBar;
	public Image energyBar;

	public void SetHUD(Unit unit)
	{
		nameText.text = unit.Cbase.name;
		healthText.text = unit.Cbase.hp.ToString();
        hpGreenBar.fillAmount = (float)unit.Cbase.hp / unit.Cbase.maxHp;
		if(gameObject.CompareTag("Player"))
			energyBar.fillAmount = (float)unit.Cbase.energy / unit.Cbase.maxEnergy;
	}

    public void SetHP(int hp, int maxHP)
    {
        hpGreenBar.fillAmount = (float)hp / maxHP;
        if (hp <= 0)
            healthText.text = "0";
		else
			healthText.text = hp.ToString();
	}
	public void SetEnergy(int energy, int maxEnergy)
	{
		energyBar.fillAmount = (float)energy / maxEnergy;
	}

}
