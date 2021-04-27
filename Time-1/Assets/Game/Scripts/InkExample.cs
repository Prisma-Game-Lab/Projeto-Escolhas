using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;

public class InkExample : MonoBehaviour
{
    public TextAsset inkJSONAsset;
    private Story story;
    public Button buttonPrefab;
    public GameObject textPrefab;
    public GameObject content;
    public GameObject scrollView;
    public GameObject buttonPlace;
    public GameObject topBar;
    private List<GameObject> lastInst = new List<GameObject>();
    private GameObject currentInst;
    public Button combatButton;
    private float posxc;
    private float posyc;
    private int lastLine;
    private float distance;
    private List<string> messages = new List<string>();

    void Start()
    {
        story = new Story(inkJSONAsset.text);

        lastLine = 0;
        distance = 150.0f;;

        float posxb = buttonPlace.transform.position.x;
        float posyb = buttonPlace.transform.position.y;
        float posyt = topBar.transform.position.y;

        //scrollView.transform.position = new Vector2(posxb, posyb * 3.7f);

        posyc = 700.0f;

        Debug.Log(Screen.currentResolution.width);

        messages.Clear();

        refresh();
    }

    void refresh()
    {
        clearUI();
        getNextStoryBlock();

        for (int i = lastLine; i < messages.Count; i++) {
            if (messages[i].Contains("Combat")) {
                showCombatButton(combatButton);
                break;
            }
            currentInst = Instantiate(textPrefab, content.transform); 
            if (messages[i].Contains("Other")) {
                currentInst.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = messages[i].Substring(6);
                currentInst.GetComponent<Image>().color = new Color32(169,169,169,255);
                posxc = 300.0f;
            } 
            else {
                currentInst.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = messages[i].Substring(7);
                posxc = 800.0f;
            }
            currentInst.transform.position = new Vector2(posxc, 550.0f);  
            if (i != 0) {
                foreach (var message in lastInst) {
                    float posxc1 = message.transform.position.x;
                    float posyc1 = message.transform.position.y;
                    message.transform.position = new Vector2(posxc1, posyc1 + distance); 
                }
            }
            lastInst.Add(currentInst);
            posxc = currentInst.transform.position.x;
            posyc = currentInst.transform.position.y;
            lastLine += 1;
        }
        
        // Get the current tags (if any)
        //List<string> tags = story.currentTags;

        foreach (Choice choice in story.currentChoices)
        {
            Button choiceButton = Instantiate(buttonPrefab, buttonPlace.transform);
            choiceButton.transform.SetParent(buttonPlace.transform);

            Text choiceText = choiceButton.GetComponentInChildren<Text>();
            choiceText.text = choice.text;

            choiceButton.onClick.AddListener(delegate {
                OnClickChoiceButton(choice);
            });

        }
    }

    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        refresh();
    }

    void clearUI()
    {
        int childCount = buttonPlace.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            Destroy(buttonPlace.transform.GetChild(i).gameObject); 
        }
    }

    void getNextStoryBlock()
    {
        while (story.canContinue)
        {
            messages.Add(story.Continue());
        }
    }

    void showCombatButton(Button combatButton) {
        combatButton.gameObject.SetActive(true);
    }
}