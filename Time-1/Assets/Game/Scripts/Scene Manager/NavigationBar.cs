using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class NavigationBar : MonoBehaviour
{
    public GameObject home;
    private GameObject _name;

    void Start()
    {
        _name = home;
    }

    public void SetActiveCanvas(GameObject canvas)
    {
        _name.gameObject.SetActive(false);
        canvas.gameObject.SetActive(true);
        _name = canvas;

    }
}
