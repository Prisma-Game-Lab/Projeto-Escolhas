using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
    public List<GameObject> actionsImages;
    [HideInInspector]public List<GameObject> spawnedActionsImages = new List<GameObject>();
    public Transform actionsPanel;

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

    public void OnAttackButton()
    {
        if (battleSystem.state != BattleState.PLAYERTURN)
            return;
        battleSystem.StartCoroutine(battleSystem.PlayerAttack());
    }

    //public void OnDefenseButton()
    //{
    //    float curEnergy = battleSystem.playerUnit.curEnergy;
    //    if (battleSystem.state != BattleState.PLAYERTURN)
    //        return;
    //    if (!battleSystem.defenseOn && battleSystem.playerUnit.TakeEnergy(3))
    //    {
    //        audioManager.Play("ShieldUp");
    //        battleSystem.defenseOn = true;
    //        playerHUD.SetShield(true);
    //    }
    //    else if (battleSystem.defenseOn)
    //    {
    //        audioManager.Play("ShieldDown");
    //        battleSystem.defenseOn = false;
    //        playerHUD.SetShield(false);
    //        battleSystem.playerUnit.GiveEnergy(3);
    //    }else
    //        audioManager.Play("Click");
    //    playerHUD.SetEnergy(curEnergy, battleSystem.playerUnit);
    //}
    public void OnActionButton(int type)
    {
        float curEnergy = battleSystem.playerUnit.curEnergy;
        if (battleSystem.state != BattleState.PLAYERTURN)
            return;
        if (!battleSystem.actions.Contains(3) && !battleSystem.actions.Contains(5) && !battleSystem.actions.Contains(type) && battleSystem.playerUnit.TakeEnergy(type))
        {
            if (type == 3)
                audioManager.Play("ShieldUp");
            else if (type == 5 && battleSystem.actions.Count > 0)
            {
                audioManager.Play("Click");
                return;
            }
            else
                audioManager.Play("Match");
            battleSystem.actions.Add(type);
            SetActionsHUD(battleSystem.actions);
        }
        else if (battleSystem.actions.Count>0 && battleSystem.actions[battleSystem.actions.Count-1]==type)
        {
            if (type == 3)
                audioManager.Play("ShieldDown");
            else
                audioManager.Play("Reject");

            if (type != 5)
            {
                battleSystem.playerUnit.GiveEnergy(type);
            }
            battleSystem.actions.Remove(type);
            SetActionsHUD(battleSystem.actions);
        }
        else
            audioManager.Play("Click");
        playerHUD.SetEnergy(curEnergy, battleSystem.playerUnit);
    }
    private void clearActionsHUD()
    {
        if (spawnedActionsImages.Count>0)
        {
            for (int i = 0; i < spawnedActionsImages.Count; i++)
            {
                Destroy(spawnedActionsImages[i]);
            }
            spawnedActionsImages.Clear();
        }
    }
    public void SetActionsHUD(List<int> actions)
    {
        clearActionsHUD();
        for (int i = 0; i < actions.Count; i++)
        {
            GameObject actionImage = Instantiate(actionsImages[actions[i] - 1], actionsPanel);
            spawnedActionsImages.Add(actionImage);
            if (actions[actions.Count - 1] != actions[i])
            {
                actionImage = Instantiate(actionsImages[3], actionsPanel);
                spawnedActionsImages.Add(actionImage);
            }
        }
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
