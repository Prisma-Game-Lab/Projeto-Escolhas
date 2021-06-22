using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
    public List<GameObject> actionsButtons;
    [HideInInspector]public List<GameObject> spawnedActionsButtons = new List<GameObject>();
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
        if (battleSystem.playerActions.Count > 0)
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
        if (!battleSystem.playerActions.Contains(3) && !battleSystem.playerActions.Contains(5) && battleSystem.playerUnit.TakeEnergy(type))
        {
            if (type == 3)
                audioManager.Play("ShieldUp");
            else if (type == 5 && battleSystem.playerActions.Count > 0)
            {
                audioManager.Play("Click");
                return;
            }
            else
                audioManager.Play("Match");
            battleSystem.playerActions.Add(type);
            SetActionsHUD(battleSystem.playerActions);
        }
        else
            audioManager.Play("Click");
        playerHUD.SetEnergy(curEnergy, battleSystem.playerUnit);
    }
    public void OnActionClicked(int type)
    {
        float curEnergy = battleSystem.playerUnit.curEnergy;
        if (type == 3)
            audioManager.Play("ShieldDown");
        else
            audioManager.Play("Reject");

        if (type != 5)
        {
            battleSystem.playerUnit.GiveEnergy(type);
        }
        battleSystem.playerActions.Remove(type);
        SetActionsHUD(battleSystem.playerActions);
        playerHUD.SetEnergy(curEnergy, battleSystem.playerUnit);
    }
    public void clearActionsHUD()
    {
        if (spawnedActionsButtons.Count>0)
        {
            for (int i = 0; i < spawnedActionsButtons.Count; i++)
            {
                Destroy(spawnedActionsButtons[i]);
            }
            spawnedActionsButtons.Clear();
        }
    }
    public void SetActionsHUD(List<int> actions)
    {
        clearActionsHUD();
        for (int i = 0; i < actions.Count; i++)
        {
            GameObject actionButton = Instantiate(actionsButtons[actions[i] - 1], actionsPanel);
            int action = actions[i];
            actionButton.GetComponent<Button>().onClick.AddListener(()=>OnActionClicked(action));
            spawnedActionsButtons.Add(actionButton);
            if ((actions.Count - 1) != i)
            {
                actionButton = Instantiate(actionsButtons[3], actionsPanel);
                spawnedActionsButtons.Add(actionButton);
            }
        }
    }

    //public void OnHealButton()
    //{
    //    if (battleSystem.state != BattleState.PLAYERTURN)
    //        return;

    //    battleSystem.StartCoroutine(battleSystem.PlayerHeal());
    //}

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
