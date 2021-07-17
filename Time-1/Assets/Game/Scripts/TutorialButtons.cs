using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TutorialButtons : MonoBehaviour
{
    public GameObject tutorialMinigame1;
    public GameObject tutorialMinigame2;
    public GameObject tutorialMinigame3;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
    }

    public void openCloseTutorial(int minigameNumber)
    {
        audioManager.Play("Click");
        if (minigameNumber == 1)
            tutorialMinigame1.SetActive(!tutorialMinigame1.activeSelf);
        else if (minigameNumber == 2)
            tutorialMinigame2.SetActive(!tutorialMinigame2.activeSelf);
        else
            tutorialMinigame3.SetActive(!tutorialMinigame3.activeSelf);
    }
}
