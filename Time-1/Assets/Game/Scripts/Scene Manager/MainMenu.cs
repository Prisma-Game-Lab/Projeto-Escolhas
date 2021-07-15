using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    private AudioManager audioManager;

    private void Start()
    {
        audioManager = GetComponent<AudioManager>();
    }

    public void GoToApp()
    {
        audioManager.Play("Click");
        SceneManager.LoadScene("App");
    }

    public void TutorialOn(GameObject canvas)
    {
        audioManager.Play("Click");
        canvas.SetActive(true);
    }

    public void StartAppTutorial(bool tutorialOn)
    {
        audioManager.Play("Click");
        GameObject.FindGameObjectWithTag("tutorialOnOff").GetComponent<tutorialOnOff>().tutorialOn = tutorialOn;
        SceneManager.LoadSceneAsync("App");
    }

    public void GoToCredits(GameObject canvas) {
        audioManager.Play("Click");
        canvas.gameObject.SetActive(true);
    }
    public void GoToMenu(GameObject canvas) {
        audioManager.Play("Click");
        canvas.gameObject.SetActive(false);
    }

    public void GoToItch() {
        audioManager.Play("Click");
        Application.OpenURL("https://prismagamelab.itch.io/");
    }

    public void GoToTwitter() {
        audioManager.Play("Click");
        Application.OpenURL("https://twitter.com/PrismaGameLab/");
    }

    public void GoToInstagram() {
        audioManager.Play("Click");
        Application.OpenURL("https://instagram.com/prismagamelab/");
    }

    public void GoToDiscord() {
        audioManager.Play("Click");
        Application.OpenURL("https://discord.com/invite/3TdQm3rEe4");
    }

    public void GoToFacebook() {
        audioManager.Play("Click");
        Application.OpenURL("https://www.facebook.com/prismagamelab/");
    }


}
