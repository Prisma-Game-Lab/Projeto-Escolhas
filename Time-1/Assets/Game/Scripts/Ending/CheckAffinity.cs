using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class CheckAffinity : MonoBehaviour
{
    private AppSave appSave;
    public int minAffinityElfa;
    public int minAffinityOrc;
    public int minAffinitySereia;
    public int minAffinityHumano;
    private int minAffinity;
    private List<int> min = new List<int>();

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
        min.Add(minAffinityElfa);
        min.Add(minAffinityOrc);
        min.Add(minAffinitySereia);
        min.Add(minAffinityHumano);
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

    public void ListNumber(string name) {
        if (name == "Amarillys") 
            appSave.love = 0;
        else if (name == "Bruce")
            appSave.love = 1;
        else if (name == "Clarissa")
            appSave.love = 2;
        else
            appSave.love = 3;
        SaveSystem.GetInstance().SaveState();
    }

    public bool HasAffinityWithSomeone(string name) {
        int checkPoints;
        if (name == "Amarillys") 
            checkPoints = appSave.elfaPoints;
        else if (name == "Bruce")
            checkPoints = appSave.orcPoints;
        else if (name == "Clarissa")
            checkPoints = appSave.sereiaPoints;
        else
            checkPoints = appSave.humanoPoints;
        foreach (int i in min) {
            if (checkPoints >= i)
                return true;
        }
        return false;
    }

    public void RenewDay() {
        SceneManager.LoadScene("App");
    }

    public void NewGame() {
        SaveSystem.GetInstance().NewGame();
        SaveSystem.DeleteSaveFile();
        Destroy(GameObject.FindGameObjectWithTag("persistentData"));
        SceneManager.LoadScene("MainMenu_Scene", LoadSceneMode.Single);
    }

}
