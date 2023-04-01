using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class BackGroundUI : MonoBehaviour
{
    
    EventTrigger eventTrigger;

    // Start is called before the first frame update
    void Start()
    {

        eventTrigger = gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry_PointerDown_Plus = new EventTrigger.Entry();
        entry_PointerDown_Plus.eventID = EventTriggerType.PointerDown;
        entry_PointerDown_Plus.callback.AddListener((data) => { OnPointerDown_Plus((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerDown_Plus);

    }
    void OnPointerDown_Plus(PointerEventData data)
    {
        GameManager.UI.Plus_Button_False();
    }
    
}
