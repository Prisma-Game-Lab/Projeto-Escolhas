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

    void Start() {
        
        StartCoroutine(TypingEffect());
    }

    IEnumerator TypingEffect() {
        TextAsset confession = confessions[0];
        string jsonString = confession.ToString();
        JSONNode text = JSON.Parse(jsonString);
        foreach(JSONNode sentence in text) {
            fullText = sentence;
            for (int i=0; i <= fullText.Length; i++) {
                currentText = fullText.Substring(0,i);
                sentenceText.text = currentText;
                yield return new WaitForSeconds(0.1f);
            }
            yield return new WaitForSeconds(1.0f);
        }
    }

}