using UnityEngine;
using System.Collections;
using UnityEngine.EventSystems;
 
public class ButtonPressed : MonoBehaviour, IPointerDownHandler, IPointerUpHandler 
{
 
    public bool buttonPressed;

    public GameObject description;
    
    public void OnPointerDown(PointerEventData eventData){
        buttonPressed = true;
        description.SetActive(true);
        
    }
    
    public void OnPointerUp(PointerEventData eventData){
        buttonPressed = false;
        description.SetActive(false);
    }
}