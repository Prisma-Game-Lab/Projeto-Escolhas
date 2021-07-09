using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NavigationBar : MonoBehaviour
{
    public GameObject home;
    private GameObject _name;
    private AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindObjectOfType<AudioManager>();
        _name = home;
    }

    public void SetActiveCanvas(GameObject canvas)
    {
        audioManager.Play("Click");
        _name.gameObject.SetActive(false);
        canvas.gameObject.SetActive(true);
        _name = canvas;

    }

    public void openCloseSettings(GameObject canvas)
    {
        audioManager.Play("Click");
        if (canvas.activeSelf)
            canvas.gameObject.SetActive(false);
        else
            canvas.gameObject.SetActive(true);
    }

}
