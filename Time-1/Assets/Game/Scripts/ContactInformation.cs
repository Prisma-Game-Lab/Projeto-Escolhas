using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ContactInformation : MonoBehaviour
{
    public new TextMeshProUGUI name;

    public GameObject image;

    public CharacterBase character;

    public void CreateContact() {
        name.text = character.Name;
        image.GetComponent<Image>().sprite = character.Sprite;
    }

    public void OpenMessage() {
        

    }
}
