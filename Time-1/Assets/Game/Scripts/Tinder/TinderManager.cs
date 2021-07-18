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
    public GameObject settingsAndDay;

    public GameObject matchAnimation;
    public GameObject whiteBackground;

    public GameObject tinderTutorial;
    
    private List<string> matchSound = new List<string>();

    private void Start()
    {
        GameObject persistentData = GameObject.FindGameObjectWithTag("persistentData");
        audioManager = FindObjectOfType<AudioManager>();
        tinderData = persistentData.GetComponent<TinderData>();
        contactManager = GetComponent<contactsManager>();
        curIndex = 0;
        tinderImage.sprite = tinderData.tinderCharacters[0].tinderImage;
        tinderCharacterName_txt.text = tinderData.tinderCharacters[0].name;
        day_txt.text = "DIA " + tinderData.curDay;
        matchSound.Clear();
        matchSound.Add("match3");
        matchSound.Add("match4");
        matchSound.Add("match5");
        matchSound.Add("match6");
        settingsAndDay.SetActive(true);
        if (tinderData.matchesNumber > 0)
        {
            tutorial.gameObject.SetActive(false);
        }
        tutorialOn();
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
        
        if (tinderData.tinderCharacters[curIndex].race != CharacterBase.CharacterRace.Fake)
        {
            tutorial.gameObject.SetActive(false);
            StartCoroutine(playAnimation());
            audioManager.Play("Match");
            contactManager.createContact(tinderData.tinderCharacters[curIndex]);
            StartCoroutine(nextTinderCharacter());
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

    private IEnumerator nextTinderCharacter()
    {
        yield return new WaitForSeconds(1.5f);
        contactManager.chatButtonPopUpImage.gameObject.SetActive(true);
        SaveSystem.GetInstance().appSave.tinderCharacters.Remove(tinderData.tinderCharacters[curIndex]);
        tinderData.tinderCharacters.Remove(tinderData.tinderCharacters[curIndex]);
        if (curIndex >= tinderData.tinderCharacters.Count)
            curIndex = 0;
        tinderImage.sprite = tinderData.tinderCharacters[curIndex].tinderImage;
        tinderCharacterName_txt.text = tinderData.tinderCharacters[curIndex].name;
        tinderData.matchesNumber += 1;
        SaveSystem.GetInstance().appSave.matchesNumber = tinderData.matchesNumber;
        SaveSystem.GetInstance().SaveState();
    }

    private void tutorialOn()
    {
        AppSave appsave = SaveSystem.GetInstance().appSave;
        if (appsave.tutorialTinder)
        {
            //yield return new WaitForSeconds(0.3f);
            tinderTutorial.SetActive(true);
            appsave.tutorialTinder = false;
            SaveSystem.GetInstance().SaveState();
        }
    }

    private IEnumerator playAnimation() {
        int rnd = Random.Range(0,4);
        audioManager.Play(matchSound[rnd]);
        whiteBackground.SetActive(true);
        matchAnimation.SetActive(true);
        yield return new WaitForSeconds(1.5f);
        whiteBackground.SetActive(false);
        matchAnimation.SetActive(false);
    }

}
