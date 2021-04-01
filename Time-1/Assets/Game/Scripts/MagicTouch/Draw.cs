using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Draw : MonoBehaviour
{
    public Camera mainCamera;
    public GameObject brush;

    LineRenderer currentLineRenderer;

    Vector2 lastPos;

    List<List<float>> pointsList = new List<List<float>>();

    List<string> names = new List<string>();

    public Text text;

    private Dictionary<List<string>,Texture2D> _templates = new Dictionary<List<string>,Texture2D>();

    //{{[caret1, caret2], img},{}}

    void Start() {
        names.Add("caret");
        names.Add("star");
    }

    private void Update() {
        Drawing();
    }

    void Drawing() {
        //int r = Random.Range(0, names.Count);
        //text.text = names[r];
        if (Input.GetKeyDown(KeyCode.Mouse0)) {
            CreateBrush();
        }
        else if (Input.GetKey(KeyCode.Mouse0)) {
            PointToMousePos();
        }
        else if (Input.GetKeyUp(KeyCode.Mouse0)) {
            currentLineRenderer.positionCount = 0;
            if (pointsList.Count >= 2) {
                bool d = OneDollar.Result(pointsList, "caret", 0.35f);
                if (d) {
                    text.text = "Correto";
                }
                else {
                    text.text = "Errado";
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
        GameObject brushInstance = Instantiate(brush);
        currentLineRenderer = brushInstance.GetComponent<LineRenderer>();
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        currentLineRenderer.SetPosition(0, mousePos);
        currentLineRenderer.SetPosition(1, mousePos);
    }

    void AddAPoint(Vector2 pointPos) {
        currentLineRenderer.positionCount++;
        int positionIndex = currentLineRenderer.positionCount - 1;
        currentLineRenderer.SetPosition(positionIndex, pointPos);
    }

    void PointToMousePos() {
        Vector2 mousePos = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        if (lastPos != mousePos) {
            AddAPoint(mousePos);
            lastPos = mousePos;
            pointsList.Add(new List<float>{lastPos.x, lastPos.y});
        }
    }
}