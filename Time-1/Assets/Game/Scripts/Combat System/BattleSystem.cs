using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using TMPro;
using System;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, RUN, ATTACK, HEAL }

public class BattleSystem : MonoBehaviour
{

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    public Transform enemyBattleStation;

    Unit playerUnit;
    Unit enemyUnit;

    public TextMeshProUGUI dialogueText;

    public BattleHUD playerHUD;
    public BattleHUD enemyHUD;

    public GameObject DecisionPanel;
    public GameObject DecisionAttackButton;
    public GameObject DecisionQuitButton;
    public GameObject CombatPanel;

    public GameObject wonDatePanel;
    public GameObject lostDatePanel;

    public BattleState state;

    // Start is called before the first frame update
    void Start()
    {
        state = BattleState.START;
        wonDatePanel.SetActive(false);
        lostDatePanel.SetActive(false);
        CombatPanel.SetActive(false);
        DecisionPanel.SetActive(true);
        DecisionAttackButton.SetActive(true);
        DecisionQuitButton.SetActive(true);
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab, enemyBattleStation);
        enemyUnit = enemyGO.GetComponent<Unit>();

        dialogueText.text = enemyUnit.Cbase.name + " se aproxima...";

        enemyUnit.Cbase.hp = enemyUnit.Cbase.maxHp;
        playerUnit.Cbase.hp = playerUnit.Cbase.maxHp;
        playerUnit.Cbase.energy = playerUnit.Cbase.maxEnergy;

        playerHUD.SetHUD(playerUnit);
        enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }


    IEnumerator PlayerAttack(int tipo)
    {
        if (playerUnit.TakeEnergy(tipo))
        {
            CombatPanel.SetActive(false);
            DecisionPanel.SetActive(true);
            playerHUD.SetEnergy(playerUnit.Cbase.energy, playerUnit.Cbase.maxEnergy);
            state = BattleState.ATTACK;
            bool isDead;

            if (tipo == 1)
            {
                isDead = enemyUnit.TakeDamage((int)(playerUnit.Cbase.strength * 1));
                dialogueText.text = "Você socou seu date!";
            }
            else if (tipo == 2)
            {
                isDead = enemyUnit.TakeDamage((int)(playerUnit.Cbase.strength * 1.5));
                dialogueText.text = "Você chutou seu date!";
            }
            else
            {
                dialogueText.text = "Você usou ataque especial no seu date!";
                isDead = enemyUnit.TakeDamage((int)(playerUnit.Cbase.strength * 2));
            }

            enemyHUD.SetHP(enemyUnit.Cbase.hp, enemyUnit.Cbase.maxHp);
            

            yield return new WaitForSeconds(1.5f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            else
            {
                state = BattleState.ENEMYTURN;
                StartCoroutine(EnemyTurn());
            }
        }
    }

    IEnumerator EnemyTurn()
    {
        dialogueText.text = enemyUnit.Cbase.name + " te socou!";

        yield return new WaitForSeconds(1f);

        bool isDead = playerUnit.TakeDamage(enemyUnit.Cbase.strength);

        playerHUD.SetHP(playerUnit.Cbase.hp, playerUnit.Cbase.maxHp);

        yield return new WaitForSeconds(0.5f);

        if (isDead)
        {
            state = BattleState.LOST;
            EndBattle();
        }
        else
        {
            state = BattleState.PLAYERTURN;
            PlayerTurn();
        }

    }

    void EndBattle()
    {
        if (state == BattleState.WON)
        {
            DecisionQuitButton.SetActive(true);
            wonDatePanel.SetActive(true);
            dialogueText.text = "Você ganhou o encontro! "+enemyUnit.Cbase.name+ " esta totalmente na sua!";
        }
        else if (state == BattleState.LOST)
        {
            lostDatePanel.SetActive(true);
            DecisionQuitButton.SetActive(true);
            dialogueText.text = "Você foi derrotado. " + enemyUnit.Cbase.name + " esta indo embora insatisfeita.";
        }
    }

    void PlayerTurn()
    {
        dialogueText.text = "Escolha uma ação:";
        DecisionAttackButton.SetActive(true);
        DecisionQuitButton.SetActive(true);
        playerUnit.GetEnergy(2);
        playerHUD.SetEnergy(playerUnit.Cbase.energy, playerUnit.Cbase.maxEnergy);
    }

    IEnumerator PlayerHeal()
    {
        state = BattleState.HEAL;
        playerUnit.Heal(5);

        playerHUD.SetHP(playerUnit.Cbase.hp, playerUnit.Cbase.maxHp);
        dialogueText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public void OnAttackButton(int tipo)
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerAttack(tipo));
    }

    public void OnHealButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;

        StartCoroutine(PlayerHeal());
    }
    public void OnQuitButton()
    {
        if (state != BattleState.PLAYERTURN && state != BattleState.WON && state != BattleState.LOST)
            return;

        StartCoroutine(PlayerRun());
    }
    public void OnAttackPanelButton()
    {
        if (state != BattleState.PLAYERTURN)
            return;
        DecisionAttackButton.SetActive(false);
        DecisionQuitButton.SetActive(false);
        DecisionPanel.SetActive(false);
        CombatPanel.SetActive(true);
    }
    public void OnBackButton()
    {
        DecisionPanel.SetActive(true);
        DecisionAttackButton.SetActive(true);
        DecisionQuitButton.SetActive(true);
        CombatPanel.SetActive(false);
    }

    IEnumerator PlayerRun()
    {
        state = BattleState.RUN;
        DecisionAttackButton.SetActive(false);
        DecisionQuitButton.SetActive(false);
        dialogueText.text = "Saindo do encontro...";
        //alguma animacao
        yield return new WaitForSeconds(1.0f);
        //sai do combate
        SceneManager.LoadScene("App");
    }
}
