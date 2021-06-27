using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Ink.Runtime;
using TMPro;
using UnityEngine.SceneManagement;
using SimpleJSON;

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
    private List<string> storedMessages;
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
    private AppSave appSave;
    private bool clickedBack;
    private string savedJson;
    private bool newDay;

    void OnEnable() {
        appSave = SaveSystem.GetInstance().appSave;
        tinderData = GameObject.FindGameObjectWithTag("persistentData").GetComponent<TinderData>();
        storedMessages = new List<string>();
        newDay = false;
    }

    void Start()
    {
        combatButton.gameObject.SetActive(false);
        if (this.gameObject.tag == "Elf") {
            story = new Story(inkJSONAsset[tinderData.elfaDay].text);
            storedMessages = appSave.elfa;
            if(appSave.elfaJson != "") {
                story.state.LoadJson(appSave.elfaJson);
            }
            else {
                newDay = true;
            }
        }
        else if (this.gameObject.tag == "Orc") {
            story = new Story(inkJSONAsset[tinderData.orcDay].text);
            storedMessages = appSave.orc;
            if(appSave.orcJson != "") {
                story.state.LoadJson(appSave.orcJson);
            }
            else {
                newDay = true;
            }
        }
        else {
            story = new Story(inkJSONAsset[tinderData.sereiaDay].text);
            storedMessages = appSave.sereia;
            if(appSave.sereiaJson != "") {
                story.state.LoadJson(appSave.sereiaJson);
            }
            else {
                newDay = true;
            }
        }
        clickedBack = false;
        audioManager = FindObjectOfType<AudioManager>();

        lastLine = 0;
        distance = (messageDeltaPosY.position.y-playerMessagePos.position.y);

        float posxb = buttonPlace.transform.position.x;
        float posyb = buttonPlace.transform.position.y;

        posyc = 700.0f;
        if (storedMessages.Count > 0) {
            if (storedMessages[storedMessages.Count-1].Contains("Combat") && newDay) {
                storedMessages.RemoveAt(storedMessages.Count-1);
            }
        }

        for (int i = 0; i < storedMessages.Count; i++) {
            Debug.Log(storedMessages[i]);
            if (storedMessages[i].Contains("Combat")) {
                showCombatButton(combatButton);
                break;
            }
            restoreMessages(i);
        }
        StartCoroutine(refresh());
    }

    void Update() {
        if (clickedBack) {
            clickedBack = false;
            StartCoroutine(refresh());
        }
    }

    private void restoreMessages(int i) {
            //if (storedMessages[i].Contains("Combat")) {
                //showCombatButton(combatButton);
            //}
            bool isSticker = false;
            bool isImage = false;
            bool isM = false;
            bool isG = false;
            bool isGG = false;
            bool isGGG = false;
            if (storedMessages[i].Contains("Other")) {
                if (storedMessages[i].Contains("sticker")) {
                    currentInst = Instantiate(textPrefab, content.transform); 
                    int len = storedMessages[i].Substring(6).Length;
                    string path = "Stickers/" + storedMessages[i].Substring(6, len-1);
                    Sprite sprite = Resources.Load<Sprite>(path);
                    currentInst.GetComponent<Image>().sprite = sprite;
                    currentInst.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = null;
                    currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(300.0f, 250.0f);
                    isSticker = true;
                }
                if (storedMessages[i].Contains("picture")) {
                    currentInst = Instantiate(textPrefab, content.transform); 
                    int len = storedMessages[i].Substring(6).Length;
                    string path = "Images/" + storedMessages[i].Substring(6, len-1);
                    Sprite sprite = Resources.Load<Sprite>(path);
                    currentInst.GetComponent<Image>().sprite = sprite;
                    currentInst.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = null;
                    currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(300.0f, 250.0f);
                    isImage = true;
                }
                else {
                    currentInst = Instantiate(textPrefab, content.transform); 
                    currentInst.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = storedMessages[i].Substring(6);
                    int stringLength = storedMessages[i].Substring(6).Length;
                    if (stringLength >= 24 && stringLength <= 38) {
                        currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(518.3558f, 140.0f);
                        isM = true;
                    }
                    if (stringLength > 38 && stringLength <= 45) {
                        currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(518.3558f, 200.0f);
                        isG = true;
                    }
                    if (stringLength > 45 && stringLength <= 75) {
                        currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(518.3558f, 220.0f);
                        isGG = true;
                    }
                    if (stringLength > 70 && stringLength <= 100) {
                        currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(518.3558f, 280.0f);
                        isGGG = true;
                    }
                    currentInst.GetComponent<Image>().sprite = otherSprite[0];
                }
                posxc = otherMessagePos.position.x;
            } 
            else {
                currentInst = Instantiate(textPrefab, content.transform); 
                currentInst.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = storedMessages[i].Substring(7);
                int stringLength = storedMessages[i].Substring(7).Length;
                if (stringLength >= 24 && stringLength <= 38) {
                    currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(518.3558f, 140.0f);
                    isM = true;
                }
                if (stringLength > 38 && stringLength <= 45) {
                    currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(518.3558f, 200.0f);
                    isG = true;
                }
                if (stringLength > 45 && stringLength <= 70) {
                    currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(518.3558f, 220.0f);
                    isGG = true;
                }
                if (stringLength > 70 && stringLength <= 100) {
                    currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(518.3558f, 280.0f);
                    isGGG = true;
                }
                currentInst.GetComponent<Image>().sprite = playerSprite[0];
                posxc = playerMessagePos.position.x;
            }
            if (isSticker)
                currentInst.transform.position = new Vector2(posxc*0.75f, playerMessagePos.position.y*1.1f);  
            else if (isImage) 
                currentInst.transform.position = new Vector2(posxc, playerMessagePos.position.y*1.5f);
            else if (isG) 
                currentInst.transform.position = new Vector2(posxc, playerMessagePos.position.y*1.1f);
            else if (isGG) 
                currentInst.transform.position = new Vector2(posxc, playerMessagePos.position.y*1.1f);
            else
                currentInst.transform.position = new Vector2(posxc, playerMessagePos.position.y);
            if (i != 0) {
                foreach (var message in lastInst) {
                    float posxc1 = message.transform.position.x;
                    float posyc1 = message.transform.position.y;
                    if (isSticker)
                        message.transform.position = new Vector2(posxc1, posyc1 + distance*2f);
                    else if (isImage) 
                        message.transform.position = new Vector2(posxc1, posyc1 + distance*4.6f); 
                    else if (isM) 
                        message.transform.position = new Vector2(posxc1, posyc1 + distance*1.3f);
                    else if (isG) 
                        message.transform.position = new Vector2(posxc1, posyc1 + distance*1.5f);
                    else if (isGG) 
                        message.transform.position = new Vector2(posxc1, posyc1 + distance*1.7f);
                    else if (isGGG) 
                        message.transform.position = new Vector2(posxc1, posyc1 + distance*3.0f); 
                    else
                        message.transform.position = new Vector2(posxc1, posyc1 + distance);
                }
            }
            lastInst.Add(currentInst);
            posxc = currentInst.transform.position.x;
            posyc = currentInst.transform.position.y;
    }

    private IEnumerator refresh()
    {
        scroll.verticalNormalizedPosition = 0;
        scroll.enabled = false;
        clearUI();
        getNextStoryBlock();

        for (int i = lastLine; i < messages.Count; i++) {
            if (messages[i].Contains("Combat")) {
                if (this.gameObject.tag == "Elf") {
                    appSave.elfaEndDay = true;
                }
                else if (this.gameObject.tag == "Orc") {
                    appSave.orcEndDay = true;
                }
                else {
                    appSave.sereiaEndDay = true;
                }
                showCombatButton(combatButton);
                break;
            }
            bool isSticker = false;
            bool isImage = false;
            bool isM = false;
            bool isG = false;
            bool isGG = false;
            bool isGGG = false;
            if (messages[i].Contains("Other")) {
                if (messages[i].Contains("sticker")) {
                    yield return new WaitForSeconds(1.0f);
                    currentInst = Instantiate(textPrefab, content.transform); 
                    int len = messages[i].Substring(6).Length;
                    string path = "Stickers/" + messages[i].Substring(6, len-1);
                    Sprite sprite = Resources.Load<Sprite>(path);
                    currentInst.GetComponent<Image>().sprite = sprite;
                    currentInst.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = null;
                    currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(300.0f, 250.0f);
                    isSticker = true;
                }
                else if (messages[i].Contains("picture")) {
                    yield return new WaitForSeconds(1.0f);
                    currentInst = Instantiate(textPrefab, content.transform); 
                    int len = messages[i].Substring(6).Length;
                    string path = "Images/" + messages[i].Substring(6, len-1);
                    Sprite sprite = Resources.Load<Sprite>(path);
                    currentInst.GetComponent<Image>().sprite = sprite;
                    currentInst.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = null;
                    currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(460.0f, 570.0f);
                    isImage = true;
                }
                else {
                    float sec = Random.Range(.0f, .5f);
                    yield return new WaitForSeconds(0.5f);
                    typing.gameObject.SetActive(true);
                    yield return new WaitForSeconds(sec);
                    typing.gameObject.SetActive(false);
                    currentInst = Instantiate(textPrefab, content.transform); 
                    currentInst.transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = messages[i].Substring(6);
                    //tamanho do balao
                    int stringLength = messages[i].Substring(6).Length;
                    if (stringLength >= 24 && stringLength <= 38) {
                        currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(518.3558f, 140.0f);
                        isM = true;
                    }
                    if (stringLength > 38 && stringLength <= 45) {
                        currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(518.3558f, 200.0f);
                        isG = true;
                    }
                    if (stringLength > 45 && stringLength <= 75) {
                        currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(518.3558f, 220.0f);
                        isGG = true;
                    }
                    if (stringLength > 70 && stringLength <= 100) {
                        currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(518.3558f, 280.0f);
                        isGGG = true;
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
                //tamanho do balao
                int stringLength = messages[i].Substring(7).Length;
                if (stringLength >= 24 && stringLength <= 38) {
                    currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(518.3558f, 140.0f);
                    isM = true;
                }
                if (stringLength > 38 && stringLength <= 45) {
                    currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(518.3558f, 200.0f);
                    isG = true;
                }
                if (stringLength > 45 && stringLength <= 70) {
                    currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(518.3558f, 220.0f);
                    isGG = true;
                }
                if (stringLength > 70 && stringLength <= 100) {
                    currentInst.gameObject.GetComponent<RectTransform>().sizeDelta = new Vector2(518.3558f, 280.0f);
                    isGGG = true;
                }
                currentInst.GetComponent<Image>().sprite = playerSprite[0];
                posxc = playerMessagePos.position.x;
            }
            if (isSticker)
                currentInst.transform.position = new Vector2(posxc*0.75f, playerMessagePos.position.y*1.1f); 
            else if (isImage) 
                currentInst.transform.position = new Vector2(posxc, playerMessagePos.position.y*1.5f);
            else if (isG) 
                currentInst.transform.position = new Vector2(posxc, playerMessagePos.position.y*1.1f);
            else if (isGG) 
                currentInst.transform.position = new Vector2(posxc, playerMessagePos.position.y*1.1f);
            else
                currentInst.transform.position = new Vector2(posxc, playerMessagePos.position.y);
            if (i != 0) {
                foreach (var message in lastInst) {
                    float posxc1 = message.transform.position.x;
                    float posyc1 = message.transform.position.y;
                    if (isSticker)
                        message.transform.position = new Vector2(posxc1, posyc1 + distance*2f); 
                    else if (isImage) 
                        message.transform.position = new Vector2(posxc1, posyc1 + distance*4.6f); 
                    else if (isM) 
                        message.transform.position = new Vector2(posxc1, posyc1 + distance*1.3f);
                    else if (isG) 
                        message.transform.position = new Vector2(posxc1, posyc1 + distance*1.5f);
                    else if (isGG) 
                        message.transform.position = new Vector2(posxc1, posyc1 + distance*1.7f);
                    else if (isGGG) 
                        message.transform.position = new Vector2(posxc1, posyc1 + distance*3.0f);
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

    public void GoToCombat() {
        for (int i = storedMessages.Count; i < messages.Count; i++) {
            storedMessages.Add(messages[i]);
        }
        if (appSave.elfaEndDay)
            appSave.elfaJson = "";
        if (appSave.orcEndDay) 
            appSave.orcJson = "";
        if (appSave.sereiaEndDay) 
            appSave.sereiaJson = "";
        for (int i = storedMessages.Count; i < messages.Count; i++) {
            storedMessages.Add(messages[i]);
        }
        SaveSystem.GetInstance().SaveState();
        audioManager.Play("Click");
        SceneManager.LoadScene("Combat_Scene");
    }
    
    public void OnBackButton(GameObject canvas) {
        clickedBack = true;
        StopAllCoroutines();
        if (this.gameObject.tag == "Elf") {
            appSave.elfaJson = story.state.ToJson();
        }
        else if (this.gameObject.tag == "Orc") {
            appSave.orcJson = story.state.ToJson();
        }
        else {
            appSave.sereiaJson = story.state.ToJson();
        }
        for (int i = storedMessages.Count; i < messages.Count; i++) {
            storedMessages.Add(messages[i]);
        }
        SaveSystem.GetInstance().SaveState();
        audioManager.Play("Click");
        canvas.gameObject.SetActive(false);
    }

    void OnApplicationQuit() {
        if (this.gameObject.tag == "Elf") {
            appSave.elfaJson = story.state.ToJson();
        }
        else if (this.gameObject.tag == "Orc") {
            appSave.orcJson = story.state.ToJson();
        }
        else {
            appSave.sereiaJson = story.state.ToJson();
        }
        for (int i = storedMessages.Count; i < messages.Count; i++) {
            storedMessages.Add(messages[i]);
        }
        SaveSystem.GetInstance().SaveState();
    }

}