using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    private GameObject player;
    private Rigidbody2D rb;
    private bool repealed;

    public float repelForce;
    public float gravityForce;

    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        repealed = false;
    }
    private void FixedUpdate()
    {
        if(!repealed)
            followPlayer();
    }

    private void followPlayer()
    {
        Vector2 direction = (player.transform.position - transform.position).normalized;
        rb.AddForce(direction * gravityForce);
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Player")
            //perde vida
            return;
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "shield")
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.AddForce(-(direction * repelForce), ForceMode2D.Impulse);
            repealed = true;
        }
        else
            Destroy(gameObject);
            
    }
}
