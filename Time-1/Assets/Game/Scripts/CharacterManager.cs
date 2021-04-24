using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterManager", menuName = "Create Character Manager")]
public class CharacterManager : ScriptableObject
{
    public List<CharacterBase> allCharacters;
    [HideInInspector] public List<CharacterBase> tinderCharacters;
    public int maxIndex;
}
