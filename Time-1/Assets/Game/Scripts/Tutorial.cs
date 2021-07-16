using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tutorial : MonoBehaviour
{
    //[HideInInspector]
    public bool tutorialOn;

    private void Awake()
    {
        tutorialOnOff tutorialInfo = GameObject.FindGameObjectWithTag("tutorialOnOff").GetComponent<tutorialOnOff>();
        this.tutorialOn = tutorialInfo.tutorialOn;
        Destroy(tutorialInfo.gameObject);
    }

    public void OpenTutorial(GameObject canvas) {
        canvas.gameObject.SetActive(true);
    }

}
