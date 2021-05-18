using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public GameObject ball;
    public TextMeshProUGUI waves_txt;
    public TextMeshProUGUI impacts_txt;
    public static bool allWavesFinished;

    [HideInInspector] public int waveNumber, impacts;
    int quantity;
    float fireRate;
    bool waveFinished;


    void Start()
    {
        fireRate = 1f;
        quantity = 13;
        waveNumber = 1;
        waveFinished = true;
        allWavesFinished = false;
        waves_txt.text = "Nivel: " + waveNumber.ToString();
    }

    void Update()
    {
        if (waveNumber >= 6 && !allWavesFinished)
            allWavesFinished = true;
        if (waveFinished && !allWavesFinished)
        {
            StartCoroutine(spawnWave());
            waveFinished = false;
        }
    }

    IEnumerator spawnWave()
    {
        for (int i = 0; i < quantity; i++)
        {
            Vector3 position = Random.insideUnitCircle.normalized * 5f;
            Instantiate(ball, position, Quaternion.identity);
            yield return new WaitForSeconds(fireRate);
        }
        yield return new WaitForSeconds(2f);
        float escalation = 0.73f;
        waveNumber++;
        if(waveNumber<=5)
            waves_txt.text = "Nivel: " + waveNumber.ToString();
        quantity=(int)((float)quantity*(2-escalation));
        fireRate *= escalation;
        waveFinished = true;
    }
}
