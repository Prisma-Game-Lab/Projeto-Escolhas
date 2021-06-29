using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HammerGame : MonoBehaviour
{
    public GameObject indicator;
    public GameObject bar;
    public float speed;
    public float speedSquareObj;
    public Animator ropeAnim;
    public GameObject squareObj;
    public TextMeshProUGUI pointsText;
    private int point;
    float totalTime;
    private Timer timer;
    private float minPos;
    private float maxPos;

    void Start()
    {
        //ropeAnim.enabled = false;
        point = 0;
        totalTime = Timer.totalTime;
        timer = this.GetComponent<Timer>();
        minPos = -1.67f;
        maxPos =  4.70f;
        
    }

   
    void Update()
    {
        if (!Timer.timeStopped && !Pause.isPaused) {
            float posy = indicator.transform.position.y;;
            if ((posy <= -2.00f) || (posy >= 4.95f))
                speed *= -1;
            indicator.transform.position = new Vector2(indicator.transform.position.x, posy - speed*Time.deltaTime);
            if (timer.timeRemaining > 10.0f && timer.timeRemaining < 20.0f) {
                UpAndDownPosition();
            }
            else if (timer.timeRemaining < 10.0f) {
                RandomPosition();
            }
        }
    }

    public void Jump() {
        if (!Timer.timeStopped)
        {
            ropeAnim.SetTrigger("Pulo");
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

    private void UpAndDownPosition() {
        float posy = squareObj.transform.position.y;
        if ((posy <= -1.67f) || (posy >= 4.70f)) 
            speedSquareObj *= -1;
        squareObj.transform.position = new Vector2(squareObj.transform.position.x, posy + speedSquareObj*Time.deltaTime);
    }
    
    private void RandomPosition() {
        float posy = squareObj.transform.position.y;
        if (posy <= minPos) {
            speedSquareObj *= -1;
            maxPos = Random.Range(2.80f, 4.70f);
        }
        if (posy >= maxPos) {
            speedSquareObj *= -1;
            minPos = Random.Range(-1.67f, 2.50f);
        }
        squareObj.transform.position = new Vector2(squareObj.transform.position.x, posy - speedSquareObj*Time.deltaTime);
    }


}
