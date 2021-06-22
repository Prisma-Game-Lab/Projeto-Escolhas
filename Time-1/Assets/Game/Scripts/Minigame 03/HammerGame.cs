using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class HammerGame : MonoBehaviour
{
    public GameObject indicator;
    public GameObject bar;
    public float speed;
    public Animator ropeAnim;
    private GameObject squareObj;
    public TextMeshProUGUI pointsText;
    private int point;
    float totalTime;

    void Start()
    {
        ropeAnim.enabled = false;
        squareObj = bar.transform.GetChild(0).gameObject;
        point = 0;
        Debug.Log(Timer.totalTime);
        //totalTime = (float)totalTime.totalTime;
        
        //StartCoroutine(ChangePosition());
    }

   
    void Update()
    {
        if (!Timer.timeStopped) {
            float posy = indicator.transform.position.y;
            if ((posy <= -2.16f) || (posy >= 4.63f))
                speed *= -1;
            indicator.transform.position = new Vector2(indicator.transform.position.x, posy - speed);
        }
    }

    public void Jump() {
        StartCoroutine(PlayAnimationOnce());
        CheckPositionIsEqual();
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
    /*
    private IEnumerator () {
        float sec = Random.Range(2.0f, 3.0f);
        float rndPos = Random.Range(-2.10f, 4.60f);
        yield return new WaitForSeconds(sec);
        squareObj.transform.position = new Vector2(squareObj.transform.position.x, rndPos);
    }
    */
    private IEnumerator RandomPosition() {
        while (!Timer.timeStopped) {
            float sec = Random.Range(2.0f, 3.0f);
            float rndPos = Random.Range(-2.10f, 4.60f);
            yield return new WaitForSeconds(sec);
            squareObj.transform.position = new Vector2(squareObj.transform.position.x, rndPos);
        }
    }


}
