using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    float rotationX=0;
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        // if (Input.GetMouseButton(0))
        // {
        float h = 2f * Input.GetAxis("Mouse X");
        Debug.Log(h);
        transform.Rotate(0, 0, h);
            //rotationX += Input.GetAxis("Mouse X") * 10f;
            //rotationX = normalizeAngle(rotationX);
           //print(Input.GetAxis("Mouse X") * 10f);
            //Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.forward);
            //transform.localRotation = xQuaternion;
            
       // }
    }

    private float normalizeAngle(float angle)
    {
        if (angle < -360)
            angle += 360;
        else if (angle > 360)
            angle -= 360;
        return angle;
    }
}
