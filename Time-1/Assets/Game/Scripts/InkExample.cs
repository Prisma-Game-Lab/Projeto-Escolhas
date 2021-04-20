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

    public GameObject buttonPlace;

    private List<string> messages = new List<string>();

    // Start is called before the first frame update
    void Start()
    {
        // Load the next story block
        story = new Story(inkJSONAsset.text);

        // Start the refresh cycle
        refresh();

    }

    // Refresh the UI elements
    //  – Clear any current elements
    //  – Show any text chunks
    //  – Iterate through any choices and create listeners on them
    void refresh()
    {
        // Clear the UI
        clearUI();

        // Create a new GameObject
        //GameObject newGameObject = new GameObject("TextChunk");
        // Set its transform to the Canvas (this)
        //newGameObject.transform.SetParent(this.transform, false);

        // Add a new Text component to the new GameObject
        //Text newTextObject = newGameObject.AddComponent<Text>();
        // Set the fontSize larger
        //newTextObject.fontSize = 55;

        // Load the next block and save text (if any)
        getNextStoryBlock();

        List<GameObject> newGameObject1 = new List<GameObject>();

        int i = 0;

        foreach (var line in messages) {
            Debug.Log(line);
            Instantiate(textPrefab, content.transform);
            textPrefab.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = line;
            textPrefab.transform.SetParent(content.transform);
            //GameObject newGameObject2 = new GameObject("line");
            //newGameObject1.Add(newGameObject2);
            //newGameObject1[i].AddComponent<Text>();
            //newGameObject1[i].transform.SetParent(content.transform);
            //newGameObject1[i].GetComponent<Text>().fontSize = 55;
            //newGameObject1[i].GetComponent<Text>().text = line;
            //newGameObject1[i].GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            //i++;
            //newGameObject.AddComponent<Image>();
            //if (line.Contains("Player")) {
                //newGameObject1[i].GetComponent<Text>().text.Alignment = "right";
            //}
        }

        foreach (var j in newGameObject1) {
            Debug.Log(j.GetComponent<Text>().text);
        }

        // Get the current tags (if any)
        List<string> tags = story.currentTags;

        // If there are tags, use the first one.
        // Otherwise, just show the text.
        //if (tags.Count > 0)
        //{
            //newTextObject.text = "<color=grey>" + tags[0] + "</color> – " + text;
        //}
        //else
        //{
            //newTextObject.text = text;
        //}

        // Load Arial from the built-in resources
        //newTextObject.font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;

        foreach (Choice choice in story.currentChoices)
        {
            Button choiceButton = Instantiate(buttonPrefab, buttonPlace.transform);
            choiceButton.transform.SetParent(buttonPlace.transform);

            // Gets the text from the button prefab
            Text choiceText = choiceButton.GetComponentInChildren<Text>();
            choiceText.text = choice.text;

            // Set listener
            choiceButton.onClick.AddListener(delegate {
                OnClickChoiceButton(choice);
            });

        }
    }

    // When we click the choice button, tell the story to choose that choice!
    void OnClickChoiceButton(Choice choice)
    {
        story.ChooseChoiceIndex(choice.index);
        refresh();
    }

    // Clear out all of the UI, calling Destory() in reverse
    void clearUI()
    {
        int childCount = buttonPlace.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            //if (this.transform.GetChild(i).gameObject.tag != "Scroll") {
            //if (this.transform.GetChild(i).gameObject.tag == "ButtonSpace") {
                Destroy(buttonPlace.transform.GetChild(i).gameObject); 
                //}
                //else {
                    //GameObject.Destroy(this.transform.GetChild(i).gameObject);
                //}
           //}
        }
    }


    // Load and potentially return the next story block
    void getNextStoryBlock()
    {

        while (story.canContinue)
        {
            //text = story.ContinueMaximally();
            messages.Add(story.Continue());
        }

        //return text;
    }

    // Update is called once per frame
    void Update()
    {

    }
}