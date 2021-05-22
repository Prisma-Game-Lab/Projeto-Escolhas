using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;

public class InkExample : MonoBehaviour
{
    public List<TextAsset> inkJSONAsset = new List<TextAsset>();
    private Story story;
    public Button buttonPrefab;
    public GameObject textPrefab;
    public GameObject content;
    public GameObject scrollView;
    public GameObject buttonPlace;
    private List<GameObject> lastInst = new List<GameObject>();
    private GameObject currentInst;
    public Button combatButton;
    private float posxc;
    private float posyc;
    private int lastLine;
    private float distance;
    private List<string> messages = new List<string>();
    TinderData tinderData;
    public List<Sprite> playerSprite = new List<Sprite>();
    public List<Sprite> otherSprite = new List<Sprite>();
    private AudioManager audioManager;
    public TextMeshProUGUI typing;

    void Start()
    {
        audioManager = FindObjectOfType<AudioManager>();
        tinderData = GameObject.FindGameObjectWithTag("persistentData").GetComponent<TinderData>();
        story = new Story(inkJSONAsset[tinderData.curDay-1].text);

        lastLine = 0;
        distance = 150.0f;

        float posxb = buttonPlace.transform.position.x;
        float posyb = buttonPlace.transform.position.y;

        //scrollView.transform.position = new Vector2(posxb, posyb * 3.7f);

        posyc = 700.0f;

        messages.Clear();

        StartCoroutine(refresh());
    }

    private IEnumerator refresh()
    {
        clearUI();
        getNextStoryBlock();

        for (int i = lastLine; i < messages.Count; i++) {
            if (messages[i].Contains("Combat")) {
                showCombatButton(combatButton);
                break;
            }
            if (messages[i].Contains("Other")) {
                if (messages[i].Contains("sticker")) {
                    yield return new WaitForSeconds(1.0f);
                    currentInst = Instantiate(textPrefab, content.transform); 
                    int len = messages[i].Substring(6).Length;
                    string path = "Stickers/" + messages[i].Substring(6, len-1);
                    Sprite sprite = Resources.Load<Sprite>(path);
                    currentInst.GetComponent<Image>().sprite = sprite;
                    currentInst.GetComponent<Image>().color = new Color32(255,255,255,255);
                    currentInst.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = null;
                    currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(300.0f, 250.0f);
                }
                else {
                    float sec = Random.Range(1.2f, 3.5f);
                    yield return new WaitForSeconds(0.5f);
                    typing.gameObject.SetActive(true);
                    yield return new WaitForSeconds(sec);
                    typing.gameObject.SetActive(false);
                    currentInst = Instantiate(textPrefab, content.transform); 
                    currentInst.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = messages[i].Substring(6);
                    //if provisorio, apenas testando o tamanho do balao
                    if (messages[i].Substring(6).Length > 37) {
                        currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(518.3558f, 140.0f);
                    }
                    currentInst.GetComponent<Image>().sprite = otherSprite[0];
                }
                posxc = 300.0f;
            } 
            else {
                currentInst = Instantiate(textPrefab, content.transform); 
                currentInst.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = messages[i].Substring(7);
                currentInst.GetComponent<Image>().sprite = playerSprite[0];
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

        foreach (Choice choice in story.currentChoices)
        {
            Button choiceButton = Instantiate(buttonPrefab, buttonPlace.transform);
            choiceButton.transform.SetParent(buttonPlace.transform);

            Text choiceText = choiceButton.GetComponentInChildren<Text>();
            choiceText.text = choice.text.Substring(7);

            choiceButton.onClick.AddListener(delegate {
                OnClickChoiceButton(choice);
            });

        }
    }

    void OnClickChoiceButton(Choice choice)
    {
        audioManager.Play("Click");
        story.ChooseChoiceIndex(choice.index);
        StartCoroutine(refresh());
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
            string text = story.Continue();
            if (!text.Contains("Skip")) {
                messages.Add(text);
            }
        }
    }

    void showCombatButton(Button combatButton) {
        combatButton.gameObject.SetActive(true);
    }
}