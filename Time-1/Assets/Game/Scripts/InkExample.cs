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
    public Transform messageDeltaPosY;
    public Transform playerMessagePos;
    public Transform otherMessagePos;
    public ScrollRect scroll;

    private List<GameObject> lastInst = new List<GameObject>();
    private GameObject currentInst;
    public Button combatButton;
    private bool buttonClicked;
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
        distance = (messageDeltaPosY.position.y-playerMessagePos.position.y);

        float posxb = buttonPlace.transform.position.x;
        float posyb = buttonPlace.transform.position.y;

        //scrollView.transform.position = new Vector2(posxb, posyb * 3.7f);

        posyc = 700.0f;

        messages.Clear();

        StartCoroutine(refresh());
    }

    private IEnumerator refresh()
    {
        scroll.verticalNormalizedPosition = 0;
        scroll.enabled = false;
        clearUI();
        getNextStoryBlock();
        for (int i = lastLine; i < messages.Count; i++) {
            if (messages[i].Contains("Combat")) {
                showCombatButton(combatButton);
                break;
            }
            bool isSticker = false;
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
                    isSticker = true;
                }
                else {
                    float sec = Random.Range(1.0f, 2.5f);
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
                posxc = otherMessagePos.position.x;
            } 
            else {
                if (!buttonClicked)
                    yield return new WaitForSeconds(1.5f);
                else
                    buttonClicked = false;
                currentInst = Instantiate(textPrefab, content.transform); 
                currentInst.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = messages[i].Substring(7);
                currentInst.GetComponent<Image>().sprite = playerSprite[0];
                posxc = playerMessagePos.position.x;
            }
            if (isSticker)
                currentInst.transform.position = new Vector2(posxc*0.75f, playerMessagePos.position.y*1.1f);  
            else
                currentInst.transform.position = new Vector2(posxc, playerMessagePos.position.y);
            if (i != 0) {
                foreach (var message in lastInst) {
                    float posxc1 = message.transform.position.x;
                    float posyc1 = message.transform.position.y;
                    if (isSticker)
                        message.transform.position = new Vector2(posxc1, posyc1 + distance*2f); 
                    else
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
        scroll.enabled = true;
    }

    void OnClickChoiceButton(Choice choice)
    {
        buttonClicked = true;
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