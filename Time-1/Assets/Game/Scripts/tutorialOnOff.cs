using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class tutorialOnOff : MonoBehaviour
{
    [HideInInspector] public bool tutorialOn;
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
    }
}
