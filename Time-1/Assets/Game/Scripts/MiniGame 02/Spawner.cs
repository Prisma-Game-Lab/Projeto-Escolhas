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

    [HideInInspector] public int waveNumber, ballImpacts, totalBallsSpawned;
    [HideInInspector] public float spawnRate ;
    int maxWaveNumber, ballsToSpawn, initialBallQuantity;
    float escalation;
    bool waveFinished; 


    void Start()
    {
        maxWaveNumber = 5;
        escalation = 0.73f;
        spawnRate = 1f;
        initialBallQuantity = 13;
        ballsToSpawn = initialBallQuantity;
        totalBallsSpawned = initialBallQuantity;
        waveNumber = 0;
        waveFinished = true;
        allWavesFinished = false;
        waves_txt.text = "Nivel: " + waveNumber.ToString();
    }

    void Update()
    {
        if (waveFinished && !allWavesFinished)
        {
            StartCoroutine(spawnWave());
            waveFinished = false;
        }
    }

    IEnumerator spawnWave()
    {
        waveNumber++;
        waves_txt.text = "Nivel: " + waveNumber.ToString();
        for (int i = 0; i < ballsToSpawn; i++)
        {
            Vector3 position = Random.insideUnitCircle.normalized * 5f;
            Instantiate(ball, position, Quaternion.identity);
            yield return new WaitForSeconds(spawnRate);
        }
        yield return new WaitForSeconds(2f);
        if (waveNumber < maxWaveNumber)
        {
            ballsToSpawn = (int)((float)ballsToSpawn * (2 - escalation));
            spawnRate *= escalation;
            totalBallsSpawned += ballsToSpawn;
        }
        else
            allWavesFinished = true;
        waveFinished = true;
    }
}
