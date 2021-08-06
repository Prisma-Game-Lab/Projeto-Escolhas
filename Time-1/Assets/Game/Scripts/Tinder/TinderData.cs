using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinderData : MonoBehaviour
{
    public List<CharacterBase> tinderCharacters;
    public List<CharacterBase> allCharacters;

    [HideInInspector] public List<CharacterBase> curContacts = new List<CharacterBase>();

    public CharacterBase combatCharacter;

    private playerStats playerStats;
    private AppSave appSave;

    public int enemyStatsPerDay;
    public int curDay;

    [HideInInspector]
    public int elfaDay, humanoDay, orcDay, sereiaDay, carneiraDay;
    [HideInInspector] public int matchesNumber;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("persistentData");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
        appSave = SaveSystem.GetInstance().appSave;
        if (appSave.matchesNumber > 0)
        {
            tinderCharacters.Clear();
            foreach (CharacterBase character in appSave.tinderCharacters)
            {
                tinderCharacters.Add(character);
            }
            curContacts.Clear();
            foreach (CharacterBase contact in appSave.curContacts)
            {
                curContacts.Add(contact);
            }
        }
        else
        {
            foreach (CharacterBase character in tinderCharacters)
            {
                appSave.tinderCharacters.Add(character);
            }
        }
        if (appSave.curDay == 0)
            curDay = 1;
        else
            curDay = appSave.curDay;
    }

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("persistentData").GetComponent<playerStats>();
        matchesNumber = appSave.matchesNumber;
    }

    public void advanceCharacterDay()
    {
        playerStats.availableStatsPoints = 100f;
        if (appSave.elfaEndDay) {
            if (appSave.curDay < 6)
                elfaDay++;
            appSave.elfaEndDay = false;
            appSave.elfaBattle = false;
            appSave.elfaJson = "";
        }
        if (appSave.humanoEndDay) {
            if (appSave.curDay < 6)
                humanoDay++;
            appSave.humanoEndDay = false;
            appSave.humanoBattle = false;
            appSave.humanoJson = "";
        }
        if (appSave.sereiaEndDay) {
            if (appSave.curDay < 6)
                sereiaDay++;
            appSave.sereiaEndDay = false;
            appSave.sereiaBattle = false;
            appSave.sereiaJson = "";
        }
        if (appSave.orcEndDay) {
            if (appSave.curDay < 6)
                orcDay++;
            appSave.orcEndDay = false;
            appSave.orcBattle = false;
            appSave.orcJson = "";
        }
        SaveSystem.GetInstance().SaveState();
    }

    public int getCharacterDay(CharacterBase character)
    {
        if (character.race == CharacterBase.CharacterRace.Elfa)
            return elfaDay;
        else if (character.race == CharacterBase.CharacterRace.Humano)
            return humanoDay;
        else if (character.race == CharacterBase.CharacterRace.Sereia)
            return sereiaDay;
        else if (character.race == CharacterBase.CharacterRace.Orc)
            return orcDay;
        else
            return carneiraDay;
    }
}
