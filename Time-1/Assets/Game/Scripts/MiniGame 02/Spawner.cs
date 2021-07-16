using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class Spawner : MonoBehaviour
{
    public GameObject ball;
    public Sprite[] ballsSprites;
    public TextMeshProUGUI waves_txt;
    public TextMeshProUGUI impacts_txt;
    public static bool allWavesFinished;

    [HideInInspector] public int waveNumber, ballImpacts, totalBallsSpawned;
    [HideInInspector] public float spawnRate ;
    public int maxWaveNumber, initialBallQuantity;
    int  ballsToSpawn;
    public float escalation;
    bool waveFinished; 


    void Start()
    {
        //maxWaveNumber = 5;
        //escalation = 0.73f;
        spawnRate = 1f;
        //initialBallQuantity = 13;
        ballsToSpawn = initialBallQuantity;
        totalBallsSpawned = initialBallQuantity;
        waveNumber = 0;
        waveFinished = true;
        allWavesFinished = false;
        waves_txt.text = "Nivel: " + waveNumber.ToString();
    }

    void Update()
    {
        if (waveFinished)
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
            GameObject curBall = Instantiate(ball, position, Quaternion.identity);
            curBall.GetComponent<SpriteRenderer>().sprite = ballsSprites[Random.Range(0, ballsSprites.Length)];
            curBall.GetComponent<Rigidbody2D>().AddTorque(4f);
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
        {
            allWavesFinished = true;
            waveFinished = false;
        }
        if(!allWavesFinished)
            waveFinished = true;
    }
}
