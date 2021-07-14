using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;
using UnityEngine.Rendering;

public class PrismaCutscene : MonoBehaviour
{
    public VideoClip video;

    private void Awake()
    {
        StartCoroutine(WaitVideoOver());
    }

    private void Update()
    {
        if(Input.GetMouseButtonDown(0))
        {
            SceneManager.LoadScene("MainMenu_Scene", LoadSceneMode.Single);
        }
    }
    private IEnumerator WaitVideoOver()
    {
        yield return new WaitForSeconds((float)video.length);
        SceneManager.LoadScene("MainMenu_Scene", LoadSceneMode.Single);
    }

}