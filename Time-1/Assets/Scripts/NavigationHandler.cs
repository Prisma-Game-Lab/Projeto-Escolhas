using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NavigationHandler : MonoBehaviour
{
    public Canvas home;
    public Canvas training;
    public Canvas stats;
    public Canvas message;
    private Canvas _name;

    void Start() {
        _name = home;
    }
    
    public void SetActiveHome() {
        if (_name != home) { 
            home.gameObject.SetActive(true);
            _name.gameObject.SetActive(false);
            _name = home;
        }
    }

    public void SetActiveTraining() {
        training.gameObject.SetActive(true);
        _name.gameObject.SetActive(false);
        _name = training;
    }

    public void SetActiveStats() {
        stats.gameObject.SetActive(true);
        _name.gameObject.SetActive(false);
        _name = stats;
    }

    public void SetActiveMessage() {
        message.gameObject.SetActive(true);
        _name.gameObject.SetActive(false);
        _name = message;
    }

    public void GoToMenu() {
        SceneManager.LoadScene("MainMenu_Scene");
    }

}
