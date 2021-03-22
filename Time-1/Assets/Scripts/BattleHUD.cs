using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class BattleHUD : MonoBehaviour
{

	public TextMeshProUGUI nameText;
	public TextMeshProUGUI levelText;
	public Image hpGreenBar;

	public void SetHUD(Unit unit)
	{
		nameText.text = unit.unitName;
		levelText.text = "Lvl " + unit.unitLevel;
		hpGreenBar.fillAmount = (float)unit.currentHP/unit.maxHP;
	}

	public void SetHP(int hp, int maxHP)
	{
		hpGreenBar.fillAmount = (float)hp /maxHP;
	}

}
