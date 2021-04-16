using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class Draw : MonoBehaviour
{
    public Camera mainCamera;

    public GameObject brush;

    LineRenderer currentLineRenderer;

    Vector2 lastPos;

    List<List<float>> pointsList = new List<List<float>>();

    public Text text;

    //Timer time;

    [System.Serializable]
    public class Templates {
        public List<string> names = new List<string>();
        public Sprite image;
    }

    public List<Templates> templates = new List<Templates>();

    public GameObject squareImg1, squareImg2, squareImg3, squareImg4;

    float squarePos;

    bool hasDrawn = false;

    private List<List<string>> _nameDrawing = new List<List<string>>();

    private bool _sort = false;

    private float _x1, _x2, _x3, _x4;

    private float _y1, _y2, _y3, _y4;

    List<GameObject> img = new List<GameObject>();

    List<int> rnd = new List<int>();

    private List<List<string>> _storeString = new List<List<string>>();

    int n = 1;

    List<int> aux = new List<int>();

    void Start() {
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
        sortDrawing();
    }

    public void Minigame() {

    }

    private void sortDrawing() {
        _sort = false;
        int rnd2 = Random.Range(0,aux.Count);
        int j = rnd2;
        rnd2 = aux[j];
        rnd.Add(rnd2);
        aux.RemoveAt(j);
        int rndTemplate = Random.Range(0,4);
        List<string> template = templates[rndTemplate].names;
        _nameDrawing.Add(template);
        _storeString[rnd2][0] = templates[rndTemplate].names[0];
        _storeString[rnd2][1] = templates[rndTemplate].names[1];
        img[rnd2].GetComponent<Image>().sprite = templates[rndTemplate].image;
        if (aux.Count > 0) {
            j = Random.Range(0,aux.Count);
            rnd2 = aux[j];
        }
    }

    private void Update() {
        if (_sort) {
            if (aux.Count > 0) {
                n = Random.Range(1,aux.Count + 1);
                sortDrawing();
            }
        }
        if (!Timer.timeStopped && !Pause.isPaused)
            Drawing();
    }

    void Drawing() {
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            CreateBrush();
        }
        else if (Input.GetKey(KeyCode.Mouse0)) {
            PointToMousePos();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0) && hasDrawn) {
            currentLineRenderer.positionCount = 0;
            if (pointsList.Count >= 2) {
                int i = 0;
                for (; i < 4; i++) {
                    if (_storeString[i][0] != "0") {
                        bool result1 = OneDollar.Result(pointsList, _storeString[i][0], 0.35f);
                        bool result2 = OneDollar.Result(pointsList, _storeString[i][1], 0.35f);
                        if (result1 || result2) {
                            Debug.Log("true");
                            img[i].GetComponent<Image>().sprite = null;
                            _storeString[i][0] = "0";
                            _storeString[i][1] = "0";
                            aux.Add(i);
                            _sort = true;
                            _nameDrawing.Clear();
                            rnd.Clear();
                            break;
                        }
                    }
                } 
                if (i == 4) {
                    //Debug.Log("false");
                    //_sort = true;
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
        }
    }

    void AddAPoint(Vector2 pointPos) {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
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