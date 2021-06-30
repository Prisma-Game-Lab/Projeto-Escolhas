using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAffinity : MonoBehaviour
{
    private AppSave appSave;

    public int minAffinity;

    //1 - Elfa
    //2 - Orc
    //3 - Sereia
    //4 - Humano
    //5 - Ninguem

    void Start() {
        appSave = SaveSystem.GetInstance().appSave;
    }

    public bool CheckIfHasAffinity(string name) {
        int checkPoints;
        if (name == "Amarillys") 
            checkPoints = appSave.elfaPoints;
        else if (name == "Bruce")
            checkPoints = appSave.orcPoints;
        else if (name == "Clarissa")
            checkPoints = appSave.sereiaPoints;
        else
            checkPoints = appSave.humanoPoints;
        if (checkPoints >= minAffinity)
            return true;
        return false;
    }
}
