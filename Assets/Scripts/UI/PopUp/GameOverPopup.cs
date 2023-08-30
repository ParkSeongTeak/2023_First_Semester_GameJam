using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using TMPro;

public class GameOverPopup : UI_PopUp
{
    enum Buttons
    {
        ReStart,
        ToMain,
    }
    enum Texts
    {
        WaveEnd,
        NowScoreEnd,
        BestScoreEnd,
    }

    void Start()
    {
        Init();
    }


    public override void Init()
    {
        base.Init();

        Bind<Button>(typeof(Buttons));
        Bind<TMP_Text>(typeof(Texts));

        Bind_Btn();
        GetText((int)Texts.WaveEnd).text = $"{GameManager.Data.Wave} WAVE";
        GetText((int)Texts.NowScoreEnd).text = $"SCORE: {GameManager.Data.NowPoint}";
        GetText((int)Texts.BestScoreEnd).text = $"BEST SCORE: {GameManager.Data.MaxPoint}";

    }


    #region Btn
    void Bind_Btn()
    {
        BindEvent(GetButton((int)Buttons.ReStart).gameObject, Btn_ReStart);
        BindEvent(GetButton((int)Buttons.ToMain).gameObject, Btn_ToMain);
    }
    void Btn_ReStart(PointerEventData evt)
    {
        GameManager.Scene.LoadScene(Define.Scenes.Game);
    }
    void Btn_ToMain(PointerEventData evt)
    {
        GameManager.Scene.LoadScene(Define.Scenes.Main);
    }
    #endregion Btn



}
