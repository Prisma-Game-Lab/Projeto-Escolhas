using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "TinderManager", menuName = "Create Tinder Manager")]
public class TinderManager : ScriptableObject
{
    public List<CharacterBase> allCharacters;
    public int curIndex;
}
