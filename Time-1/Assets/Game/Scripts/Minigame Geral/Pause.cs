using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pause : MonoBehaviour
{
    public static bool isPaused;

    public GameObject pauseMenu;
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
    }

    public void PauseResume() {
        audioManager.Play("Click");
        if (!isPaused) {
            pauseMenu.SetActive(true);
            Time.timeScale = 0f;
            isPaused = true;
        }
        else {
            pauseMenu.SetActive(false);
            Time.timeScale = 1f;
            isPaused = false;
        }
    }


}
