using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class ContactInformation : MonoBehaviour
{
    public new TextMeshProUGUI name;

    public GameObject image;

    public GameObject button;

    public void CreateContact(CharacterBase character) {
        name.text = character.Name;
        image.GetComponent<Image>().sprite = character.Sprite;
        button.SetActive(true);
    }

    public void OpenMessage(GameObject panel) {
        panel.SetActive(true);
    }

    public void CloseMessage(GameObject panel) {
        panel.SetActive(false);
    }
}
