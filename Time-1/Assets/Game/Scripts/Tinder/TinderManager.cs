using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class TinderManager : MonoBehaviour
{
    public TextMeshProUGUI day_txt;
    public Image tinderImage;
    private int curIndex;
    TinderData tinderData;

    private void Start()
    {
        tinderData = GameObject.FindGameObjectWithTag("persistentData").GetComponent<TinderData>();
        curIndex = 0;
        tinderImage.sprite = tinderData.tinderCharacters[0].zoomImage;
        day_txt.text = "Day " + tinderData.curDay;
    }

    public void OnNoButtonPressed()
    {
        curIndex++;
        if (curIndex > tinderData.tinderCharacters.Count - 1)
            curIndex = 0;
        tinderImage.sprite = tinderData.tinderCharacters[curIndex].zoomImage;
    }
    public void OnYesButtonPressed()
    {
        if (tinderData.curDay > tinderData.matchesNumber)
        {
            tinderData.tinderCharacters.Remove(tinderData.tinderCharacters[curIndex]);
            curIndex = 0;
            tinderImage.sprite = tinderData.tinderCharacters[curIndex].zoomImage;
            tinderData.matchesNumber += 1;
            //cria e abre uma conversa com o personagem
        }
    }
    public void OnBioButtonPressed()
    {
        //abre UI da bio
    }
}
