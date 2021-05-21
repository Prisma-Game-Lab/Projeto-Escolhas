using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Create a new character")]
public class CharacterBase : ScriptableObject
{
    public new string name;
    public Sprite profileChatImage;
    public Sprite tinderImage;
    public Sprite combatImage;
    public Sprite combatWhiteImage;
    public int maxHealth;
    public int attack;
    public int defense;
    public int velocity;
    public int maxEnergy;
    [HideInInspector]
    public bool popUp;

    public enum CharacterRace {Elfa, Humano, Sereia, Orc, Carneira};
    public CharacterRace race;

    public string Name {
        get { return name; }
    }

    public Sprite Sprite {
        get { return profileChatImage; }
    }

    public int Strength {
        get { return attack; }
    }

    public int Defense {
        get { return defense; }
    }
    
}
