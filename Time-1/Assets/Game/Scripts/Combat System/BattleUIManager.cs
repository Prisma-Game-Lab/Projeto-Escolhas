using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class BattleUIManager : MonoBehaviour
{
    public List<GameObject> actionsButtons;
    [HideInInspector] public List<GameObject> spawnedActionsButtons = new List<GameObject>();
    public Transform actionsPanel;
    public Image enemyImage;
    public Image scenerioImage;

    public Image sliderImage;
    public Sprite sliderRestSprite;
    public Sprite sliderPunchSprite;

    public TextMeshProUGUI dialogueText;
    public TextMeshProUGUI turnText;
    public Slider attackSlider;
    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public GameObject DecisionPanel;
    public GameObject DecisionAttackButton;
    public GameObject DecisionQuitButton;
    public TextMeshProUGUI DecisionQuitButton_text;
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
        actionsPanel.gameObject.SetActive(false);
        //DecisionAttackButton.SetActive(true);
        //DecisionQuitButton.SetActive(true);
    }

    public void OnAttackButton()
    {
        if (battleSystem.playerActions.Count == 0)
            OnActionButton(5);
        if (battleSystem.state != BattleState.PLAYERTURN)
            return;
        if (battleSystem.playerActions.Count > 0)
            battleSystem.StartCoroutine(battleSystem.PlayerAttack());
    }

    public void OnActionButton(int type)
    {
        float curEnergy = battleSystem.playerUnit.curEnergy;
        if (battleSystem.state != BattleState.PLAYERTURN)
        {
            audioManager.Play("Click");
            return;
        }
        if (type == 3 && battleSystem.playerUnit.shieldsAvailable <= 0)
        {
            audioManager.Play("Click");
            return;
        }
        if (!battleSystem.playerActions.Contains(3) && battleSystem.playerUnit.TakeEnergy(type))
        {
            if (type == 3)
            {
                audioManager.Play("ShieldUp");
            }
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
        if (battleSystem.playerActions.Contains(5))
        {
            sliderImage.sprite = sliderPunchSprite;
            OnActionClicked(5, 0);
        }
        else
            audioManager.Play("Click");
        playerHUD.SetEnergy(curEnergy, battleSystem.playerUnit);
    }
    public void OnActionClicked(int type, int index)
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
        battleSystem.playerActions.RemoveAt(index);

        if (battleSystem.playerActions.Count == 0)
        {
            sliderImage.sprite = sliderRestSprite;
            battleSystem.playerActions.Add(5);
        }
        SetActionsHUD(battleSystem.playerActions);
        playerHUD.SetEnergy(curEnergy, battleSystem.playerUnit);
    }
    public void clearActionsHUD()
    {
        if (spawnedActionsButtons.Count > 0)
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
            int index = i;
            actionButton.GetComponent<Button>().onClick.AddListener(() => OnActionClicked(action, index));
            spawnedActionsButtons.Add(actionButton);
            if ((actions.Count - 1) != i)
            {
                actionButton = Instantiate(actionsButtons[3], actionsPanel);
                spawnedActionsButtons.Add(actionButton);
            }
        }
    }

    public void OnAttackPanelButton()
    {
        if (battleSystem.state != BattleState.PLAYERTURN)
            return;
        audioManager.Play("Click");
        actionsPanel.gameObject.SetActive(true);
        DecisionAttackButton.SetActive(false);
        DecisionQuitButton.SetActive(false);
        DecisionPanel.SetActive(false);
        CombatPanel.SetActive(true);
    }

    public void OnQuitButton()
    {
        if (battleSystem.state != BattleState.PLAYERTURN && battleSystem.state != BattleState.WON && battleSystem.state != BattleState.LOST)
            return;
        audioManager.Play("Click");
        battleSystem.StartCoroutine(battleSystem.PlayerRun());
    }

    public void OnBackButton()
    {
        audioManager.Play("Click");
        DecisionPanel.SetActive(true);
        DecisionAttackButton.SetActive(true);
        //DecisionQuitButton.SetActive(true);
        CombatPanel.SetActive(false);
    }

    public IEnumerator showText(string dialogue)
    {
        for (int i = 0; i < dialogue.Length + 1; i++)
        {
            string currentText = dialogue.Substring(0, i);
            dialogueText.text = currentText;
            yield return new WaitForSeconds(0.01f);
        }
    }
}
