using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TinderManager : MonoBehaviour
{
    public Image tinderImage;
    public CharacterManager characters;

    private int curIndex;

    private void Start()
    {
        curIndex = 0;
        tinderImage.sprite = characters.tinderCharacters[curIndex].zoomImage;
    }

    public void OnNoButtonPressed()
    {
        curIndex++;
        if (curIndex > characters.tinderCharacters.Count - 1)
            curIndex = 0;
        tinderImage.sprite = characters.tinderCharacters[curIndex].zoomImage;
    }
    public void OnYesButtonPressed()
    {
        characters.tinderCharacters.Remove(characters.tinderCharacters[curIndex]);
        curIndex = 0;
        tinderImage.sprite = characters.tinderCharacters[curIndex].zoomImage;
        //cria e abre uma conversa com o personagem
    }
    public void OnBioButtonPressed()
    {
        //abre UI da bio
    }
}
