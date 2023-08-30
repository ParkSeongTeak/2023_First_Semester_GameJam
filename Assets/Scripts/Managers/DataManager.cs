using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class DataManager 
{
    #region Volume
    float _BGMVolume;
    public float BGMVolume { get { return _BGMVolume; } set { PlayerPrefs.SetFloat("BGMVolume", value); _BGMVolume = value; } }
    
    float _SFXVolume;
    public float SFXVolume { get { return _SFXVolume; } set { PlayerPrefs.SetFloat("SFXVolume", value); _SFXVolume = value; } }

    #endregion Volume

    #region Map
    public Vector3[] Direction { get; set; } = new Vector3[9];

    #endregion Map

    #region 공유데이터

    GameObject[] _Tower { get; set; }
    GameObject[] _Skill { get; set; }
    List<List<GameObject>> _map;// = new List<List<GameObject>>();

    int _startHP = 2;
    int _waveHPPlus = 4;

    int _nowPoint = 0;
    int _thiswavenum = 1;
    int _thiswaveRegen = 0;
    int _life = 5;
    int _money = 40;
    int _monsterHP = 10;
    int _wave = 1;
    int _shootSpeed = 0;
    int[] _lv;// 0,1,2: 불 물 풀  4: 돈  5, 

    Vector3[] _direction = new Vector3[9];

    //상수
    public float[] DMGTABLE { get; private set; } = new float[5] { 3, 8, 16, 42, 90 };
    public float[] SHOOTSPEED { get; private set; } = new float[5] { 2.0f, 1.20f, 0.5f, 0.25f, 0.08f };
    public int[] UPGRATECOST { get; private set; } = new int[5] { 20, 100, 680, 3000, 3000 };
    public int[] GETMONEY { get; private set; } = new int[5] { 2, 4, 8, 16, 32 };
    public float[] SKILLEXISTTIME { get; private set; } = new float[5] { 4, 7, 14, 20, 30 };

    public readonly string SCENENAME = "Game";
    public readonly string SCENENAMELHW = "LHW3";
    public readonly string maxSCORESTR = "MaxScoreasfln;e;wfnkawe;fnk";
    public int StartHP { get { return  _startHP; } set {  _startHP = value; } }
    public int WaveHPPlus { get { return  _waveHPPlus; } set {  _waveHPPlus = value; } }

    public string MAXSCORESTR { get { return maxSCORESTR; } }

    public GameObject[] Tower { get { return  _Tower; } }
    public GameObject[] Skills { get { return  _Skill; } }
    public int Money { get { return  _money; } set {  _money = value; GameManager.UI.ShowSceneUI<GameUI>()?.UpdateData(); } }
    public int Wave { get { return  _wave; } set {  _wave = value; GameManager.UI.ShowSceneUI<GameUI>()?.UpdateData(); } }
    public int[] LV { get { return  _lv; } set {  _lv = value; } }
    public int ShootSpeed { get { return  _shootSpeed; } set {  _shootSpeed = value; } }
    // warning: 이 값의 조정은 Monster_Controller에서 하는것을 원칙으로 한다.
    int _maxPoint = 0;
    public int MaxPoint { get { return  _maxPoint; } set {  _maxPoint = value; PlayerPrefs.SetInt(MAXSCORESTR, _maxPoint); } }
    // warn 값의 조정은 Monster_Controller에서 하는것을 원칙으로 한다.
    public int NowPoint { get { return  _nowPoint; } set {  _nowPoint = value; } }
    public int ThisWaveNum { get { return  _thiswavenum; } set {  _thiswavenum = value; } }
    public int ThisWaveRegen { get { return  _thiswaveRegen; } set {  _thiswaveRegen = value; } }
    // warn 값의 조정은 EndPoint에서 하는것을 원칙으로 한다.
    public int Life { get { return  _life; } set {  _life = value; if ( _life <= 0) {  GameManager.GameOver(); } GameManager.UI.ShowSceneUI<GameUI>()?.UpdateData(); } }
    public int MonsterHP { get { return  _monsterHP; } set {  _monsterHP = value; } }

    public List<List<GameObject>> Map = new List<List<GameObject>>();

    // int _point = 0; == 죽인 적의 수 몬스터의 Dead상황에 ++해 줘야할 data; 
    // public int _point{}; == 죽인 적의 수 몬스터의 Dead상황에 ++해 줘야할 data; 
    #endregion

    public void Init()
    {
        _BGMVolume = PlayerPrefs.GetFloat("BGMVolume", 1.0f);
        _SFXVolume = PlayerPrefs.GetFloat("SFXVolume", 1.0f);

        _Tower = new GameObject[3];
        _Skill = new GameObject[3];
        _Tower[(int)Define.Properties.Fire] =   GameManager.Resource.Load<GameObject>($"Prefabs/Tower/Tower{(int)Define.Properties.Fire}");
        _Tower[(int)Define.Properties.Water] =  GameManager.Resource.Load<GameObject>($"Prefabs/Tower/Tower{(int)Define.Properties.Water}");
        _Tower[(int)Define.Properties.Grass] =  GameManager.Resource.Load<GameObject>($"Prefabs/Tower/Tower{(int)Define.Properties.Grass}");
        _Skill[(int)Define.Skills.Explosion] =  GameManager.Resource.Load<GameObject>($"Prefabs/Skill/Skill{(int)Define.Skills.Explosion}");
        _Skill[(int)Define.Skills.Neutralize] = GameManager.Resource.Load<GameObject>($"Prefabs/Skill/Skill{(int)Define.Skills.Neutralize}");
        _Skill[(int)Define.Skills.Slow] =       GameManager.Resource.Load<GameObject>($"Prefabs/Skill/Skill{(int)Define.Skills.Slow}");

    }
    public void StartGameScene()
    {
        for (int i = 0; i < 9; i++)
        {
            GameManager.Data.Direction[i] = GameObject.Find($"dir{i + 1}").transform.position;

        }

        _money = 40;
        NowPoint = 0;
        Wave = 0;
        Life = 5;

        _lv = new int[(int)Define.LV.MaxCount] { 0, 0, 0, 0, 0, 0, 0, 0 };
        _maxPoint = PlayerPrefs.GetInt(MAXSCORESTR, 0);
        _map = new List<List<GameObject>>();
        for (int i = 0; i < 11; i++)
        {
            GameObject tmp = GameObject.Find($"Line ({i})");
            _map.Add(new List<GameObject>());
            for (int num = 0; num < tmp.transform.childCount; num++)
            {
                _map[i].Add(GameObject.Find($"Square ({num})"));
            }
        }

        for (int y = 0; y < 11; y++)
        {
            for (int x = 0; x < _map[0].Count; x++)
            {
                _map[y][x].GetComponent<Tile_Controller>().Y = y;
                _map[y][x].GetComponent<Tile_Controller>().X = x;

            }
        }

        // Explosion는 업그레이드 불가 ==  태생 lv5
        _lv[(int)Define.LV.Explosion] = 4;

    }

}
