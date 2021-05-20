using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TinderManager : MonoBehaviour
{
    public TextMeshProUGUI day_txt;
    public TextMeshProUGUI tinderCharacterName_txt;
    public Image tinderImage;
    private int curIndex;
    TinderData tinderData;
    private contactsManager contactManager;

    private void Start()
    {
        tinderData = GameObject.FindGameObjectWithTag("persistentData").GetComponent<TinderData>();
        contactManager = GetComponent<contactsManager>();
        curIndex = 0;
        tinderImage.sprite = tinderData.tinderCharacters[0].tinderImage;
        tinderCharacterName_txt.text = tinderData.tinderCharacters[0].name;
        day_txt.text = "Dia " + tinderData.curDay;
    }

    public void OnNoButtonPressed()
    {
        curIndex++;
        if (curIndex > tinderData.tinderCharacters.Count - 1)
            curIndex = 0;
        tinderImage.sprite = tinderData.tinderCharacters[curIndex].tinderImage;
        tinderCharacterName_txt.text = tinderData.tinderCharacters[curIndex].name;
    }
    public void OnYesButtonPressed()
    {
        if (tinderData.curDay > tinderData.matchesNumber && tinderData.tinderCharacters[curIndex].race != CharacterBase.CharacterRace.Humano)
        {
            contactManager.createContact(tinderData.tinderCharacters[curIndex]);
            tinderData.tinderCharacters.Remove(tinderData.tinderCharacters[curIndex]);
            curIndex = 0;
            tinderImage.sprite = tinderData.tinderCharacters[curIndex].tinderImage;
            tinderCharacterName_txt.text = tinderData.tinderCharacters[curIndex].name;
            tinderData.matchesNumber += 1;
        }
    }
    public void OnBioButtonPressed()
    {
        //abre UI da bio
    }
}
