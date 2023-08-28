using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;
using static Define;


public class UIManager 
{

    
    TextMeshProUGUI _text = new TextMeshProUGUI();

    GameObject[] lifeArray = new GameObject[5];
    //GameObject GameOverScene;
    GameObject _UI;
    GameObject _UIField;

    GameObject _mainScene;
    Sprite FullLife;
    Sprite emptyLife;

    GameObject[] _buttonsLV;
    readonly int FULLLIFE = 5;

    TextMeshProUGUI[] _textFields;
    GameObject[] _elseFields;



    Sprite[] _LVImage;
    //public GameObject MainScene { get { return _mainScene; } set { _mainScene = value; } }
    public Sprite[] LVImage { get { return _LVImage; } }

    public GameObject[] ButtonsLV { get {  return _buttonsLV; } }
    public GameObject[] ElseFields { get { return _elseFields; } }

    int idx = 0;


    public void PointUpdate()
    {
        _textFields[(int)TextEnum.WavePoint].text = $"{GameManager.Instance.Wave} WAVE";
        _textFields[(int)TextEnum.NowPoint].text = $"SCORE: {GameManager.Instance.NowPoint}";
        _textFields[(int)TextEnum.MaxPoint].text = $"BEST SCORE: {GameManager.Instance.MaxPoint}";
        _textFields[(int)TextEnum.MoneyPoint].text = $"{(int)GameManager.Instance.Money}";
        

    }
    
    


    /////////////////////////////////////////////////////////////////

    int _order = 10;

    Stack<UI_PopUp> _popupStack = new Stack<UI_PopUp>();
    UI_Scene _sceneUI = null;

    public GameObject Root
    {
        get
        {
            GameObject root = GameObject.Find("UI_Root");
            if (root == null)
                root = new GameObject { name = "UI_Root" };
            return root;
        }
    }

    public void SetCanvas(GameObject go, bool sort = true)
    {
        Canvas canvas = Util.GetOrAddComponent<Canvas>(go);
        canvas.renderMode = RenderMode.ScreenSpaceOverlay;
        canvas.overrideSorting = true;

        if (sort)
        {
            canvas.sortingOrder = _order;
            _order++;
        }
        else
        {
            canvas.sortingOrder = -3;
        }
    }

    public T ShowSceneUI<T>(string name = null) where T : UI_Scene
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        T sceneUI = Util.FindChild<T>(Root, name);
        if (sceneUI == null)
        {
            GameObject go = GameManager.Resource.Instantiate($"UI/Scene/{name}");
            sceneUI = Util.GetOrAddComponent<T>(go);
            _sceneUI = sceneUI;

            go.transform.SetParent(Root.transform);


        }
        return sceneUI;

    }

    public T ShowPopupUI<T>(string name = null) where T : UI_PopUp
    {
        if (string.IsNullOrEmpty(name))
            name = typeof(T).Name;

        GameObject go = GameManager.Resource.Instantiate($"UI/Popup/{name}");
        T popup = Util.GetOrAddComponent<T>(go);
        _popupStack.Push(popup);

        go.transform.SetParent(Root.transform);

        return popup;
    }

    public void ClosePopupUI(UI_PopUp popup)
    {
        if (_popupStack.Count == 0)
            return;

        if (_popupStack.Peek() != popup)
        {
            Debug.Log("Close Popup Failed!");
            return;
        }

        ClosePopupUI();
    }

    public void ClosePopupUI()
    {
        if (_popupStack.Count == 0)
            return;

        UI_PopUp popup = _popupStack.Pop();
        GameManager.Resource.Destroy(popup.gameObject);
        popup = null;
        _order--;
    }

    public void CloseAllPopupUI()
    {
        while (_popupStack.Count > 0)
            ClosePopupUI();
    }
}

