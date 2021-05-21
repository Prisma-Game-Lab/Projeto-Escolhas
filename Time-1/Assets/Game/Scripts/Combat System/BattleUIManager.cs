using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
    public Image elfWhiteImage;
    public Image orcWhiteImage;
    public Image sereiaWhiteImage;
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
    private AudioManager audioManager;

    // Start is called before the first frame update
    void Start()
    {
        battleSystem = GetComponent<BattleSystem>();
        audioManager = FindObjectOfType<AudioManager>();
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
        float curEnergy = battleSystem.playerUnit.curEnergy;
        if (battleSystem.state != BattleState.PLAYERTURN)
            return;
        if (!battleSystem.defenseOn && battleSystem.playerUnit.TakeEnergy(3))
        {
            audioManager.Play("ShieldUp");
            battleSystem.defenseOn = true;
            playerHUD.SetShield(true);
        }
        else if (battleSystem.defenseOn)
        {
            audioManager.Play("ShieldDown");
            battleSystem.defenseOn = false;
            playerHUD.SetShield(false);
            battleSystem.playerUnit.GiveEnergy(3);
        }else
            audioManager.Play("Click");
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
        audioManager.Play("Click");
        DecisionAttackButton.SetActive(false);
        DecisionQuitButton.SetActive(false);
        DecisionPanel.SetActive(false);
        CombatPanel.SetActive(true);
    }

    public void OnQuitButton()
    {
        if (battleSystem.state != BattleState.PLAYERTURN && battleSystem.state != BattleState.WON && battleSystem.state != BattleState.LOST)
            return;
        audioManager.Play("Run");
        battleSystem.StartCoroutine(battleSystem.PlayerRun());
    }

    public void OnBackButton()
    {
        audioManager.Play("Click");
        DecisionPanel.SetActive(true);
        DecisionAttackButton.SetActive(true);
        DecisionQuitButton.SetActive(true);
        CombatPanel.SetActive(false);
    }
}
