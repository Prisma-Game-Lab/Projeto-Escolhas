using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SettingsPage : MonoBehaviour
{

    void Start()
    {

    }

    // Update is called once per frame
    public void OpenPage(GameObject canvas) {
        canvas.gameObject.SetActive(true);
        
    }

    public void ClosePage(GameObject canvas) {
        canvas.gameObject.SetActive(false);
        
    }

    public void OpenCloseSettings(GameObject canvas) {
        if (!canvas.activeSelf)
            canvas.gameObject.SetActive(true);
        else
            canvas.gameObject.SetActive(false);
        
    }

    public void NewGame() {
        SaveSystem.GetInstance().NewGame();
        SaveSystem.DeleteSaveFile();
    }

}
