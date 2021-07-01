using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Character", menuName = "Character/Create a new character")]
public class CharacterBase : ScriptableObject
{
    public new string name;
    [TextArea]
    public string bio;
    public string bioWork;
    public Sprite MensagemImage;
    public Sprite ChatImage;
    public Sprite BioImage;
    public Sprite tinderImage;
    public Sprite combatImage;
    public Sprite combatWhiteImage;
    public Sprite scenerio;
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

    public string Bio {
        get { return bio; }
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
