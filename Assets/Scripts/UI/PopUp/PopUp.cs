using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class UI_PopUp : UI_Base
{
    public override void Init()
    {
        GameManager.UI.SetCanvas(gameObject, true);
        SetResolution();

    }

    public virtual void ClosePopupUI()
    {
        GameManager.UI.ClosePopupUI(this);
    }
}
