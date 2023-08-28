using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Option : UI_PopUp
{
    enum Buttons
    {
        Restart,
        End,
        Esc,
    }

    enum Sliders
    {
        BGMSlider,
        SFXSlider,
    }



    void Start()
    {
        Init();
    }

    public override void Init()
    {
        base.Init();
        GameManager.Pause();
        Bind<Button>(typeof(Buttons));
        Bind<Slider>(typeof(Sliders));

        Bind_Btn();

        Get<Slider>((int)Sliders.BGMSlider).onValueChanged.AddListener(delegate { VolumeChange(Define.Sounds.BGM); });
        Get<Slider>((int)Sliders.SFXSlider).onValueChanged.AddListener(delegate { VolumeChange(Define.Sounds.SFX); });
    }

    #region Btn

    void Bind_Btn()
    {
        BindEvent(GetButton((int)Buttons.Restart).gameObject, Btn_Restart);
        BindEvent(GetButton((int)Buttons.End).gameObject, Btn_End);
        BindEvent(GetButton((int)Buttons.Esc).gameObject, Btn_Esc);
    }

    void Btn_Restart(PointerEventData data)
    {
        GameManager.Scene.LoadScene(Define.Scenes.Game);
    }
    void Btn_End(PointerEventData data)
    {
        GameManager.QuitApp();
    }
    void Btn_Esc(PointerEventData data)
    {
        GameManager.UnPause();
        GameManager.UI.ClosePopupUI();
    }
    #endregion Btn

    void VolumeChange(Define.Sounds Sound)
    {
        float volume;
        if (Sound == Define.Sounds.BGM)
        {
            volume = Get<Slider>((int)Sliders.BGMSlider).value;
            GameManager.Data.BGMVolume = volume;

        }
        else
        {
            volume = Get<Slider>((int)Sliders.SFXSlider).value;
            GameManager.Data.SFXVolume = volume;

        }
        GameManager.Sound.SetVolume(Sound, volume);

    }

}
