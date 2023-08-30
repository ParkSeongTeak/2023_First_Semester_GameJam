using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Help : UI_PopUp
{
    enum Buttons
    {
        Help,
    }

    private void Start()
    {
        Init();
    }
    void Init()
    {
        base.Init();
        Bind<Button>(typeof(Buttons));
        GameManager.Pause();
        BindEvent(GetButton((int)Buttons.Help).gameObject, Btn_Help);

    }

    void Btn_Help(PointerEventData data)
    {
        GameManager.UnPause();
        GameManager.UI.ClosePopupUI();
    }

}
