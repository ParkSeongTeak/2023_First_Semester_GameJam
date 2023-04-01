using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using TMPro;

public class EventTriggerEX : MonoBehaviour
{
    EventTrigger eventTrigger;
    protected void init()
    {
        eventTrigger = gameObject.GetComponent<EventTrigger>();
        EventTrigger.Entry entry_PointerDown = new EventTrigger.Entry();
        entry_PointerDown.eventID = EventTriggerType.PointerDown;
        entry_PointerDown.callback.AddListener((data) => { OnPointerDown((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerDown);

        EventTrigger.Entry entry_Drag = new EventTrigger.Entry();
        entry_Drag.eventID = EventTriggerType.Drag;
        entry_Drag.callback.AddListener((data) => { OnDrag((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_Drag);

        EventTrigger.Entry entry_EndDrag = new EventTrigger.Entry();
        entry_EndDrag.eventID = EventTriggerType.EndDrag;
        entry_EndDrag.callback.AddListener((data) => { OnEndDrag((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_EndDrag);

        EventTrigger.Entry entry_Click = new EventTrigger.Entry();
        entry_Click.eventID = EventTriggerType.PointerClick;
        entry_Click.callback.AddListener((data) => { OnPointerClick((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_Click);

        EventTrigger.Entry entry_BeginDrag = new EventTrigger.Entry();
        entry_BeginDrag.eventID = EventTriggerType.BeginDrag;
        entry_BeginDrag.callback.AddListener((data) => { OnBeginDrag((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_BeginDrag);



        EventTrigger.Entry entry_PointerExit = new EventTrigger.Entry();
        entry_PointerExit.eventID = EventTriggerType.PointerExit;
        entry_PointerExit.callback.AddListener((data) => { OnPointerExit((PointerEventData)data); });
        eventTrigger.triggers.Add(entry_PointerExit);
    }
    protected virtual void OnPointerExit(PointerEventData data)
    {
        //TODO
    }
    protected virtual void OnPointerDown(PointerEventData data)
    {
        //TODO
    }

    protected virtual void OnDrag(PointerEventData data)
    {
        //TODO
        //ex)
        //UiDragImage.transform.position = data.position;
        //드래그로 UI이미지를 끌고다니는 코드
    }

    protected virtual void OnEndDrag(PointerEventData data)
    {
        //TODO
    }

    protected virtual void OnPointerClick(PointerEventData data)
    {
        //TODO
    }
    protected virtual void OnBeginDrag(PointerEventData data)
    {
        //TODO
    }
 }