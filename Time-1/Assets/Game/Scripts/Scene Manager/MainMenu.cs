using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenu : MonoBehaviour
{
    
    public void GoToApp() {
        SceneManager.LoadScene("App");
    }

    public void GoToCredits(GameObject canvas) {
        canvas.gameObject.SetActive(true);
    }
    public void GoToMenu(GameObject canvas) {
        canvas.gameObject.SetActive(false);
    }

    public void GoToItch() {
        Application.OpenURL("https://prismagamelab.itch.io/");
    }

    public void GoToTwitter() {
        Application.OpenURL("https://twitter.com/PrismaGameLab/");
    }

    public void GoToInstagram() {
        Application.OpenURL("https://instagram.com/prismagamelab/");
    }

    public void GoToLinkedin() {
        Application.OpenURL("https://www.linkedin.com/company/prismagamelab/");
    }

    public void GoToFacebook() {
        Application.OpenURL("https://www.facebook.com/prismagamelab/");
    }


}
