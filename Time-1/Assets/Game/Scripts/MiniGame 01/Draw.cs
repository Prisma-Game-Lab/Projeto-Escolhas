using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Draw : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject brush;
    [HideInInspector] public LineRenderer currentLineRenderer;
    Vector2 lastPos;
    List<List<float>> pointsList = new List<List<float>>();
    public Text text;

    [System.Serializable]
    public class Templates {
        public List<string> names = new List<string>();
        public Sprite image;
    }

    public List<Templates> templates = new List<Templates>();
    public GameObject squareImg1, squareImg2, squareImg3, squareImg4;
    float squarePos;
    bool hasDrawn = false;
    private float _x1, _x2, _x3, _x4;
    private float _y1, _y2, _y3, _y4;
    List<GameObject> img = new List<GameObject>();
    List<int> rnd = new List<int>();
    private List<List<string>> _storeString = new List<List<string>>();
    List<int> aux = new List<int>();
    public int point;
    public TextMeshProUGUI pointsText;

    private bool finishedTime = false;

    public float resetDrawsTime;
    
    public int totalDraws;

    private AudioManager audioManager;


    void Start() {
        audioManager = FindObjectOfType<AudioManager>();
        _y3 = squareImg3.gameObject.transform.position.y;
        img.Add(squareImg1);
        img.Add(squareImg2);
        img.Add(squareImg3);
        img.Add(squareImg4);
        squarePos = _y3 - _y3/2;
        _storeString.Add(new List<string>{"0", "0"});
        _storeString.Add(new List<string>{"0", "0"});
        _storeString.Add(new List<string>{"0", "0"});
        _storeString.Add(new List<string>{"0", "0"});
        aux.Add(0);
        aux.Add(1);
        aux.Add(2);
        aux.Add(3);
        point = 0;
        resetDrawsTime = 3.5f;
        totalDraws = 0;
        sortMany();
        StartCoroutine(sortThenErase(resetDrawsTime));
    }

    private void sortDrawing() {
        int rnd2 = Random.Range(0,aux.Count);
        int j = rnd2;
        rnd2 = aux[j];
        rnd.Add(rnd2);
        aux.RemoveAt(j);
        int rndTemplate = Random.Range(0,4);
        List<string> template = templates[rndTemplate].names;
        _storeString[rnd2][0] = templates[rndTemplate].names[0];
        _storeString[rnd2][1] = templates[rndTemplate].names[1];
        img[rnd2].GetComponent<Image>().sprite = templates[rndTemplate].image;
        if (aux.Count > 0) {
            j = Random.Range(0,aux.Count);
            rnd2 = aux[j];
        }
    }

    public void eraseDrawing(int pos) {
        img[pos].GetComponent<Image>().sprite = null;
        _storeString[pos][0] = "0";
        _storeString[pos][1] = "0";
        aux.Add(pos);
        rnd.Clear();
    }

    private IEnumerator sortThenErase(float waitTime) {
        while (!Timer.timeStopped && !Pause.isPaused) {
            yield return new WaitForSeconds(waitTime);
            for (int i = 0; i < 4; i++) {
                if (!aux.Contains(i))
                    eraseDrawing(i);
            }
            sortMany();
        }
    }

    private void sortMany() {
        audioManager.Play("DrawSorted");
        totalDraws++;
        int n = Random.Range(3, aux.Count + 1);
        for (int i = 0; i < n; i++)
            sortDrawing();
    }

    private void Update() {
        if (!Timer.timeStopped && !Pause.isPaused)
            Drawing();
        else if (!finishedTime)
        {
            StopAllCoroutines();
            if (currentLineRenderer != null)
                currentLineRenderer.positionCount = 0;
            finishedTime = true;
        }
    }

    void Drawing() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            CreateBrush();
        }
        else if (Input.GetKey(KeyCode.Mouse0)) {
            PointToMousePos();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && hasDrawn) {
            if(currentLineRenderer!=null)
                currentLineRenderer.positionCount = 0;
            if (pointsList.Count >= 2) {
                int i = 0;
                for (; i < 4; i++) {
                    if (_storeString[i][0] != "0") {
                        bool result1 = OneDollar.Result(pointsList, _storeString[i][0], 0.35f);
                        bool result2 = OneDollar.Result(pointsList, _storeString[i][1], 0.35f);
                        if (result1 || result2) {
                            audioManager.Play("DrawCompleted");
                            eraseDrawing(i);
                            if (aux.Count == 4) {
                                point++;
                                pointsText.text = point.ToString();
                                StopAllCoroutines();
                                sortMany();
                                StartCoroutine(sortThenErase(resetDrawsTime));
                            }
                            break;
                        }
                    }
                } 
            }
            currentLineRenderer = null;
            pointsList.Clear();
        }
        else {
            currentLineRenderer = null;
        }
    }

    void CreateBrush() {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        hasDrawn = false;
        if (mousePos.y < squarePos) {
            hasDrawn = true;
            GameObject brushInstance = Instantiate(brush);
            currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
            currentLineRenderer.SetPosition(0, mousePos);
            currentLineRenderer.SetPosition(1, mousePos);
            Destroy(brushInstance, 4);
        }
    }

    void AddAPoint(Vector2 pointPos) {
        if (currentLineRenderer != null)
        {
            currentLineRenderer.positionCount++;
            int positionIndex = currentLineRenderer.positionCount - 1;
            currentLineRenderer.SetPosition(positionIndex, pointPos);
        }
    }

    void PointToMousePos() {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (lastPos != mousePos && mousePos.y < squarePos) {
            if (!hasDrawn)
                CreateBrush();
            AddAPoint(mousePos);
            lastPos = mousePos;
            pointsList.Add(new List<float>{lastPos.x, lastPos.y});
        }
    }
}