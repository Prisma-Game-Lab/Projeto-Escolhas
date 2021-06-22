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

    public int curDay;
    //[HideInInspector]
    public int elfaDay, humanoDay, orcDay, sereiaDay, carneiraDay;
    [HideInInspector] public int matchesNumber;
    private AppSave appSave;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("persistentData");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        playerStats = GameObject.FindGameObjectWithTag("persistentData").GetComponent<playerStats>();
        matchesNumber = 0;
        curDay = 1;
    }

    public void advanceCharacterDay()
    {
        appSave = SaveSystem.GetInstance().appSave;
        print("antes");
        print(elfaDay);
        print(humanoDay);
        print(sereiaDay);
        print(orcDay);
        playerStats.availableStatsPoints = 100f;
        if (appSave.elfaEndDay) {
            elfaDay++;
            appSave.elfaEndDay = false;
        }
        if (appSave.humanoEndDay) {
            humanoDay++;
            appSave.humanoEndDay = false;
        }
        if (appSave.sereiaEndDay) {
            sereiaDay++;
            appSave.sereiaEndDay = false;
        }
        if (appSave.orcEndDay) {
            orcDay++;
            appSave.orcEndDay = false;
        }
        SaveSystem.GetInstance().SaveState();
        //if (combatCharacter.race == CharacterBase.CharacterRace.Carneira)
            //carneiraDay++;
        print("depois");
        print(elfaDay);
        print(humanoDay);
        print(sereiaDay);
        print(orcDay);
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
