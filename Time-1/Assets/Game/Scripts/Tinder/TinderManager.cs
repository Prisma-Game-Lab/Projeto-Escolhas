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

    public Image bioProfileImage;
    public TextMeshProUGUI bioName_txt;
    public TextMeshProUGUI bioWork_txt;
    public TextMeshProUGUI bioDescription_txt;

    private int curIndex;
    TinderData tinderData;
    private contactsManager contactManager;
    private AudioManager audioManager;
    public TextMeshProUGUI tutorial;

    private void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        tinderData = GameObject.FindGameObjectWithTag("persistentData").GetComponent<TinderData>();
        contactManager = GetComponent<contactsManager>();
        curIndex = 0;
        tinderImage.sprite = tinderData.tinderCharacters[0].tinderImage;
        tinderCharacterName_txt.text = tinderData.tinderCharacters[0].name;
        day_txt.text = "Dia " + tinderData.curDay;
        if (tinderData.tinderCharacters.Count < 8) {
            tutorial.gameObject.SetActive(false);
        }
    }

    public void OnNoButtonPressed()
    {
        audioManager.Play("Reject");
        curIndex++;
        if (curIndex > tinderData.tinderCharacters.Count - 1)
            curIndex = 0;
        tinderImage.sprite = tinderData.tinderCharacters[curIndex].tinderImage;
        tinderCharacterName_txt.text = tinderData.tinderCharacters[curIndex].name;
    }
    public void OnYesButtonPressed()
    {
        tutorial.gameObject.SetActive(false);
        if (tinderData.tinderCharacters[curIndex].race != CharacterBase.CharacterRace.Fake)
        {
            contactManager.chatButtonPopUpImage.gameObject.SetActive(true);
            audioManager.Play("Match");
            contactManager.createContact(tinderData.tinderCharacters[curIndex]);
            tinderData.tinderCharacters.Remove(tinderData.tinderCharacters[curIndex]);
            if(curIndex>=tinderData.tinderCharacters.Count)
                curIndex = 0;
            tinderImage.sprite = tinderData.tinderCharacters[curIndex].tinderImage;
            tinderCharacterName_txt.text = tinderData.tinderCharacters[curIndex].name;
            tinderData.matchesNumber += 1;
        }
        else
            OnNoButtonPressed();

    }
    public void OnBioButtonPressed()
    {
        //abre UI da bio
        bioDescription_txt.text = tinderData.tinderCharacters[curIndex].bio;
        bioWork_txt.text = tinderData.tinderCharacters[curIndex].bioWork;
        bioName_txt.text = tinderData.tinderCharacters[curIndex].name;
        bioProfileImage.sprite = tinderData.tinderCharacters[curIndex].BioImage;
    }
}
