using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "CharacterManager", menuName = "Create Character Manager")]
public class CharacterManager : ScriptableObject
{
    public List<CharacterBase> allCharacters;
    public List<CharacterBase> tinderCharacters;
    public List<CharacterBase> blockedCharacters;
}
