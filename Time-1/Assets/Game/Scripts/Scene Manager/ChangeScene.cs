using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ChangeScene : MonoBehaviour
{
    public Draw draw;

    public void Minigame1() {
        SceneManager.LoadScene("Minigame01");
        //MinigameManager.MagicTouch();
        draw.Minigame();
    }

}
