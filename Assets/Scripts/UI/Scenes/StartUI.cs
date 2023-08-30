using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class StartUI : UI_Scene
{
    enum Buttons
    {
        GameStart,
        GameEnd,
    }


    void Start()
    {
        Debug.Log("???");
        Init();
    }


    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));

        Bind_Btn();

    }


    #region Btn
    void Bind_Btn()
    {
        BindEvent(GetButton((int)Buttons.GameStart).gameObject, Btn_ToGameScene);
        BindEvent(GetButton((int)Buttons.GameEnd).gameObject, Btn_ExitApp);
    }
    void Btn_ToGameScene(PointerEventData evt)
    {
        Debug.Log("Click");
        GameManager.Scene.LoadScene(Define.Scenes.Game);
    }
    void Btn_ExitApp(PointerEventData evt)
    {
        Debug.Log("???????");

        GameManager.QuitApp();
    }
    #endregion Btn
}
