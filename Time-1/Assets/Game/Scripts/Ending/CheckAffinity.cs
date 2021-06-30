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

    public List<int> CheckIfHasAffinity() {
        List<int> affinityNumber = new List<int>();
        for (int i=0; i < 4; i++) {
            affinityNumber.Add(-1);
        }

        if (appSave.elfaPoints >= minAffinity)
            affinityNumber[0] = 1;

        return affinityNumber;
    }
}
