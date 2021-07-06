using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class AttackSlider : MonoBehaviour, IPointerUpHandler
{
    Slider attackSlider;
    public BattleUIManager battleUIManager;
    private void Start() {
        
        attackSlider = GetComponent<Slider>();
        
    }

    public void OnPointerUp(PointerEventData ped)
    {
        if(attackSlider.value >= 0.9)
        {
            attackSlider.value = 1;
            battleUIManager.OnAttackButton();
        }
        else
        {
            attackSlider.value = 0;
        }
    }
}
