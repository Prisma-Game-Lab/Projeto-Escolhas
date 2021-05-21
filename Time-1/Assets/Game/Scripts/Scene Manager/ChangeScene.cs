using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void Minigame1() {
        audioManager.Play("Click");
        SceneManager.LoadScene("Minigame01");
    }

    public void Minigame2()
    {
        audioManager.Play("Click");
        SceneManager.LoadScene("Minigame02");
    }
    public void Minigame3()
    {
        audioManager.Play("Click");
        SceneManager.LoadScene("Minigame03");
    }

    public void App() {
        audioManager.Play("Click");
        Time.timeScale = 1f;
        Pause.isPaused = false;
        SceneManager.LoadScene("App");
        //MinigameManager.MagicTouch();
        //draw.Minigame();
    }
    public void GoToCombat()
    {
        audioManager.Play("Click");
        SceneManager.LoadScene("Combat_Scene");
    }
   }
