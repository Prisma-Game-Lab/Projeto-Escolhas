using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HammerGame : MonoBehaviour
{
    public GameObject indicator;
    public GameObject bar;
    private float barx, bary, height;
    private float speed = 10.5f;
    private float newPos;

    void Start()
    {
        bary = bar.transform.position.y;
        Debug.Log(bar.GetComponent<RectTransform>().rect.height);
    }

   
    void Update()
    {
        float posy = indicator.transform.position.y;
        if ((posy <= bary - bar.GetComponent<RectTransform>().rect.height*0.27f ) || (posy >= bary + bar.GetComponent<RectTransform>().rect.height*0.27f )) {
            Debug.Log(posy);
            speed *= -1;
        }
        indicator.transform.position = new Vector2(indicator.transform.position.x, posy - speed);

    }
}
