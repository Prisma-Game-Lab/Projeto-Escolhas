using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;

public class InkExample : MonoBehaviour
{
    public TextAsset inkJSONAsset;

    private Story story;

    public Button buttonPrefab;

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
            GameObject newGameObject2 = new GameObject("yvby");
            newGameObject1.Add(newGameObject2);
            newGameObject1[i].AddComponent<Text>();
            newGameObject1[i].transform.SetParent(this.transform, false);
            newGameObject1[i].GetComponent<Text>().fontSize = 55;
            newGameObject1[i].GetComponent<Text>().text = line;
            newGameObject1[i].GetComponent<Text>().font = Resources.GetBuiltinResource(typeof(Font), "Arial.ttf") as Font;
            i++;
            //newGameObject.AddComponent<Image>();
            //if (line.Contains("Player")) {
                //newTextObject.text = line;
            //}
            //else {
                //newTextObject.text = line;
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
            Button choiceButton = Instantiate(buttonPrefab) as Button;
            choiceButton.transform.SetParent(this.transform, false);

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
        int childCount = this.transform.childCount;
        for (int i = childCount - 1; i >= 0; i--)
        {
            GameObject.Destroy(this.transform.GetChild(i).gameObject);
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