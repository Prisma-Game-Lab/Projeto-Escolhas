using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

	public TextMeshProUGUI nameText;
	public TextMeshProUGUI healthText;
    public Image healthBar;
	public Image energyBar;
	public Image shieldIcon;

	public void SetHUD(Unit unit)
	{
        nameText.text = unit.cBase.name;
		healthText.text = "" + (int)unit.curHealth;
		healthBar.fillAmount = unit.curHealth / unit.maxHealth;
		if(gameObject.CompareTag("Player"))
			energyBar.fillAmount = unit.curEnergy / unit.maxEnergy;
	}

    public void SetHP(float curHealth, Unit unit, int type)
    {
		StartCoroutine(LerpUI(curHealth, unit, type));
		//if (unit.curHealth <= 0)
  //          healthText.text = "0";
		//else
		//	healthText.text = ""+(int)unit.curHealth;
	}
    public void SetEnergy(float curEnergy, Unit unit, int type)
    {
		StartCoroutine(LerpUI(curEnergy, unit, type));
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

	private IEnumerator LerpUI(float curValue, Unit unit, int type)
    {
		float elapsedTime=0;
		float waitTime=0.15f;
		while (elapsedTime < waitTime)
		{
			elapsedTime += Time.deltaTime;
			if (type == 1)
			{
				float value = Mathf.Lerp(curValue, unit.curHealth, (elapsedTime / waitTime));
				healthBar.fillAmount = (float)value / unit.maxHealth;
				if(value>=0)
					healthText.text = "" + (int)value;
				else
					healthText.text = "0";
			}
			else if (type == 2)
			{
				float value = Mathf.Lerp(curValue, unit.curEnergy, (elapsedTime / waitTime));
				energyBar.fillAmount = (float)value / unit.maxEnergy;
			}
			yield return null;
		}
		yield return null;
	}

}
