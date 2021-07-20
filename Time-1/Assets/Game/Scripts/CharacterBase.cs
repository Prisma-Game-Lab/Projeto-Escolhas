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
    [SerializeField]
    public Sprite MensagemImage;
    public Sprite ChatImage;
    public Sprite BioImage;
    public Sprite tinderImage;
    public Sprite combatImage;
    //public Sprite combatWhiteImage;
    public Sprite scenario;

    //public int maxHealth;
    public float attack;
    public float defense;
    public float velocity;
    public int maxEnergy;
    [HideInInspector]
    public bool popUp;

    public enum CharacterRace {Elfa, Humano, Sereia, Orc, Carneira, Fake};
    public CharacterRace race;

    public string Name {
        get { return name; }
    }

    public string Bio {
        get { return bio; }
    }

    public Sprite Sprite {
        get { return ChatImage; }
    }

    public float Strength {
        get { return attack; }
    }

    public float Defense {
        get { return defense; }
    }
    
}
