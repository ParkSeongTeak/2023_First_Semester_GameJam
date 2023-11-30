using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;
//using UnityEngine.UIElements;

public abstract class UI_Base : MonoBehaviour
{
    /// <summary>
    /// 관리할 object (GameObject, Text, Image 등 통칭) 자료구조
    /// </summary>
    protected Dictionary<Type, UnityEngine.Object[]> _objects = new Dictionary<Type, UnityEngine.Object[]>();

    /// <summary>
    /// initiate 함수
    /// </summary>
    public abstract void Init();

    /// <summary>
    /// T 타입 objects dic에 저장
    /// </summary>
    /// <typeparam name="T"> 해당 타입 </typeparam>
    /// <param name="type"> 해당 타입 정보 가진 enum </param>
    protected void Bind<T>(Type type) where T : UnityEngine.Object
    {
        string[] names = Enum.GetNames(type);
        UnityEngine.Object[] objs = new UnityEngine.Object[names.Length];
        _objects.Add(typeof(T), objs);

        for (int i = 0; i<names.Length; i++)
        {
            objs[i] = Util.FindChild<T>(gameObject, names[i], true);

            if (objs[i] == null)
            {
                Debug.Log($"Failed to Bind : {names[i]} on {gameObject.name}");
            }
        }
    }
    /// <summary>
    /// 해당 game object에 이벤트 할당
    /// </summary>
    /// <param name="action">할당할 이벤트</param>
    /// <param name="type">이벤트 발생 조건</param>
    public static void BindEvent(GameObject go, Action<PointerEventData> action, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_EventHandler evt = Util.GetOrAddComponent<UI_EventHandler>(go);

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
            case Define.UIEvent.Drag:
                evt.OnDragHandler -= action;
                evt.OnDragHandler += action;
                break;
            case Define.UIEvent.DragStart:
                evt.OnBeginDragHandler -= action;
                evt.OnBeginDragHandler += action;
                break;
            case Define.UIEvent.DragEnd:
                evt.OnEndDragHandler -= action;
                evt.OnEndDragHandler += action;
                break;
        }
    }

    /// <summary>
    /// bind된 object에서 원하는 object 얻기
    /// </summary>
    protected T Get<T>(int idx) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objects = null;
        if (_objects.TryGetValue(typeof(T), out objects) == false)
            return null;

        return objects[idx] as T;
    }
    #region Get_Override
    protected GameObject GetGameObject(int idx) => Get<GameObject>(idx);
    protected TMP_Text GetText(int idx) => Get<TMP_Text>(idx);

    protected Image GetImage(int idx) => Get<Image>(idx);
    protected Button GetButton(int idx) => Get<Button>(idx);
    #endregion Get_Override

   

    /// <summary> 반복문으로 사용하기 위한 index 사용 event 할당 </summary>
    public static void BindEvent(GameObject go, Action<PointerEventData, object> action, object pivot, Define.UIEvent type = Define.UIEvent.Click)
    {
        UI_PivotEventHandler evt = Util.GetOrAddComponent<UI_PivotEventHandler>(go);
        evt.Pivot = pivot;

        switch (type)
        {
            case Define.UIEvent.Click:
                evt.OnClickHandler -= action;
                evt.OnClickHandler += action;
                break;
        }
    }
 
    public void SetResolution()
    {
        float setWidth = 1080; // 사용자 설정 너비
        float setHeight = 2340; // 사용자 설정 높이

        float deviceWidth = Screen.width; // 기기 너비 저장
        float deviceHeight = Screen.height; // 기기 높이 저장

        CanvasScaler _canvasScaler;
        _canvasScaler = GetComponent<CanvasScaler>();

        Screen.SetResolution((int)setWidth, (int)(((float)deviceHeight / deviceWidth) * setWidth), true);

        _canvasScaler.uiScaleMode = CanvasScaler.ScaleMode.ScaleWithScreenSize;
        _canvasScaler.referenceResolution = new Vector2(setWidth, setHeight);
        _canvasScaler.screenMatchMode = CanvasScaler.ScreenMatchMode.MatchWidthOrHeight;

        if (setWidth / setHeight < deviceWidth / deviceHeight)
        {
            _canvasScaler.matchWidthOrHeight = 1f;
        }
        else
        {
            _canvasScaler.matchWidthOrHeight = 0f;
        }
    }


}
