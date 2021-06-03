using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerController : MonoBehaviour
{
    FloatingJoystick joyStick;

    public float rotationSpeed;
    float rotationX = 0;

    private void Start()
    {
        joyStick = FindObjectOfType<FloatingJoystick>();
    }


    void Update()
    {

        if (!Pause.isPaused && !joyStick.enabled)
            joyStick.enabled = true;
        else if (!Pause.isPaused)
            lookJoystickDirection();
        else if (Pause.isPaused && joyStick.enabled)
            joyStick.enabled = false;
    }

    private void lookJoystickDirection()
    {
        Vector2 direction = joyStick.Direction;
        if (direction != Vector2.zero)
        {
            float angle = Mathf.Atan2(direction.y, direction.x) * Mathf.Rad2Deg;
            transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle - 90f, Vector3.forward), 0.2f);
        }
    }

    //PARA MOBILE   
    /*private void lookTouchPos()
    {
        Touch touch = Input.GetTouch(0);
        Vector3 direcao = touch.position - (Vector2)Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle - 90f, Vector3.forward);
    }
    */



    //PARA WEB
    /*
    private void lookMousePos()
    {
        Vector3 direcao = Input.mousePosition - Camera.main.WorldToScreenPoint(transform.position);
        float angle = Mathf.Atan2(direcao.y, direcao.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.AngleAxis(angle - 90f, Vector3.forward),0.3f);
    }
    */
}
