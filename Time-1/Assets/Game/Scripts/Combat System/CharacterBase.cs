using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Create a new character")]
public class CharacterBase : ScriptableObject
{
    [SerializeField] new string name;
    [SerializeField] Sprite sprite;
    [SerializeField] int hp;
    [SerializeField] int strength;
    [SerializeField] int defense;

    public string Name {
        get { return name; }
    }

    public Sprite Sprite {
        get { return sprite; }
    }

    public int HP {
        get { return hp; }
    }

    public int Strength {
        get { return strength; }
    }

    public int Defense {
        get { return defense; }
    }

}
