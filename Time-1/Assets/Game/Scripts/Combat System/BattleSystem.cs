using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, RUN, ATTACK, HEAL }

public class BattleSystem : MonoBehaviour
{
    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    [HideInInspector] public Unit playerUnit, enemyUnit;

    public BattleState state;
    [HideInInspector] public bool defenseOn;

    private BattleUIManager battleUI;

    void Start()
    {
        battleUI = GetComponent<BattleUIManager>();
        defenseOn = false;
        state = BattleState.START;
        StartCoroutine(SetupBattle());
    }

    IEnumerator SetupBattle()
    {
        GameObject playerGO = Instantiate(playerPrefab);
        playerUnit = playerGO.GetComponent<Unit>();

        GameObject enemyGO = Instantiate(enemyPrefab);
        enemyUnit = enemyGO.GetComponent<Unit>();

        battleUI.enemyImage.sprite = enemyUnit.cBase.combatImage;
        battleUI.dialogueText.text = enemyUnit.cBase.name + " se aproxima...";

        battleUI.playerHUD.SetHUD(playerUnit);
        battleUI.enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(2f);

        state = BattleState.PLAYERTURN;
        PlayerTurn();
    }

    public IEnumerator PlayerAttack(int tipo)
    {
        int curEnergy = playerUnit.curEnergy;
        if (playerUnit.TakeEnergy(tipo))
        {
            battleUI.CombatPanel.SetActive(false);
            battleUI.DecisionPanel.SetActive(true);
            battleUI.playerHUD.SetEnergy(curEnergy, playerUnit,2);
            state = BattleState.ATTACK;
            bool isDead;
            int enemyCurHealth = enemyUnit.curHealth;

            if (tipo == 1)
            {
                isDead = enemyUnit.TakeDamage((int)(playerUnit.attack * 1));
                battleUI.dialogueText.text = "Você socou seu date!";
            }
            else if (tipo == 2)
            {
                isDead = enemyUnit.TakeDamage((int)(playerUnit.attack * 1.5));
                battleUI.dialogueText.text = "Você chutou seu date!";
            }
            else
            {
                battleUI.dialogueText.text = "Você usou ataque especial no seu date!";
                isDead = enemyUnit.TakeDamage((int)(playerUnit.attack * 2));
            }

            battleUI.enemyHUD.SetHP(enemyCurHealth, enemyUnit, 1);

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
        battleUI.dialogueText.text = enemyUnit.cBase.name + " te socou!";

        yield return new WaitForSeconds(1f);
        bool isDead;
        int playerCurHealth = enemyUnit.curHealth;

        if (defenseOn)
        {
            int damage = (int)(enemyUnit.attack - playerUnit.defense);
            damage = Mathf.Clamp(damage, 1, 9999);
            isDead = playerUnit.TakeDamage(damage);
            battleUI.playerHUD.SetHP(playerCurHealth, playerUnit, 1);
        }
        else {
            isDead = playerUnit.TakeDamage(enemyUnit.attack);
            battleUI.playerHUD.SetHP(playerCurHealth, playerUnit, 1);
        }


        yield return new WaitForSeconds(0.5f);

        if (defenseOn)
        {
            defenseOn = false;
            battleUI.playerHUD.SetShield(false);
        }
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
            battleUI.DecisionQuitButton.SetActive(true);
            battleUI.wonDatePanel.SetActive(true);
            battleUI.dialogueText.text = "Você ganhou o encontro! "+enemyUnit.cBase.name+ " esta totalmente na sua!";
            GameObject.FindGameObjectWithTag("persistentData").GetComponent<TinderData>().curDay += 1;
        }
        else if (state == BattleState.LOST)
        {
            battleUI.lostDatePanel.SetActive(true);
            battleUI.DecisionQuitButton.SetActive(true);
            battleUI.dialogueText.text = "Você foi derrotado. " + enemyUnit.cBase.name + " esta indo embora insatisfeita.";
        }
    }

    void PlayerTurn()
    {
        int curEnergy = playerUnit.curEnergy;
        battleUI.dialogueText.text = "Escolha uma ação:";
        battleUI.DecisionAttackButton.SetActive(true);
        battleUI.DecisionQuitButton.SetActive(true);
        playerUnit.GiveEnergy(2);
        battleUI.playerHUD.SetEnergy(curEnergy, playerUnit, 2);
    }

    public IEnumerator PlayerHeal()
    {
        int playerCurHealth = enemyUnit.curHealth;
        state = BattleState.HEAL;
        playerUnit.Heal(5);

        battleUI.playerHUD.SetHP(playerCurHealth, playerUnit, 1);
        battleUI.dialogueText.text = "You feel renewed strength!";

        yield return new WaitForSeconds(2f);

        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    public IEnumerator PlayerRun()
    {
        state = BattleState.RUN;
        battleUI.DecisionAttackButton.SetActive(false);
        battleUI.DecisionQuitButton.SetActive(false);
        battleUI.dialogueText.text = "Saindo do encontro...";
        //alguma animacao
        yield return new WaitForSeconds(1.0f);
        //sai do combate
        SceneManager.LoadScene("App");
    }
}
