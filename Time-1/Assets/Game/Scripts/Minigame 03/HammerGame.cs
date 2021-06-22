using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HammerGame : MonoBehaviour
{
    public GameObject indicator;
    public GameObject bar;
    public float speed;
    private float speedSquareObj;
    public Animator ropeAnim;
    public GameObject squareObj;
    public TextMeshProUGUI pointsText;
    private int point;
    float totalTime;
    private Timer timer;

    void Start()
    {
        ropeAnim.enabled = false;
        //squareObj = bar.transform.GetChild(0).gameObject;
        point = 0;
        //Debug.Log(Timer.totalTime);
        totalTime = Timer.totalTime;
        timer = this.GetComponent<Timer>();
        speedSquareObj = 0.05f;
        //StartCoroutine(UpAndDownPosition());
    }

   
    void Update()
    {
        if (!Timer.timeStopped && !Pause.isPaused) {
            float posy = indicator.transform.position.y;;
            if ((posy <= -2.16f) || (posy >= 4.63f))
                speed *= -1;
            indicator.transform.position = new Vector2(indicator.transform.position.x, posy - speed);
            if (timer.timeRemaining > 10.0f && timer.timeRemaining < 20.0f) {
                UpAndDownPosition();
            }
            //else if (timer.timeRemaining < 10.0f) {
                //RandomPosition();
            //}
        }
    }

    public void Jump() {
        if (!Timer.timeStopped)
        {
            StartCoroutine(PlayAnimationOnce());
            CheckPositionIsEqual();
        }
    }

    private void CheckPositionIsEqual() {
        float sqHeight = squareObj.GetComponent<SpriteRenderer>().bounds.size.y;
        float posy = indicator.transform.position.y;
        float sqPosY = squareObj.transform.position.y;
        if ((posy < sqPosY + sqHeight*0.5f) && (posy > sqPosY - sqHeight*0.5f)) {
            point++;
            pointsText.text = point.ToString();
        }
    }

    private IEnumerator PlayAnimationOnce() {
        ropeAnim.enabled = true;
        yield return new WaitForSeconds(0.32f);
        ropeAnim.enabled = false;
    }

    private void UpAndDownPosition() {
        float posy = squareObj.transform.position.y;
        if ((posy <= -2.16f) || (posy >= 4.50f)) 
            speedSquareObj *= -1;
        squareObj.transform.position = new Vector2(squareObj.transform.position.x, posy + speedSquareObj);
    }
    
    private void RandomPosition() {
        float rndPos = Random.Range(-2.10f, 4.60f);
        squareObj.transform.position = new Vector2(squareObj.transform.position.x, rndPos);
    }


}
