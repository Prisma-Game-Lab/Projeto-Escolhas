using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class FinishedMinigameUI : MonoBehaviour
{
    public GameObject finishedMinigameUI;
    public Image progressBar;
    public TextMeshProUGUI percentage_txt;
    public TextMeshProUGUI comments_txt;
    private List<string> comments = new List<string>();

    private RaisePlayerStats stats;

    private void Awake()
    {
        this.enabled = false;
    }

    void Start()
    {
        stats = GetComponent<RaisePlayerStats>();
        comments.Add("Excelente!");
        comments.Add("Ótimo");
        comments.Add("Bom!");
        comments.Add("Mais ou menos!");
        comments.Add("Ruim!");
        comments.Add("Péssimo!");

        finishedMinigameUI.SetActive(true);
        StartCoroutine(LerpUI(stats.performacePercentage));
        if (stats.performacePercentage >= 1f)
            comments_txt.text = comments[0];
        else if (stats.performacePercentage >= 0.8f)
            comments_txt.text = comments[1];
        else if (stats.performacePercentage >= 0.7f)
            comments_txt.text = comments[2];
        else if (stats.performacePercentage >= 0.6f)
            comments_txt.text = comments[3];
        else if (stats.performacePercentage >= 0.4f)
            comments_txt.text = comments[4];
        else
            comments_txt.text = comments[5];
        Pause.isPaused = true;
    }

   /* private void LateUpdate()
    {
        if ((Timer.timeStopped || Spawner.allWavesFinished) && !finishedUIShown)
        {
            print(stats.performacePercentage);
            finishedMinigameUI.SetActive(true);
            StartCoroutine(LerpUI(stats.performacePercentage));
            if (stats.performacePercentage >= 1f)
                comments_txt.text = comments[0];
            else if (stats.performacePercentage >= 0.8f)
                comments_txt.text = comments[1];
            else if (stats.performacePercentage >= 0.7f)
                comments_txt.text = comments[2];
            else if (stats.performacePercentage >= 0.6f)
                comments_txt.text = comments[3];
            else if (stats.performacePercentage >= 0.4f)
                comments_txt.text = comments[4];
            else
                comments_txt.text = comments[5];
            finishedUIShown = true;
        }
    }*/

    private IEnumerator LerpUI(float performanceValue)
    {
        float elapsedTime = 0;
        float waitTime = 1f;
        while (elapsedTime < waitTime)
        {
            elapsedTime += Time.deltaTime;
            float value = Mathf.Lerp(0f, performanceValue, Mathf.SmoothStep(0.0f, 1.0f, (elapsedTime / waitTime)));
            percentage_txt.text = "" + (int)(value * 100) + "%";
            progressBar.fillAmount = value;
            yield return null;
        }
        yield return null;
    }
}
