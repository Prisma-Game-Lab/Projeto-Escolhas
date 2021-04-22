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

        scrollView.transform.position = new Vector2(posxb, posyb * 3.7f);

        posxc = 800.0f;
        posyc = 700.0f;

        messages.Clear();

        refresh();
    }

    void refresh()
    {
        clearUI();
        getNextStoryBlock();

        for (int i = lastLine; i < messages.Count; i++) {
            Debug.Log(messages[i]);
            GameObject inst = Instantiate(textPrefab, content.transform);
            inst.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = messages[i];
            Debug.Log(posyc);
            inst.transform.position = new Vector2(posxc, posyc + distance);      
            posxc = inst.transform.position.x;
            posyc = inst.transform.position.y;
            posyc += posyc/2;
            distance = 0.01f;
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
}