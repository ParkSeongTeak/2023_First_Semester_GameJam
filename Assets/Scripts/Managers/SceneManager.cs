using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class SceneManager
{
    public void init()
    {

    }

    public void LoadScene(Define.Scenes scene)
    {
        Application.LoadLevel(Enum.GetName(typeof(Define.Scenes), scene));
        GameManager.UI.CloseAllPopupUI();
        Time.timeScale = 1.0f;

    }

    public void Clear()
    {

    }
}
