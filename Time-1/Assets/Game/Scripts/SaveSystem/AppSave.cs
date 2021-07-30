using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Save", menuName = "Save/Scriptable Obj")]
public class AppSave : ScriptableObject
{
    public List<CharacterBase> tinderCharacters = new List<CharacterBase>();
    public List<CharacterBase> curContacts = new List<CharacterBase>();
    public List<string> elfa = new List<string>();
    public List<string> humano = new List<string>();
    public List<string> sereia = new List<string>();
    public List<string> orc = new List<string>();
    public string elfaJson;
    public string humanoJson;
    public string sereiaJson;
    public string orcJson;
    public bool elfaEndDay;
    public bool humanoEndDay;
    public bool sereiaEndDay;
    public bool orcEndDay;
    public int elfaPoints;
    public int humanoPoints;
    public int sereiaPoints;
    public int orcPoints;
    public bool tutorialTinder;
    public bool tutorialMinigame1;
    public bool tutorialMinigame2;
    public bool tutorialMinigame3;
    public bool askTutorialOn;
    public int curDay;
    public int love;
    public int matchesNumber;
    public bool elfaBattle;
    public bool orcBattle;
    public bool sereiaBattle;
    public bool humanoBattle;
    public float playerAtk;
    public float playerDef;
    public float playerVel;
    public List<CharacterBase> blockedCharacters;

}
