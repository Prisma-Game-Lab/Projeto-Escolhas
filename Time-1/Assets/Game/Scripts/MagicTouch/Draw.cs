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

    public TextMeshProUGUI time;

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

    public List<GameObject> img = new List<GameObject>();

    int rnd;

    int n = 1;

    void Start() {
        _y3 = squareImg3.gameObject.transform.position.y;
        img.Add(squareImg1);
        img.Add(squareImg2);
        img.Add(squareImg3);
        img.Add(squareImg4);
        squarePos = _y3 - _y3/2;
        sortDrawing(n);
    }

    public void Minigame() {

    }

    private void sortDrawing(int n) {
        rnd = Random.Range(0,4);
        for (int i = 0; i < n; i++) {
            int rndTemplate = Random.Range(0,4);
            List<string> template = templates[rndTemplate].names;
            _nameDrawing.Add(template);
            img[rnd].GetComponent<Image>().sprite = templates[rndTemplate].image;
        }
        _sort = false;
    }

    private void Update() {
        if (_sort) {
            n = Random.Range(1,5);
            n = 1;
            sortDrawing(n);
        }
        if (!time.text.Contains("00:00"))
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
                bool result1 = false;
                bool result2 = false;
                for (int i = 0; i < n; i++) {
                    result1 = OneDollar.Result(pointsList, _nameDrawing[0][0], 0.35f);
                    result2 = OneDollar.Result(pointsList, _nameDrawing[0][1], 0.35f);
                }
                if (result1 || result2) {
                    text.text = "Correto";
                    img[rnd].GetComponent<Image>().sprite = null;
                    _sort = true;
                    _nameDrawing.Clear();
                }
                else 
                    text.text = "Errado";
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