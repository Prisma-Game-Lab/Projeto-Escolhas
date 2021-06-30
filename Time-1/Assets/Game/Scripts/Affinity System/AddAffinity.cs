using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AddAffinity : MonoBehaviour
{
    private AppSave appSave;

    public int battleLostAffinity;

    public int battleWonAffinity;

    public int messageAffinity;

    //1 - message
    //2 - won battle
    //3 - lost battle

    void Start() {
        appSave = SaveSystem.GetInstance().appSave;
    }

    public void AddPoints(string tag, int from) {
        int points;
        if (from == 1)
            points = messageAffinity;
        else if (from == 2)
            points = battleWonAffinity;
        else
            points = battleLostAffinity;
        if (tag == "Elf")
            appSave.elfaPoints += points;
        else if (tag == "Orc")
            appSave.orcPoints += points;
        else if (tag == "Sereia")
            appSave.sereiaPoints += points;
        else
            appSave.humanoPoints += points;
        SaveSystem.GetInstance().SaveState();
    }

    public string CharacterTag(string name) {
        string tag;
        if (name == "Amarillys") 
            tag = "Elf";
        else if (name == "Bruce")
            tag = "Orc";
        else if (name == "Clarissa")
            tag = "Sereia";
        else
            tag = "Humano";
        return tag;
    }
    
}
