using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheckAffinity : MonoBehaviour
{
    private AppSave appSave;

    public int minAffinityElfa;
    public int minAffinityOrc;
    public int minAffinitySereia;
    public int minAffinityHumano;
    private int minAffinity;

    void Start() {
        appSave = SaveSystem.GetInstance().appSave;
        if (name == "Amarillys") 
            minAffinity = minAffinityElfa;
        else if (name == "Bruce")
            minAffinity = minAffinityOrc;
        else if (name == "Clarissa")
            minAffinity = minAffinitySereia;
        else
            minAffinity = minAffinityHumano;
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
