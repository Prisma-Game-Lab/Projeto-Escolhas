using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject ball;
    public int waveNumber;
    int quantity;
    float fireRate;
    bool waveFinished;

    // Start is called before the first frame update
    void Start()
    {
        fireRate = 1f;
        quantity = 3;
        waveNumber = 1;
        waveFinished = true;
    }

    // Update is called once per frame
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
        for (int i = 0; i < quantity; i++)
        {
            Vector3 position = Random.insideUnitCircle.normalized*3.5f;
            Instantiate(ball, position, Quaternion.identity);
            yield return new WaitForSeconds(fireRate);
        }
        waveNumber ++;
        quantity+=3;
        fireRate *= 0.85f;
        //yield return new WaitForSeconds(3);
        waveFinished = true;
    }
}
