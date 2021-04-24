using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Create a new character")]
public class CharacterBase : ScriptableObject
{
    public new string name;
    public Sprite sprite;
    public Sprite zoomImage;
    public Sprite entireImage;
    public int hp;
    public int maxHp;
    public int strength;
    public int defense;
    public int energy;
    public int maxEnergy;


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
