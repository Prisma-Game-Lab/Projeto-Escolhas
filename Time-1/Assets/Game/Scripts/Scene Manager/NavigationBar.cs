using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NavigationBar : MonoBehaviour
{
    public Canvas home;
    private Canvas _name;

    void Start()
    {
        _name = home;
    }

    public void SetActiveCanvas(Canvas canvas)
    {
        _name.gameObject.SetActive(false);
        canvas.gameObject.SetActive(true);
        _name = canvas;

    }

    public void GoToMenu()
    {
        SceneManager.LoadScene("MainMenu_Scene");
    }

    public void GoToCombat()
    {
        SceneManager.LoadScene("Combat_Scene");
    }

}
