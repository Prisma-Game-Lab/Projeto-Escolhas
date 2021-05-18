using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
    public Image enemyWhiteImage;
    public Image enemyImage;

    public TextMeshProUGUI dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public GameObject DecisionPanel;
    public GameObject DecisionAttackButton;
    public GameObject DecisionQuitButton;
    public GameObject CombatPanel;

    public GameObject wonDatePanel;
    public GameObject lostDatePanel;

    private BattleSystem battleSystem;

    // Start is called before the first frame update
    void Start()
    {
        battleSystem = GetComponent<BattleSystem>();

        wonDatePanel.SetActive(false);
        lostDatePanel.SetActive(false);
        CombatPanel.SetActive(false);
        DecisionPanel.SetActive(true);
        DecisionAttackButton.SetActive(true);
        DecisionQuitButton.SetActive(true);
    }

    public void OnAttackButton(int tipo)
    {
        if (battleSystem.state != BattleState.PLAYERTURN)
            return;

        battleSystem.StartCoroutine(battleSystem.PlayerAttack(tipo));
    }

    public void OnDefenseButton()
    {
        int curEnergy = battleSystem.playerUnit.curEnergy;
        if (battleSystem.state != BattleState.PLAYERTURN)
            return;
        if (!battleSystem.defenseOn && battleSystem.playerUnit.TakeEnergy(3))
        {
            battleSystem.defenseOn = true;
            playerHUD.SetShield(true);
        }
        else if (battleSystem.defenseOn)
        {
            battleSystem.defenseOn = false;
            playerHUD.SetShield(false);
            battleSystem.playerUnit.GiveEnergy(3);
        }
        playerHUD.SetEnergy(curEnergy, battleSystem.playerUnit, 2);
    }

    public void OnHealButton()
    {
        if (battleSystem.state != BattleState.PLAYERTURN)
            return;

        battleSystem.StartCoroutine(battleSystem.PlayerHeal());
    }

    public void OnAttackPanelButton()
    {
        if (battleSystem.state != BattleState.PLAYERTURN)
            return;
        DecisionAttackButton.SetActive(false);
        DecisionQuitButton.SetActive(false);
        DecisionPanel.SetActive(false);
        CombatPanel.SetActive(true);
    }

    public void OnQuitButton()
    {
        if (battleSystem.state != BattleState.PLAYERTURN && battleSystem.state != BattleState.WON && battleSystem.state != BattleState.LOST)
            return;

        battleSystem.StartCoroutine(battleSystem.PlayerRun());
    }

    public void OnBackButton()
    {
        DecisionPanel.SetActive(true);
        DecisionAttackButton.SetActive(true);
        DecisionQuitButton.SetActive(true);
        CombatPanel.SetActive(false);
    }
}
