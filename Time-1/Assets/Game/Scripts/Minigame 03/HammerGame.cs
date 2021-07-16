using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HammerGame : MonoBehaviour
{
    public GameObject tutorial;
    public GameObject indicator;
    public GameObject bar;
    public GameObject cima;
    public GameObject baixo;
    public float speed;
    public float speedSquareObj;
    public Animator ropeAnim;
    public GameObject squareObj;
    public TextMeshProUGUI pointsText;
    [HideInInspector] public int point;
    [HideInInspector] public int totalPossiblePoints;
    [HideInInspector] public bool outOfSquareBounds;
    float totalTime;
    private Timer timer;
    private float minPos;
    private float maxPos;
    private float randomMaxPos;
    private float randomMinPos;

    void Start()
    {
        totalPossiblePoints = 0;
        point = 0;
        totalTime = Timer.totalTime;
        timer = this.GetComponent<Timer>();
        minPos = baixo.transform.position.y;
        maxPos = cima.transform.position.y;
        randomMaxPos = maxPos;
        randomMinPos = minPos;
        outOfSquareBounds = true;

        AppSave appSave = SaveSystem.GetInstance().appSave;
        if (appSave.tutorialMinigame3)
        {
            appSave.tutorialMinigame3 = false;
            SaveSystem.GetInstance().SaveState();
            tutorial.SetActive(true);
            Time.timeScale = 0f;
            Pause.isPaused = true;
        }
    }


    void Update()
    {

        if(Input.GetMouseButtonDown(0) && Input.mousePosition.y < 400) 
        {
            Jump();
        }
        Debug.LogWarning(Input.mousePosition.y);

        if (!Timer.timeStopped && !Pause.isPaused)
        {
            float posy = indicator.transform.position.y; ;
            if ((posy <= minPos+0.1) || (posy >= maxPos-0.1))
                speed *= -1;
            indicator.transform.position = new Vector2(indicator.transform.position.x, posy - speed * Time.deltaTime);
            if (timer.timeRemaining > 10.0f && timer.timeRemaining < 20.0f)
            {
                UpAndDownPosition();
            }
            else if (timer.timeRemaining < 10.0f)
            {
                RandomPosition();
            }
        }
    }

    public void Jump()
    {
        if (!Timer.timeStopped)
        {
            ropeAnim.SetTrigger("Pulo");
            CheckPositionIsEqual();
        }
    }

    private void CheckPositionIsEqual()
    {
        float sqHeight = squareObj.GetComponent<SpriteRenderer>().bounds.size.y;
        float posy = indicator.transform.position.y;
        float sqPosY = squareObj.transform.position.y;
        if ((posy < sqPosY + sqHeight * 0.5f) && (posy > sqPosY - sqHeight * 0.5f) && outOfSquareBounds)
        {
            outOfSquareBounds = false;
            point++;
            pointsText.text = point.ToString();
        }
    }

    private void UpAndDownPosition()
    {
        float posy = squareObj.transform.position.y;
        if ((posy <= minPos + 0.3f) || (posy >= maxPos - 0.3f))
            speedSquareObj *= -1;
        squareObj.transform.position = new Vector2(squareObj.transform.position.x, posy + speedSquareObj * Time.deltaTime);
    }

    private void RandomPosition()
    {
        float posy = squareObj.transform.position.y;
        if (posy <= randomMinPos)
        {
            speedSquareObj *= -1;
            randomMaxPos = Random.Range(2.80f, maxPos-0.3f);
        }
        if (posy >= randomMaxPos)
        {
            speedSquareObj *= -1;
            randomMinPos = Random.Range(minPos+0.3f, 2.50f);
        }
        squareObj.transform.position = new Vector2(squareObj.transform.position.x, posy - speedSquareObj * Time.deltaTime);
    }

    //private void RandomPosition()
    //{
    //    float posy = squareObj.transform.position.y;
    //    if (posy <= minPos)
    //    {
    //        speedSquareObj *= -1;
    //        maxPos = Random.Range(2.80f, 4.70f);
    //    }
    //    if (posy >= maxPos)
    //    {
    //        speedSquareObj *= -1;
    //        minPos = Random.Range(-1.67f, 2.50f);
    //    }
    //    squareObj.transform.position = new Vector2(squareObj.transform.position.x, posy - speedSquareObj * Time.deltaTime);
    //}
}
