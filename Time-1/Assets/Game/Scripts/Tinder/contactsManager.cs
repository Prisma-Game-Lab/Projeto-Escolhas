using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class contactsManager : MonoBehaviour
{
    public GameObject contactPrefab;
    public GameObject messagePanel;
    public Image chatButtonPopUpImage;
    public List<GameObject> characterPanelList = new List<GameObject>();

    private TinderData tinderData;
    private AudioManager audioManager;
    public GameObject settingsAndDay;
    private AppSave appSave;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        tinderData = GameObject.FindGameObjectWithTag("persistentData").GetComponent<TinderData>();
        if (tinderData.curContacts.Count != 0)
        {
            foreach (CharacterBase contact in tinderData.curContacts)
            {
                createContact(contact);
            }
        }
    }


    public void createContact(CharacterBase character)
    {
        appSave = SaveSystem.GetInstance().appSave;
        GameObject contact = Instantiate(contactPrefab, messagePanel.transform);
        if (character.name == "Amarillys") 
            contact.gameObject.tag = "Elf";
        else if (character.name == "Bruce")
            contact.gameObject.tag = "Orc";
        else if (character.name == "Clarissa")
            contact.gameObject.tag = "Sereia";
        else
            contact.gameObject.tag = "Humano";
        contact.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = character.MensagemImage;
        contact.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = character.name;
        Image popUp = contact.transform.GetChild(0).GetChild(2).gameObject.GetComponent<Image>();
        contact.SetActive(true);
        //setando o botao
        GameObject characterPanel = getCharacterPanel(character);
        Button button = contact.transform.GetChild(0).GetComponent<Button>();
        button.onClick.AddListener(() => openMessage(characterPanel, popUp, character));
        foreach (CharacterBase blocked in appSave.blockedCharacters) {
            if (character.name == blocked.name) {
                contact.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = "BLOQUEADO";
                button.interactable = false;
            }
        }
        if (!tinderData.curContacts.Contains(character))
        {
            character.popUp = true;
            tinderData.curContacts.Add(character);
            SaveSystem.GetInstance().appSave.curContacts.Add(character);
            SaveSystem.GetInstance().SaveState();
        }
        else if (character.popUp)
        {
            chatButtonPopUpImage.gameObject.SetActive(true);
            popUp.enabled = true;
        }
        else
        {
            popUp.enabled = false;
        }
    }
    
    private GameObject getCharacterPanel(CharacterBase character)
    {
        if (character.race == CharacterBase.CharacterRace.Elfa)
            return characterPanelList[0];
        else if (character.race == CharacterBase.CharacterRace.Humano)
            return characterPanelList[1];
        else if (character.race == CharacterBase.CharacterRace.Sereia)
            return characterPanelList[2];
        else if (character.race == CharacterBase.CharacterRace.Orc)
            return characterPanelList[3];
        else if (character.race == CharacterBase.CharacterRace.Carneira)
            return characterPanelList[4];
        else
            return characterPanelList[0];
    }

    private void openMessage(GameObject panel, Image popUp, CharacterBase character)
    {
        audioManager.Play("Click");
        settingsAndDay.SetActive(false);
        if (popUp.IsActive())
        {
            character.popUp = false;
            popUp.enabled = false;
            int counter = 0;
            foreach (CharacterBase contact in tinderData.curContacts)
            {
                if (contact.popUp)
                {
                    counter += 1;
                }
            }
            if (counter > 0)
                chatButtonPopUpImage.gameObject.SetActive(true);
            else
                chatButtonPopUpImage.gameObject.SetActive(false);
        }
        panel.SetActive(true);
        tinderData.combatCharacter = character;
    }
    
    public void blockContact(string tag) {
        int childCount = messagePanel.transform.childCount;
        appSave = SaveSystem.GetInstance().appSave;
        for (int i = 1; i < childCount; i++) {
            if (messagePanel.transform.GetChild(i).gameObject.tag == tag) {
                messagePanel.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.GetComponent<Button>().interactable = false;
                messagePanel.transform.GetChild(i).gameObject.transform.GetChild(0).gameObject.transform.GetChild(1).GetComponent<TextMeshProUGUI>().text = "BLOQUEADO";
                CharacterBase curContact = tinderData.curContacts[i-1];
                appSave.blockedCharacters.Add(curContact);
                SaveSystem.GetInstance().SaveState();
                break;
            }
        }
    }
    
}
