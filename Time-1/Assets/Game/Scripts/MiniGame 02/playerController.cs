using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    //PARA MOBILE E WEB
    public float rotationSpeed;
    float rotationX=0;

    void Update()
    {
        //Movimento seguinto eixo x do mouse ou do touch
        //PARA WEB
        //playerMouseXRotation();

        //PARA MOBILE
        /*
        if (Input.touchCount > 0)
        {
            playerTouchXRotation();
        }*/

        //Movimento seguindo mouse ou touch
        if(!Pause.isPaused)
            lookMousePos();
    }
    //PARA MOBILE   
    private void lookTouchPos()
    {
        Touch touch = Input.GetTouch(0);
        Vector3 direcao = touch.position - (Vector2)Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
    }
    private void playerTouchXRotation()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Moved)
        {
            Quaternion Xrotation = Quaternion.Euler(0f, 0f, -touch.deltaPosition.x * rotationSpeed);
            transform.rotation = Xrotation * transform.rotation;

            //rotationX += -touch.deltaPosition.x*rotationSpeed*Time.deltaTime;
            //rotationX = normalizeAngle(rotationX);
            //Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.forward);
            //transform.localRotation = xQuaternion;
        }
    }





    //PARA WEB
    private void lookMousePos()
    {
        Vector3 direcao = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle - 90f, Vector3.forward),0.3f);
    }
    private void playerMouseXRotation()
    {
        if (Input.GetMouseButton(0))
        {
            rotationX += Input.GetAxis("Mouse X") * rotationSpeed;
            rotationX = normalizeAngle(rotationX);
            Quaternion xQuaternion = Quaternion.AngleAxis(rotationX, Vector3.forward);
            transform.localRotation = xQuaternion;
        }
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
