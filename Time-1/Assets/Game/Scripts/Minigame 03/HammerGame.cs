using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerGame : MonoBehaviour
{
    public GameObject indicator;
    public GameObject bar;
    public float speed;
    
    void Start()
    {
    }

   
    void Update()
    {
        float posy = indicator.transform.position.y;
        if ((posy <= -2.16f) || (posy >= 4.63f)) {
            speed *= -1;
        }
        indicator.transform.position = new Vector2(indicator.transform.position.x, posy - speed);

    }
}
