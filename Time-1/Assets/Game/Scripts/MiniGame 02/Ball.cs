using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameObject player;
    private Spawner spawner;
    private Rigidbody2D rb;
    private bool repealed;

    public float repelForce;
    public float gravityForce;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        spawner = player.GetComponent<Spawner>();
        rb = GetComponent<Rigidbody2D>();
        repealed = false;
    }
    private void FixedUpdate()
    {
        if (!repealed)
            followPlayer();
        if (Spawner.allWavesFinished)
            Destroy(gameObject);
    }

    private void followPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.AddForce(direction * gravityForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            spawner.ballImpacts += 1;
            spawner.impacts_txt.text = "Impactos: " + spawner.ballImpacts.ToString();
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "shield")
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.AddForce(-(direction * repelForce), ForceMode2D.Impulse);
            repealed = true;
            Destroy(gameObject, 3f);
        }
        else
            Destroy(gameObject);
            
    }
}
