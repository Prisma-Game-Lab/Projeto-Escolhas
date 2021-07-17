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
    private int curTurn; 
    public int maxTurn;

    public BattleState state;
    [HideInInspector] public bool defenseOn;

    private BattleUIManager battleUI;
    private AudioManager audioManager;

    [HideInInspector] public List<int> playerActions;
    [HideInInspector] public List<int> enemyActions;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        battleUI = GetComponent<BattleUIManager>();
        curTurn = 1;
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
        battleUI.scenerioImage.sprite = enemyUnit.cBase.scenerio;
        battleUI.enemyImage.sprite = enemyUnit.cBase.combatImage;

        battleUI.StartCoroutine(battleUI.showText(enemyUnit.cBase.name + " se aproxima..."));

        battleUI.playerHUD.SetHUD(playerUnit);
        battleUI.enemyHUD.SetHUD(enemyUnit);
        battleUI.turnText.text = "TURNO\n" + curTurn + "/" + maxTurn;
        battleUI.DecisionAttackButton.SetActive(false);
        battleUI.DecisionQuitButton.SetActive(false);
        yield return new WaitForSeconds(1.5f);

        state = BattleState.PLAYERTURN;
        StartTurn();
    }

    public IEnumerator PlayerAttack()
    {
        battleUI.CombatPanel.SetActive(false);
        battleUI.DecisionPanel.SetActive(true);
        battleUI.attackSlider.value = 0;
        state = BattleState.ATTACK;
        int counter = playerActions.Count;

        //battleUI.StartCoroutine(battleUI.showText("Executando..."));

        //yield return new WaitForSeconds(1f);

        for (int i = 0; i < counter; i++)
        {
            bool isDead = false;
            float damageReduction = 1f;
            float enemyCurHealth = enemyUnit.curHealth;
            if (enemyActions.Contains(3) && enemyUnit.currentShieldHits <= 2)
            {
                if (enemyUnit.currentShieldHits == 0)
                    damageReduction = 0.2f;
                else if (enemyUnit.currentShieldHits == 1)
                    damageReduction = 0.4f;
                else
                    damageReduction = 0.6f;
                enemyUnit.currentShieldHits += 1;
            }
            if (playerActions[0] == 1)
            {
                audioManager.Play("Punch");
                //print("atk "+enemyUnit.defense);
                //print("def " + playerUnit.attack);
                //print(playerUnit.attack * 2 * damageReduction);
                //print(enemyUnit.defense * 0.5f);
                
                //print(((playerUnit.attack * 2) / (enemyUnit.defense * 0.5f)) * damageReduction);
                isDead = enemyUnit.TakeDamage(((playerUnit.attack * 2) / (enemyUnit.defense * 0.5f))*damageReduction);
                battleUI.StartCoroutine(battleUI.showText("Você socou seu date!"));
            }
            else if (playerActions[0] == 2)
            {
                audioManager.Play("Kick");
                isDead = enemyUnit.TakeDamage(((1.5f * playerUnit.attack * 2) / (enemyUnit.defense * 0.5f)) * damageReduction);
                battleUI.StartCoroutine(battleUI.showText("Você chutou seu date!"));
            }
            else if (playerActions[0] == 3)
            {
                battleUI.StartCoroutine(battleUI.showText("Você esta se defendendo!"));
            }
            else if (playerActions[0] == 5)
            {
                battleUI.StartCoroutine(battleUI.showText("Você vai recuperar 7 de energia!"));
                playerUnit.GiveEnergy(5);
            }
            if (playerActions[0] != 3)
            {
                playerActions.Remove(playerActions[0]);
                battleUI.enemyHUD.SetHP(enemyCurHealth, enemyUnit);
                battleUI.SetActionsHUD(playerActions);
            }
            yield return new WaitForSeconds(0.7f);

            if (isDead)
            {
                state = BattleState.WON;
                EndBattle();
            }
            yield return new WaitForSeconds(1f);
        }
        if (enemyActions.Contains(3))
        {
            //audioManager.Play("ShieldDown");
            enemyActions.Remove(3);
            enemyUnit.currentShieldHits = 0;
        }
        state = BattleState.ENEMYTURN;
        StartCoroutine(EnemyTurn());
    }

    IEnumerator EnemyTurn()
    {
        battleUI.StartCoroutine(battleUI.showText(enemyUnit.cBase.name + " está se preparando..."));
        fillEnemyActions();
        yield return new WaitForSeconds(2f);

        int counter = enemyActions.Count;
        for (int i = 0; i < counter; i++)
        {
            bool isDead = false;
            float damage;
            float damageReduction = 1f;
            float playerCurHealth = playerUnit.curHealth;
            if (playerActions.Contains(3) && playerUnit.currentShieldHits<=2)
            {
                if (playerUnit.currentShieldHits == 0)
                    damageReduction = 0.2f;
                else if (playerUnit.currentShieldHits == 1)
                    damageReduction = 0.4f;
                else
                    damageReduction = 0.6f;
                playerUnit.currentShieldHits += 1;
            }
            if (enemyActions[0] == 1)
            {
                battleUI.StartCoroutine(battleUI.showText(enemyUnit.cBase.name + " te deu um soco!"));
                audioManager.Play("Punch");
                damage = (((2 * enemyUnit.attack) / (playerUnit.defense * 0.5f)) * damageReduction);
                isDead = playerUnit.TakeDamage(damage);
            }
            else if (enemyActions[0] == 2)
            {
                battleUI.StartCoroutine(battleUI.showText(enemyUnit.cBase.name + " te deu um chute!"));
                audioManager.Play("Kick");
                damage = (((1.5f * enemyUnit.attack * 2) / (playerUnit.defense * 0.5f)) * damageReduction);
                isDead = playerUnit.TakeDamage(damage);
            }
            else if (enemyActions[0] == 3)
            {
                battleUI.StartCoroutine(battleUI.showText(enemyUnit.cBase.name + " esta se defendendo!"));
            }
            else if (enemyActions[0] == 5)
            {
                battleUI.StartCoroutine(battleUI.showText(enemyUnit.cBase.name + " vai recuperar mais energia!"));
                enemyUnit.GiveEnergy(5);
            }
            if (enemyActions[0] != 3)
            {
                enemyActions.Remove(enemyActions[0]);
                battleUI.playerHUD.SetHP(playerCurHealth, playerUnit);
            }
            yield return new WaitForSeconds(1f);

            if (isDead)
            {
                state = BattleState.LOST;
                EndBattle();
            }
            yield return new WaitForSeconds(1f);
        }

        yield return new WaitForSeconds(0.5f);

        if (playerActions.Contains(3))
        {
            audioManager.Play("ShieldDown");
            playerActions.Remove(3);
            battleUI.SetActionsHUD(playerActions);
            playerUnit.currentShieldHits = 0;
            playerUnit.shieldsAvailable -= 1;
        }

        state = BattleState.PLAYERTURN;
        curTurn +=1;
        if (curTurn >= maxTurn)
        {
            if(playerUnit.curHealth/playerUnit.maxHealth > enemyUnit.curHealth/enemyUnit.maxHealth)
            {
                state = BattleState.WON;
            }
            else
            {
                state = BattleState.LOST;
            }
            EndBattle();
            yield return new WaitForSeconds(0.5f);
        }
        battleUI.turnText.text = "TURNO\n" + curTurn + "/" + maxTurn;
        PlayerTurn();
    }

    void EndBattle()
    {
        battleUI.DecisionQuitButton_text.text = "Sair";
        StopAllCoroutines();
        AddAffinity addAffinity = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<AddAffinity>();
        string tag = addAffinity.CharacterTag(enemyUnit.cBase.name);
        if (state == BattleState.WON)
        {
            battleUI.DecisionPanel.SetActive(true);
            battleUI.DecisionQuitButton.SetActive(true);
            battleUI.wonDatePanel.SetActive(true);
            Debug.Log("won date");
            battleUI.CombatPanel.SetActive(false);
            Debug.Log("combat");
            battleUI.StartCoroutine(battleUI.showText("Você ganhou o encontro! " + enemyUnit.cBase.name + " esta totalmente na sua!"));
            GameObject.FindGameObjectWithTag("persistentData").GetComponent<TinderData>().advanceCharacterDay();
            GameObject.FindGameObjectWithTag("persistentData").GetComponent<TinderData>().curDay += 1;
            addAffinity.AddPoints(tag, 2);
        }
        else if (state == BattleState.LOST)
        {
            battleUI.DecisionPanel.SetActive(true);
            battleUI.lostDatePanel.SetActive(true);
            Debug.Log("lost date");
            battleUI.DecisionQuitButton.SetActive(true);
            battleUI.CombatPanel.SetActive(false);
            Debug.Log("combat");
            battleUI.StartCoroutine(battleUI.showText("Você foi derrotado. " + enemyUnit.cBase.name + " esta indo embora insatisfeita."));
            GameObject.FindGameObjectWithTag("persistentData").GetComponent<TinderData>().advanceCharacterDay();
            GameObject.FindGameObjectWithTag("persistentData").GetComponent<TinderData>().curDay += 1;
            addAffinity.AddPoints(tag, 3);
        }

        if (GameObject.FindGameObjectWithTag("persistentData").GetComponent<TinderData>().curDay == 2) {
            CheckAffinity checkAffinity = GameObject.FindGameObjectWithTag("BattleManager").GetComponent<CheckAffinity>();
            if(checkAffinity.CheckIfHasAffinity(enemyUnit.cBase.name)) {
                checkAffinity.ListNumber(enemyUnit.cBase.name);
                SceneManager.LoadScene("TheEnd");
            }
            else {
                SceneManager.LoadScene("TheEnd");
            }
        }

    }

    void fillEnemyActions()
    {
        enemyActions.Clear();
        enemyUnit.curEnergy = Mathf.Clamp(enemyUnit.curEnergy += 2, 0, enemyUnit.maxEnergy);
        print("Energia agr: " + enemyUnit.curEnergy);
        if (enemyUnit.curEnergy <= enemyUnit.maxEnergy * 0.3)
        {
            enemyActions.Add(5);
            print("inimigo descansou");
        }
        else
        {
            bool enemyShieldOn = false;
            int energyToSpend = UnityEngine.Random.Range(Mathf.CeilToInt(enemyUnit.curEnergy / 2)+2, Mathf.CeilToInt(enemyUnit.curEnergy));
            print("energia para gastar: "+energyToSpend);
            if (energyToSpend <= 4)
                energyToSpend = 4;
            enemyUnit.curEnergy -= energyToSpend;

            if (energyToSpend >= 5 && UnityEngine.Random.Range(1, 101) <= 80 && enemyUnit.shieldsAvailable>0)
            {
                enemyUnit.shieldsAvailable -= 1;
                enemyShieldOn = true;
                energyToSpend -= 2;
            }
            while (energyToSpend >= 2)
            {
                print("energia para gastar: " + energyToSpend);
                int newAction = UnityEngine.Random.Range(1, 3);
                if (newAction == 1)
                {
                    energyToSpend -= 2;
                    enemyActions.Add(newAction);
                }
                else if (newAction == 2 && energyToSpend >= 4)
                {
                    energyToSpend -= 4;
                    enemyActions.Add(newAction);
                }
            }
            if (enemyShieldOn)
                enemyActions.Add(3);
            enemyUnit.curEnergy += energyToSpend;
        }
        for (int i = 0; i < enemyActions.Count; i++)
        {
            print("Acao "+i+": "+enemyActions[i]);
        }
        print("Energia Max: "+enemyUnit.maxEnergy);
        print("Energia Atual: " + enemyUnit.curEnergy);
    }

    void StartTurn()
    {
        float curEnergy = playerUnit.curEnergy;
        battleUI.StartCoroutine(battleUI.showText("Deseja iniciar encontro? "));
        battleUI.DecisionAttackButton.SetActive(true);
        battleUI.DecisionQuitButton.SetActive(true);
        battleUI.sliderImage.sprite = battleUI.sliderRestSprite;
        battleUI.playerHUD.SetEnergy(curEnergy, playerUnit);
        playerActions.Add(5);
        battleUI.SetActionsHUD(playerActions);
    }

    void PlayerTurn()
    {
        float curEnergy = playerUnit.curEnergy;
        playerUnit.GiveEnergy(0);
        battleUI.sliderImage.sprite = battleUI.sliderRestSprite;
        playerActions.Add(5);
        battleUI.SetActionsHUD(playerActions);
        battleUI.playerHUD.SetEnergy(curEnergy, playerUnit);
        battleUI.DecisionAttackButton.SetActive(false);
        battleUI.DecisionQuitButton.SetActive(false);
        battleUI.DecisionPanel.SetActive(false);
        battleUI.CombatPanel.SetActive(true);
    }

    public IEnumerator PlayerRun()
    {
        state = BattleState.RUN;
        battleUI.DecisionAttackButton.SetActive(false);
        battleUI.DecisionQuitButton.SetActive(false);
        battleUI.StartCoroutine(battleUI.showText("Saindo do encontro..."));
        //alguma animacao
        yield return new WaitForSeconds(0.5f);
        //sai do combate
        SceneManager.LoadScene("App");
    }

}
