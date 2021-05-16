using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public GameObject ball;
    public TextMeshProUGUI waves_txt;
    public TextMeshProUGUI impacts_txt;

    [HideInInspector] public int waveNumber, impacts;
    int quantity;
    float fireRate;
    bool waveFinished;
    bool timeFinished;


    void Start()
    {
        fireRate = 1.5f;
        quantity = 6;
        waveNumber = 1;
        timeFinished = false;
        waveFinished = true;
        waves_txt.text = "Dificuldade: " + waveNumber.ToString();
    }

    void Update()
    {
        if (waveFinished && !Timer.timeStopped)
        {
            StartCoroutine(spawnWave());
            waveFinished = false;
        }else if (Timer.timeStopped && !timeFinished)
        {
            StopAllCoroutines();
            timeFinished = true;
        }
    }

    IEnumerator spawnWave()
    {
        for (int i = 0; i < quantity; i++)
        {
            Vector3 position = Random.insideUnitCircle.normalized*5f;
            Instantiate(ball, position, Quaternion.identity);
            yield return new WaitForSeconds(fireRate);
        }
        waveNumber ++;
        waves_txt.text = "Dificuldade: " + waveNumber.ToString();
        quantity+=4;
        fireRate *= 0.7f;
        yield return new WaitForSeconds(2);
        waveFinished = true;
    }
}
