using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TinderManager : MonoBehaviour
{
    public Image tinderImage;
    public CharacterManager characters;

    private List<CharacterBase> curCharacters=new List<CharacterBase>(); 
    private int curIndex=0;

    private void Start()
    {
        if (curCharacters.Count == 0)
        {
            for (int i = 0; i <= characters.maxIndex; i++)
            {
                curCharacters.Add(characters.allCharacters[i]);
            }
        }
    }
    public void OnNoButtonPressed()
    {
        curIndex++;
        if (curIndex > characters.maxIndex)
            curIndex = 0;
        tinderImage.sprite = characters.allCharacters[curIndex].zoomImage;
    }
    public void OnYesButtonPressed()
    {
        //add personagem na conversa e remover da lista do tinder
    }
}
