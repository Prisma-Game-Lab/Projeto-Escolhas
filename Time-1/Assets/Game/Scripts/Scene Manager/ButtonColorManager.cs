using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonColorManager : MonoBehaviour
{
    public Sprite pressedColor;

    private bool pressedState;
    private bool unpressedState;
    private Sprite unpressedColor;
    private Image buttonImage;
    private Toggle toggle;
    void Start()
    {
        pressedState = false;
        unpressedState = false;
        buttonImage = GetComponent<Image>();
        unpressedColor = buttonImage.sprite;
        toggle = GetComponent<Toggle>();
    }
    private void Update()
    {
        if (toggle.isOn && !pressedState)
        {
            buttonImage.sprite = pressedColor;
            unpressedState = false;
            pressedState = true;
        }
        else if (!toggle.isOn && !unpressedState)
        {
            buttonImage.sprite = unpressedColor;
            unpressedState = true;
            pressedState = false;
        }
    }
}
