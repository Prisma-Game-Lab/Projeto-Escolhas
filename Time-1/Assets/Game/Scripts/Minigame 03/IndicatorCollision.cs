using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class IndicatorCollision : MonoBehaviour
{
    HammerGame hGame;
    // Start is called before the first frame update
    void Start()
    {
        hGame = GetComponentInParent<HammerGame>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.transform.CompareTag("GreenSquare"))
        {
            hGame.totalPossiblePoints += 1;
            hGame.outOfSquareBounds = false;
        }
    }
    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.transform.CompareTag("GreenSquare"))
        {
            hGame.outOfSquareBounds = true;
            hGame.canMakePoint = true;
        }
    }
}
