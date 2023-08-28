using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class UI_PivotEventHandler : MonoBehaviour, IPointerClickHandler
{
    public Action<PointerEventData, object> OnClickHandler = null;
    public object Pivot { get; set; } = 0;

    public void OnPointerClick(PointerEventData eventData)
    {
        OnClickHandler?.Invoke(eventData, Pivot);
    }
}
