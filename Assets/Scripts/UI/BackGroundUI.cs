using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackGroundUI : EventTriggerEX
{
    // Start is called before the first frame update
    private void Start()
    {

        init();
    }
    protected override void OnPointerDown(PointerEventData data)
    {
        GameManager.UI.Plus_Button_False();
    }

}
