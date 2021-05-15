using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    public float rotationSpeed;
    float rotationX=0;

    private Touch touch;
    private Quaternion Xrotation;

    void Update()
    {
        if (Input.touchCount > 0)
        {
            touch = Input.GetTouch(0);
            if (touch.phase == TouchPhase.Moved)
            {
                Xrotation = Quaternion.Euler(0f, 0f, -touch.deltaPosition.x * rotationSpeed);
                transform.rotation = Xrotation * transform.rotation;
                //rotationX += -touch.deltaPosition.x*rotationSpeed*Time.deltaTime;
                //rotationX = normalizeAngle(rotationX);
                //Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.forward);
                //transform.localRotation = xQuaternion;
            }
        }

        //FUNCIONA PARA WEB
        //if (Input.GetMouseButton(0))
        //{
        //    rotationX += Input.GetAxis("Mouse X") * rotationSpeed;
        //    rotationX = normalizeAngle(rotationX);
        //    Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.forward);
        //    transform.localRotation = xQuaternion;
        //}

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
