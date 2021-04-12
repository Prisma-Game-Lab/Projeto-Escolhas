using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{

    public void Minigame1() {
        SceneManager.LoadScene("Minigame01");
        
    }

    public void App() {
        Time.timeScale = 1f;
        Pause.isPaused = false;
        SceneManager.LoadScene("App");
        //MinigameManager.MagicTouch();
        //draw.Minigame();
    }

}
