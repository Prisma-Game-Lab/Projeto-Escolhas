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
    private bool canJump;
    [HideInInspector] public bool canMakePoint;
    AudioManager audioManager;

    void Start()
    {
        audioManager = GameObject.FindGameObjectWithTag("persistentData").GetComponent<AudioManager>();
        canJump = true;
        canMakePoint = true;
        totalPossiblePoints = 0;
        point = 0;
        totalTime = Timer.totalTime;
        timer = this.GetComponent<Timer>();
        minPos = baixo.transform.position.y;
        maxPos = cima.transform.position.y;
        randomMaxPos = maxPos-0.35f;
        randomMinPos = minPos+0.35f;
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

        if(Input.GetMouseButtonDown(0) && Input.mousePosition.y < 400 && canJump && !Timer.timeStopped && !Pause.isPaused) 
        {
            canJump = false;
            StartCoroutine(Jump());
        }
        //Debug.LogWarning(Input.mousePosition.y);

        if (!Timer.timeStopped && !Pause.isPaused)
        {
            float posy = indicator.transform.position.y;
            if ((posy <= minPos))
            {
                //print("vai pra cima");
                speed *= -1;
                //print(indicator.transform.position.y);
                indicator.transform.position = new Vector2(indicator.transform.position.x, minPos+0.10f);
                //print(indicator.transform.position.y);
            }
            else if(posy >= maxPos)
            {
                //print("vai pra baixo");
                speed *= -1;
                //print(indicator.transform.position.y);
                indicator.transform.position = new Vector2(indicator.transform.position.x, maxPos-0.10f);
                //print(indicator.transform.position.y);
            }
            indicator.transform.Translate(0,speed * Time.deltaTime,0);
            if (timer.timeRemaining > 10.0f && timer.timeRemaining < 20.0f)
            {
                UpAndDownPosition();
            }
            else if (timer.timeRemaining < 10.0f)
            {
                RandomPosition();
            }
            //print(indicator.transform.position.y);
        }
    }

    public IEnumerator Jump()
    {
        if (!Timer.timeStopped)
        {
            ropeAnim.SetTrigger("Pulo");
            if (!outOfSquareBounds && canMakePoint) {
                audioManager.Play("RightJump");
                point++;
                pointsText.text = point.ToString();
                canMakePoint = false;
            }else
                audioManager.Play("WrongJump");
            //CheckPositionIsEqual();
        }
        yield return new WaitForSeconds(0.4f);
        canJump = true;
    }

    //private void CheckPositionIsEqual()
    //{
    //    float sqHeight = squareObj.GetComponent<SpriteRenderer>().bounds.size.y;
    //    float posy = indicator.transform.position.y;
    //    float sqPosY = squareObj.transform.position.y;
    //    if ((posy < sqPosY + sqHeight * 0.5f) && (posy > sqPosY - sqHeight * 0.5f) && outOfSquareBounds)
    //    {
    //        outOfSquareBounds = false;
    //        point++;
    //        pointsText.text = point.ToString();
    //    }
    //}

    private void UpAndDownPosition()
    {
        float posy = squareObj.transform.position.y;
        if ((posy <= minPos + 0.35f))
        {
            speedSquareObj *= -1;
            squareObj.transform.position = new Vector2(squareObj.transform.position.x, minPos + 0.4f);
        }
        else if ((posy >= maxPos - 0.35f))
        {
            speedSquareObj *= -1;
            squareObj.transform.position = new Vector2(squareObj.transform.position.x, maxPos - 0.4f);
        }
        squareObj.transform.Translate(0, speedSquareObj * Time.deltaTime,0);
    }

    private void RandomPosition()
    {
        float posy = squareObj.transform.position.y;
        if (posy <= randomMinPos)
        {
            speedSquareObj *= -1;
            randomMaxPos = Random.Range(2.80f, maxPos - 0.35f);
            squareObj.transform.position = new Vector2(squareObj.transform.position.x, randomMinPos+0.05f);
        }
        else if (posy >= randomMaxPos)
        {
            speedSquareObj *= -1;
            randomMinPos = Random.Range(minPos+0.35f, 2.50f);
            squareObj.transform.position = new Vector2(squareObj.transform.position.x, randomMaxPos-0.05f);
        }
        squareObj.transform.Translate(0, speedSquareObj * Time.deltaTime, 0);
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
