using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ball : MonoBehaviour
{
    public GameObject ballFragment;
    public Sprite[] ballFragmentSprites;
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
            spawner.audioManager.Play("BallHitPlayer");
            spawner.ballImpacts += 1;
            spawner.impacts_txt.text = "Impactos: " + spawner.ballImpacts.ToString();
            Destroy(gameObject);
        }
        else if (collision.gameObject.tag == "fragment")
            Destroy(collision.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "shield")
        {
            spawner.audioManager.Play("BallReflected");
            Vector2 direction = (player.transform.position - transform.position).normalized;
            rb.AddForce(-(direction * repelForce), ForceMode2D.Impulse);
            repealed = true;
            Destroy(gameObject, 3f);
        }
        else
        {
            Vector2 direction = (player.transform.position - transform.position).normalized;
            GameObject fragment1 = Instantiate(ballFragment);
            fragment1.GetComponent<SpriteRenderer>().sprite = ballFragmentSprites[0];
            fragment1.transform.position = transform.position;
            Rigidbody2D fragment1rb = fragment1.GetComponent<Rigidbody2D>();
            float xMultiplier1 = Random.RandomRange(1f, 1.6f);
            float yMultiplier1= Random.RandomRange(0.4f, 1f);
            fragment1rb.AddForce(new Vector2(-direction.x * xMultiplier1, -direction.y * yMultiplier1),ForceMode2D.Impulse);
            fragment1rb.AddTorque(1f, ForceMode2D.Impulse);
            GameObject fragment2 = Instantiate(ballFragment);
            fragment2.GetComponent<SpriteRenderer>().sprite = ballFragmentSprites[1];
            fragment2.transform.position = transform.position;
            Rigidbody2D fragment2rb = fragment2.GetComponent<Rigidbody2D>();
            float xMultiplier2 = Random.RandomRange(0.2f, 0.8f);
            float yMultiplier2 = Random.RandomRange(1.2f, 1.8f);
            fragment2rb.AddForce(new Vector2(-direction.x * xMultiplier2, -direction.y * yMultiplier2), ForceMode2D.Impulse);
            fragment2rb.AddTorque(3f,ForceMode2D.Impulse);
            Destroy(fragment1, 3f);
            Destroy(fragment2, 3f);
            Destroy(gameObject);
        }

        }
}
