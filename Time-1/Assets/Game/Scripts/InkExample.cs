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
    private GameObject lastInst;
    private GameObject currentInst;
    public Button combatButton;
    private float posxc;
    private float posyc;
    private int lastLine = 0;
    private float distance;
    private List<string> messages = new List<string>();

    void Start()
    {
        story = new Story(inkJSONAsset.text);

        lastLine = 0;
        distance = 0;

        float posxb = buttonPlace.transform.position.x;
        float posyb = buttonPlace.transform.position.y;
        float posyt = topBar.transform.position.y;

        //scrollView.transform.position = new Vector2(posxb, posyb * 3.7f);

        //posxc = 800.0f;
        posyc = 700.0f;

        messages.Clear();

        refresh();
    }

    void refresh()
    {
        clearUI();
        getNextStoryBlock();

        for (int i = lastLine; i < messages.Count; i++) {
            //if (i != 0) {
                //lastInst = currentInst;
                //lastInst.transform.position = new Vector2(posxc, posyc + distance); 
            //}
            if (messages[i].Contains("Combat")) {
                showCombatButton(combatButton);
                break;
            }
            currentInst = Instantiate(textPrefab, content.transform); 
            currentInst.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = messages[i];
            if (messages[i].Contains("Other")) {
                currentInst.GetComponent<Image>().color = new Color32(169,169,169,255);
                posxc = 300.0f;
            } 
            else {
                posxc = 800.0f;
            }
            currentInst.transform.position = new Vector2(posxc, posyc + distance);  
            posxc = currentInst.transform.position.x;
            posyc = currentInst.transform.position.y;
            distance = 150.0f;
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