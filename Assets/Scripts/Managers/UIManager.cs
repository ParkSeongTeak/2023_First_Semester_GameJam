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

    GameObject[] _uibuttons;
    TextMeshProUGUI[] _textFields;
    GameObject[] _elseFields;



    Sprite[] _LVImage;
    //public GameObject MainScene { get { return _mainScene; } set { _mainScene = value; } }
    public Sprite[] LVImage { get { return _LVImage; } }
    public GameObject[] Uibuttons { get { return _uibuttons; } }

    public TextMeshProUGUI[] _TextFields { get { return _textFields; } }
    public GameObject[] ButtonsLV { get {  return _buttonsLV; } }
    public GameObject[] ElseFields { get { return _elseFields; } }
    public GameObject UI { get { return _UI; } }
    public GameObject UIField { get { return _UIField; } }

    int idx = 0;




    public void init()
    {
        
        _uibuttons = new GameObject[(int)Define.ButtonsEnum.MaxCount];
        string[] _uibuttonsstr = Enum.GetNames(typeof(Define.ButtonsEnum));
        for (int i = 0; i < (int)Define.ButtonsEnum.MaxCount; i++)
        {
            _uibuttons[i] = GameObject.Find(_uibuttonsstr[i]);

            if(_uibuttons[i] == null)
            {
                //Debug.Log("_uibuttons[i]" + i);
            }
        }

        Plus_Button_False();
        _textFields = new TextMeshProUGUI[(int)Define.TextEnum.MaxCount];
        string[] _textButtonsstr = Enum.GetNames(typeof(Define.TextEnum));
        for (int i = 0; i < (int)Define.TextEnum.MaxCount; i++)
        {
            _textFields[i] = GameObject.Find(_textButtonsstr[i]).GetComponent<TextMeshProUGUI>();
        }
        _elseFields = new GameObject[(int)Define.Else.MaxCount];
        string[] _elseFieldsstr = Enum.GetNames(typeof(Define.Else));
        for (int i = 0; i < (int)Define.Else.MaxCount; i++)
        {
            _elseFields[i] = GameObject.Find(_elseFieldsstr[i]);
        }


        //GameOverScene = GameObject.Find("GameOver").gameObject;

        _mainScene = GameObject.Find("MainPage").gameObject;
        

        FullLife = Resources.Load<Sprite>("Sprite/UI/3_체력O");
        emptyLife = Resources.Load<Sprite>("Sprite/UI/3_체력X");

        _LVImage = new Sprite[5];
        for (int i = 0; i < 5; i++)
        {
            _LVImage[i] = Resources.Load<Sprite>($"Sprite/UI/9_LV{i + 1}");

        }
       

        _buttonsLV = new GameObject[(int)Define.ButtonsEnum.MaxCount];

        _buttonsLV[(int)Define.ButtonsEnum.ShootSpeed] = GameObject.Find("AttackSpeadLV");
        _buttonsLV[(int)Define.ButtonsEnum.MoneyGet] = GameObject.Find("UpgradeMoneyLV");
        

        for (int i = 0; i < 5; i++)
        {
            lifeArray[i] = GameObject.Find($"Life{i+1}").gameObject;
        }
        //GameOverScene.SetActive(false);
        ElseFields[(int)Define.Else.GameOver].SetActive(false);

        GameManager.UI.PointUpdate();

        _UI = GameObject.Find("UI");
        _UI.GetComponent<UI>().init();
        _UI.SetActive(false);
        _UIField = GameObject.Find("UIField").gameObject;


    }
    public void PointUpdate()
    {
        _textFields[(int)TextEnum.WavePoint].text = $"{GameManager.Instance.Wave} WAVE";
        _textFields[(int)TextEnum.NowPoint].text = $"SCORE: {GameManager.Instance.NowPoint}";
        _textFields[(int)TextEnum.MaxPoint].text = $"BEST SCORE: {GameManager.Instance.MaxPoint}";
        _textFields[(int)TextEnum.MoneyPoint].text = $"{(int)GameManager.Instance.Money}";
        HPBar();

    }
    void HPBar()
    {
        for (int i = 0; i < GameManager.Instance.Life; i++)
        {
            lifeArray[i].GetComponent<Image>().sprite = FullLife;
        }
        for (int i = GameManager.Instance.Life; i < FULLLIFE; i++)
        {
            lifeArray[i].GetComponent<Image>().sprite = emptyLife;
        }

    }

    public void GameOverUI()
    {
        ElseFields[(int)Define.Else.GameOver].SetActive(true);
        //GameOverScene.SetActive(true);

        _textFields[(int)TextEnum.WaveEnd].text = $"{GameManager.Instance.Wave} WAVE";
        _textFields[(int)TextEnum.NowScoreEnd].text = $"SCORE: {GameManager.Instance.NowPoint}";
        _textFields[(int)TextEnum.BestScoreEnd].text = $"BEST SCORE: {GameManager.Instance.MaxPoint}";

    }

    public void ReLoadScene()
    {
        SceneManager.LoadScene(GameManager.SCENENAME);
        UnityEngine.Object.Destroy(GameManager.Instance.gameObject);
        
    }

    public void GameStart()
    {
        GameManager.Instance.UnPauseTime();
        GameManager.Sound.Play("BGM/GAMEPLAY", Define.Sound.BGM);
        _mainScene.SetActive(false);

    }
    public void MainPageOff()
    {
        _mainScene.SetActive(false);

    }
    public void Plus_Button_False()
    {
        _uibuttons[(int)ButtonsEnum.FireT_Plus].SetActive(false);
        _uibuttons[(int)ButtonsEnum.WaterT_Plus].SetActive(false);
        _uibuttons[(int)ButtonsEnum.GrassT_Plus].SetActive(false);

        _uibuttons[(int)ButtonsEnum.UpgradeMoney_Plus].SetActive(false);
        
        _uibuttons[(int)ButtonsEnum.SkillExplosion_Plus].SetActive(false);
        _uibuttons[(int)ButtonsEnum.SkillNone_Plus].SetActive(false);
        _uibuttons[(int)ButtonsEnum.SkillSlow_Plus].SetActive(false);

        _uibuttons[(int)ButtonsEnum.AttackSpead_Plus].SetActive(false);





    }

}

