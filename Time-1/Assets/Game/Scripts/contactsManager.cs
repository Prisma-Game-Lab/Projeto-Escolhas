using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;

public class contactsManager : MonoBehaviour
{
    public GameObject contactPrefab;
    public GameObject messagePanel;
    public List<GameObject> characterPanelList = new List<GameObject>();

    private GameObject panel;


    void Start()
    {
        
    }


    public void createMessage(CharacterBase character, int index)
    {
        GameObject contact = Instantiate(contactPrefab, messagePanel.transform);
        contact.transform.GetChild(0).GetChild(0).gameObject.GetComponent<Image>().sprite = character.profileChatImage;
        contact.transform.GetChild(0).GetChild(1).gameObject.GetComponent<TextMeshProUGUI>().text = character.name;
        contact.SetActive(true);
        panel = characterPanelList[index];
        Button button = contact.transform.GetChild(0).GetComponent<Button>();
        button.onClick.AddListener(openMessage);
    }
    private void openMessage()
    {
        panel.SetActive(true);
    }
}
