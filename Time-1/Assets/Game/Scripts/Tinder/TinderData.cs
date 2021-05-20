using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TinderData : MonoBehaviour
{
    public List<CharacterBase> tinderCharacters;
    public List<CharacterBase> allCharacters;

    [HideInInspector] public List<CharacterBase> curContacts = new List<CharacterBase>();
    //[HideInInspector] public List<int> curContactsIndex = new List<int>();

    public int curDay;
    [HideInInspector] public int matchesNumber;

    void Awake()
    {
        GameObject[] objs = GameObject.FindGameObjectsWithTag("persistentData");
        if (objs.Length > 1)
        {
            Destroy(this.gameObject);
        }
        DontDestroyOnLoad(this.gameObject);
    }

    // Start is called before the first frame update
    void Start()
    {
        matchesNumber = 0;
        curDay = 1;
    }
}
