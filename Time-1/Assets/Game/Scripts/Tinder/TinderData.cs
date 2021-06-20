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
        playerStats.availableStatsPoints = 100f;
        if (combatCharacter.race == CharacterBase.CharacterRace.Elfa)
            elfaDay ++;
        else if (combatCharacter.race == CharacterBase.CharacterRace.Humano)
            humanoDay++;
        else if (combatCharacter.race == CharacterBase.CharacterRace.Sereia)
            sereiaDay++;
        else if (combatCharacter.race == CharacterBase.CharacterRace.Orc)
            orcDay++;
        else if (combatCharacter.race == CharacterBase.CharacterRace.Carneira)
            carneiraDay++;
    }
}
