using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System;

public enum BattleState { START, PLAYERTURN, ENEMYTURN, WON, LOST, RUN, ATTACK, HEAL }

public class BattleSystem : MonoBehaviour
{
    //public 

    public GameObject playerPrefab;
    public GameObject enemyPrefab;

    [HideInInspector] public Unit playerUnit, enemyUnit;

    public BattleState state;
    [HideInInspector] public bool defenseOn;

    private BattleUIManager battleUI;
    private AudioManager audioManager;

    [HideInInspector] public List<int> playerActions;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
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
        //enemyUnit.cBase = GameObject.FindGameObjectWithTag("persistentData").GetComponent<TinderData>().combatCharacter;
        battleUI.enemyImage.sprite = enemyUnit.cBase.combatImage;
        battleUI.dialogueText.text = enemyUnit.cBase.name + " se aproxima...";

        battleUI.playerHUD.SetHUD(playerUnit);
        battleUI.enemyHUD.SetHUD(enemyUnit);

        yield return new WaitForSeconds(1.5f);

        state = BattleState.PLAYERTURN;
        StartTurn();
    }

    //public IEnumerator PlayerAttack(int tipo)
    //{
    //    float curEnergy = playerUnit.curEnergy;
    //    if (playerUnit.TakeEnergy(tipo))
    //    {
    //        battleUI.CombatPanel.SetActive(false);
    //        battleUI.DecisionPanel.SetActive(true);
    //        battleUI.playerHUD.SetEnergy(curEnergy, playerUnit);
    //        state = BattleState.ATTACK;
    //        bool isDead;
    //        float enemyCurHealth = enemyUnit.curHealth;

    //        if (tipo == 1)
    //        {
    //            audioManager.Play("Punch");
    //            isDead = enemyUnit.TakeDamage((int)(playerUnit.attack * 1));
    //            battleUI.dialogueText.text = "Você socou seu date!";
    //        }
    //        else if (tipo == 2)
    //        {
    //            audioManager.Play("Kick");
    //            isDead = enemyUnit.TakeDamage((int)(playerUnit.attack * 1.5));
    //            battleUI.dialogueText.text = "Você chutou seu date!";
    //        }
    //        else
    //        {
    //            battleUI.dialogueText.text = "Você usou ataque especial no seu date!";
    //            isDead = enemyUnit.TakeDamage((int)(playerUnit.attack * 2));
    //        }

    //        battleUI.enemyHUD.SetHP(enemyCurHealth, enemyUnit);

    //        yield return new WaitForSeconds(1.5f);

    //        if (isDead)
    //        {
    //            state = BattleState.WON;
    //            EndBattle();
    //        }
    //        else
    //        {
    //            state = BattleState.ENEMYTURN;
    //            StartCoroutine(EnemyTurn());
    //        }
    //    }
    //    else
    //        audioManager.Play("Click");
    //}
    public IEnumerator PlayerAttack()
    {
        battleUI.CombatPanel.SetActive(false);
        battleUI.DecisionPanel.SetActive(true);
        state = BattleState.ATTACK;
        int counter = playerActions.Count;
        for (int i = 0; i < counter; i++)
        {
            battleUI.dialogueText.text = "Executando...";
            bool isDead = false;
            float enemyCurHealth = enemyUnit.curHealth;
            yield return new WaitForSeconds(1f);
            if (playerActions[0] == 1)
            {
                audioManager.Play("Punch");
                print("atk "+enemyUnit.defense);
                print("def " + playerUnit.attack);
                print(playerUnit.attack * 2);
                print(enemyUnit.defense * 0.5f);
                
                print(((playerUnit.attack * 2) / (enemyUnit.defense * 0.5f)));
                isDead = enemyUnit.TakeDamage((playerUnit.attack * 2) / (enemyUnit.defense * 0.5f));
                battleUI.dialogueText.text = "Você socou seu date!";
            }
            else if (playerActions[0] == 2)
            {
                audioManager.Play("Kick");
                isDead = enemyUnit.TakeDamage((1.5f * playerUnit.attack * 2) / (enemyUnit.defense * 0.5f));
                battleUI.dialogueText.text = "Você chutou seu date!";
            }
            else if (playerActions[0] == 3)
            {
                battleUI.dialogueText.text = "Você esta se defendendo!";
            }
            else if (playerActions[0] == 5)
            {
                battleUI.dialogueText.text = "Você descansou e vai recuperar mais energia!";
                playerUnit.GiveEnergy(5);
            }
            if (playerActions[0] != 3)
            {
                playerActions.Remove(playerActions[0]);
                battleUI.enemyHUD.SetHP(enemyCurHealth, enemyUnit);
                battleUI.SetActionsHUD(playerActions);
            }
            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            yield return new WaitForSeconds(1f);
        }
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        battleUI.dialogueText.text = enemyUnit.cBase.name + " esta te dando um socou!";
        yield return new WaitForSeconds(1f);
        audioManager.Play("Punch");
        bool isDead;
        float playerCurHealth = playerUnit.curHealth;

        if (playerActions.Contains(3))
        {
            int damage = (int)(((2 * enemyUnit.attack) / (playerUnit.defense * 0.5f)) * 0.2f);
            damage = Mathf.Clamp(damage, 1, 99999);
            isDead = playerUnit.TakeDamage(damage);
            battleUI.playerHUD.SetHP(playerCurHealth, playerUnit);
        }
        else
        {
            isDead = playerUnit.TakeDamage((2 * enemyUnit.attack) / (playerUnit.defense * 0.5f));
            battleUI.playerHUD.SetHP(playerCurHealth, playerUnit);
        }


        yield return new WaitForSeconds(0.5f);

        if (playerActions.Contains(3))
        {
            audioManager.Play("ShieldDown");
            playerActions.Remove(3);
            battleUI.SetActionsHUD(playerActions);
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
        StopAllCoroutines();
        if (state == BattleState.WON)
        {
            battleUI.DecisionPanel.SetActive(true);
            battleUI.DecisionQuitButton.SetActive(true);
            battleUI.wonDatePanel.SetActive(true);
            battleUI.CombatPanel.SetActive(false);
            battleUI.dialogueText.text = "Você ganhou o encontro! " + enemyUnit.cBase.name + " esta totalmente na sua!";
            GameObject.FindGameObjectWithTag("persistentData").GetComponent<TinderData>().advanceCharacterDay();
            GameObject.FindGameObjectWithTag("persistentData").GetComponent<TinderData>().curDay += 1;
        }
        else if (state == BattleState.LOST)
        {
            battleUI.DecisionPanel.SetActive(true);
            battleUI.lostDatePanel.SetActive(true);
            battleUI.DecisionQuitButton.SetActive(true);
            battleUI.CombatPanel.SetActive(false);
            battleUI.dialogueText.text = "Você foi derrotado. " + enemyUnit.cBase.name + " esta indo embora insatisfeita.";
        }
    }

    void StartTurn()
    {
        float curEnergy = playerUnit.curEnergy;
        battleUI.dialogueText.text = "Escolha uma ação:";
        battleUI.DecisionAttackButton.SetActive(true);
        battleUI.DecisionQuitButton.SetActive(true);
        battleUI.playerHUD.SetEnergy(curEnergy, playerUnit);
    }

    void PlayerTurn()
    {
        float curEnergy = playerUnit.curEnergy;
        playerUnit.GiveEnergy(0);
        battleUI.playerHUD.SetEnergy(curEnergy, playerUnit);
        battleUI.DecisionAttackButton.SetActive(false);
        battleUI.DecisionQuitButton.SetActive(false);
        battleUI.DecisionPanel.SetActive(false);
        battleUI.CombatPanel.SetActive(true);
    }

    //public IEnumerator PlayerHeal()
    //{
    //    float playerCurHealth = enemyUnit.curHealth;
    //    state = BattleState.HEAL;
    //    playerUnit.Heal(5);

    //    battleUI.playerHUD.SetHP(playerCurHealth, playerUnit);
    //    battleUI.dialogueText.text = "You feel renewed strength!";

    //    yield return new WaitForSeconds(2f);

    //    state = BattleState.ENEMYTURN;
    //    StartCoroutine(EnemyTurn());
    //}

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
