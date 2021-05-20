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
    public GameObject contactPrefab;
    public GameObject messagePanel;

    private void Start()
    {
        tinderData = GameObject.FindGameObjectWithTag("persistentData").GetComponent<TinderData>();
        curIndex = 0;
        tinderImage.sprite = tinderData.tinderCharacters[0].tinderImage;
        tinderCharacterName_txt.text = tinderData.tinderCharacters[0].name;
        day_txt.text = "Day " + tinderData.curDay;
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
        if (tinderData.curDay > tinderData.matchesNumber)
        {
            createMessage(tinderData.tinderCharacters[curIndex]);
            tinderData.tinderCharacters.Remove(tinderData.tinderCharacters[curIndex]);
            curIndex = 0;
            tinderImage.sprite = tinderData.tinderCharacters[curIndex].tinderImage;
            tinderCharacterName_txt.text = tinderData.tinderCharacters[curIndex].name;
            tinderData.matchesNumber += 1;
            //cria e abre uma conversa com o personagem
        }
    }
    public void OnBioButtonPressed()
    {
        //abre UI da bio
    }
    private void createMessage(CharacterBase character) 
    {
        GameObject cp = Instantiate(contactPrefab, messagePanel.transform); 
        cp.transform.GetChild(0).gameObject.GetComponent<Image>().sprite = character.profileChatImage;
        cp.transform.GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = character.name;
        cp.SetActive(true);
    }
}
