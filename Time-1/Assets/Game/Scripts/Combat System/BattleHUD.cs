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
	public Image shieldIcon;

	public void SetHUD(Unit unit)
	{
        nameText.text = unit.cBase.name;
		healthText.text = unit.curHealth.ToString();
		hpGreenBar.fillAmount = (float)unit.curHealth / unit.maxHealth;
		if(gameObject.CompareTag("Player"))
			energyBar.fillAmount = (float)unit.curEnergy / unit.maxEnergy;
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
	public void SetShield(bool state)
    {
		if (state)
		{
			Color white = new Color(255, 255, 255);
			shieldIcon.color = white;
		}
        else
        {
			Color black = new Color(0, 0, 0);
			shieldIcon.color = black;
		}
    }

}
