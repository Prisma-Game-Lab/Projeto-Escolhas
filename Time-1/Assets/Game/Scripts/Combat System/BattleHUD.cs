using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

	public TextMeshProUGUI nameText;
	public TextMeshProUGUI healthText;
	public TextMeshProUGUI energyText;
	public Image healthBar;
	public Image energyBar;
	public Image shieldIcon;

	public void SetHUD(Unit unit)
	{
        nameText.text = unit.cBase.name;
		healthText.text = "" + (int)unit.curHealth;
		healthBar.fillAmount = unit.curHealth / unit.maxHealth;
		if (gameObject.CompareTag("Player"))
		{
			energyBar.fillAmount = unit.curEnergy / unit.maxEnergy;
			energyText.text = (int)unit.curEnergy + " / " + (int)unit.maxEnergy;
		}
	}

    public void SetHP(float curHealth, Unit unit)
    {
		StartCoroutine(LerpUI(curHealth, unit, healthBar, healthText, 1));
	}
    public void SetEnergy(float curEnergy, Unit unit)
    {
		StartCoroutine(LerpUI(curEnergy, unit, energyBar, energyText, 2));
    }
	//public void SetShield(bool state)
	//   {
	//	if (state)
	//	{
	//		Color white = new Color(255, 255, 255);
	//		shieldIcon.color = white;
	//	}
	//       else
	//       {
	//		Color black = new Color(0, 0, 0);
	//		shieldIcon.color = black;
	//	}
	//   }

	private IEnumerator LerpUI(float curValue, Unit unit, Image imageBar, TextMeshProUGUI barText, int type)
	{
		float elapsedTime = 0;
		float waitTime = 0.15f;
		while (elapsedTime < waitTime)
		{
			elapsedTime += Time.deltaTime;

			if (type == 1)
			{
				float value = Mathf.Lerp(curValue, unit.curHealth, (elapsedTime / waitTime));
				imageBar.fillAmount = (float)value / unit.maxHealth;
				if (value >= 0)
					barText.text = "" + (int)value;
				else
					barText.text = "0";
			}else if (type == 2)
            {
				float value = Mathf.Lerp(curValue, unit.curEnergy, (elapsedTime / waitTime));
				imageBar.fillAmount = (float)value / unit.maxEnergy;
				if (value >= 0)
					barText.text = (int)unit.curEnergy + " / " + (int)unit.maxEnergy;
				else
					barText.text = "0 / "+ (int)unit.maxEnergy;
			}
			yield return null;
		}
		yield return null;
	}

}
