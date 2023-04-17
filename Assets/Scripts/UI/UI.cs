using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class UI : MonoBehaviour
{
    GameObject ui;
    float BGMVol = 0.5f;
    float EffectVol = 0.5f ;
    GameObject MainScene;
    Slider BGMSlider;
    Slider EffectSlider;

   
    public void init()
    {

        ui = GameManager.UI.UI;
        BGMSlider = GameObject.Find("BGMSlider").GetComponent<Slider>();
        EffectSlider = GameObject.Find("EffectSlider").GetComponent<Slider>();
        MainScene = GameManager.UI.ElseFields[(int)Define.Else.MainPage];

        BGMVol = PlayerPrefs.GetFloat("BGMVol", 0.5f);
        EffectVol = PlayerPrefs.GetFloat("EffectVol", 0.5f);

        BGMSlider.value = BGMVol;
        EffectSlider.value = EffectVol;
    }


    public void AndroidGameEnd()
    {
        GameManager.Instance.DataSave();
        Application.Quit();
    }


    public void ActiveUI()
    {
        ui.SetActive(true);
    }

    public void InActiveUI()
    {
        ui.SetActive(false);
    }
    public void PauseTime()
    {
        GameManager.Instance.PauseTime();
    }
    public void UnPauseTime()
    {
        if(GameManager.UI.ElseFields[(int)Define.Else.MainPage].activeSelf == false && GameManager.UI.ElseFields[(int)Define.Else.GameOver].activeSelf == false)
        {
            GameManager.Instance.UnPauseTime();
        }

    }
    public void GameStart()
    {
        GameManager.UI.MainPageOff();
        GameManager.Sound.Play("BGM/GAMEPLAY", Define.Sound.BGM);
        UnPauseTime();
        GameManager.UI.PointUpdate();
    }
    public void ReStartGame()
    {
        GameManager.Instance.ReLoadScene();

        GameStart();
    }
    public void ToMain()
    {
        GameManager.Instance.ReLoadScene();

    }
    private void Update()
    {
        if (ui.activeSelf &&  ((BGMVol != BGMSlider.value )||(EffectVol != EffectSlider.value))) {

            BGMVol = BGMSlider.value;
            EffectVol = EffectSlider.value;
            GameManager.Sound.SetAudioSourceVolume(BGMSlider.value, Define.Sound.BGM);
            GameManager.Sound.SetAudioSourceVolume(EffectSlider.value, Define.Sound.Effect);

            PlayerPrefs.SetFloat("BGMVol", BGMVol);
            PlayerPrefs.SetFloat("EffectVol", EffectVol);

        }



    }

    public void TowerShootSpeedUpgrade()
    {
        if (GameManager.Instance.LV[(int)Define.LV.ShootSpeed] < 4 && (GameManager.Instance.Money >= GameManager.UPGRATECOST[GameManager.Instance.LV[(int)Define.LV.ShootSpeed]]))
        {
            GameManager.Sound.Play("Effect/button2");

            GameManager.Instance.Money -= GameManager.UPGRATECOST[GameManager.Instance.LV[(int)Define.LV.ShootSpeed]];
            GameManager.Instance.LV[(int)Define.LV.ShootSpeed] += 1;
            GameManager.UI.PointUpdate();

            GameManager.UI.ButtonsLV[(int)Define.ButtonsEnum.ShootSpeed].GetComponent<Image>().sprite = GameManager.UI.LVImage[GameManager.Instance.LV[(int)Define.LV.ShootSpeed]];
            GameManager.UI.Uibuttons[(int)Define.ButtonsEnum.ShootSpeed].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{GameManager.UPGRATECOST[GameManager.Instance.LV[(int)Define.LV.ShootSpeed]]}";

            GameManager.UI.Uibuttons[(int)Define.ButtonsEnum.ShootSpeed].SetActive(false);
        }
    }

    public void MoneyGetUpgrade()
    {
        if (GameManager.Instance.LV[(int)Define.LV.MoneyGet] < 4 && (GameManager.Instance.Money >= GameManager.UPGRATECOST[GameManager.Instance.LV[(int)Define.LV.MoneyGet]]))
        {
            GameManager.Sound.Play("Effect/button2");
            GameManager.Instance.Money -= GameManager.UPGRATECOST[GameManager.Instance.LV[(int)Define.LV.MoneyGet]];
            GameManager.Instance.LV[(int)Define.LV.MoneyGet] += 1;
            GameManager.UI.PointUpdate();

            GameManager.UI.ButtonsLV[(int)Define.ButtonsEnum.MoneyGet].GetComponent<Image>().sprite = GameManager.UI.LVImage[GameManager.Instance.LV[(int)Define.LV.MoneyGet]];
            GameManager.UI.Uibuttons[(int)Define.ButtonsEnum.MoneyGet].transform.GetChild(0).GetComponent<TextMeshProUGUI>().text = $"{GameManager.UPGRATECOST[GameManager.Instance.LV[(int)Define.LV.MoneyGet]]}";
            GameManager.UI.Uibuttons[(int)Define.ButtonsEnum.MoneyGet].SetActive(false);

        }
    }


}
