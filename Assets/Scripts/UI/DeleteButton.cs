using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

using TMPro;
public class DeleteButton : MonoBehaviour
{
    EventTrigger eventTrigger;
    private void Start()
    {
        eventTrigger = gameObject.GetComponent<EventTrigger>();
        
        EventTrigger.Entry entry_PointerExit_Plus = new EventTrigger.Entry();
        entry_PointerExit_Plus.eventID = EventTriggerType.PointerExit;
        entry_PointerExit_Plus.callback.AddListener((data) => { PointerExit_Plus((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerExit_Plus);

    }
    void PointerExit_Plus(PointerEventData data)
    {
        gameObject.SetActive(false);

    }
    
    
}
