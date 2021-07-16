using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using SimpleJSON;

public class LoveConfession : MonoBehaviour
{
    public List<TextAsset> confessions = new List<TextAsset>();
    public TextMeshProUGUI sentenceText;
    private string currentText;
    private string fullText;
    public List<Image> loveImage = new List<Image>();
    private AppSave appSave;
    private AudioManager audioManager;
    private string path;

    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        appSave = SaveSystem.GetInstance().appSave;
        int c;
        c = appSave.love;
        if (c == 0)
            path = "elfa_";
        else if(c == 1)
            path = "orc_";
        else if(c == 2)
            path = "sereia_";
        else
            path = "humano_";
        loveImage[c].enabled = true;
        StartCoroutine(TypingEffect(c));
    }

    IEnumerator TypingEffect(int c) {
        TextAsset confession = confessions[0];
        string jsonString = confession.ToString();
        JSONNode text = JSON.Parse(jsonString);
        int pos = 1;
        foreach(JSONNode sentence in text) {
            fullText = sentence;
            string oi = path + pos;
            Debug.Log(oi.Length);
            audioManager.Play(path + pos);
            for (int i=0; i <= fullText.Length; i++) {
                currentText = fullText.Substring(0,i);
                sentenceText.text = currentText;
                yield return new WaitForSeconds(0.1f);
            }
            pos ++;
            yield return new WaitForSeconds(1.0f);
        }
    }

}