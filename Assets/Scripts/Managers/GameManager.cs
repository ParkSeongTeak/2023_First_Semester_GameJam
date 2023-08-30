using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.SceneManagement;

using UnityEngine.UI;


public class GameManager : MonoBehaviour
{
    #region Managers
    static GameManager _instance;
    GameManager() { }

    InputManager _inputManager = new InputManager();
    SoundManager _soundManager = new SoundManager();
    ResourceManager _resourceManager = new ResourceManager();
    SkillManager _skillManager = new SkillManager();
    UIManager _uIManager = new UIManager();
    PoolingManager _poolingManager = new PoolingManager();
    SceneManager _sceneManager = new SceneManager();
    DataManager _dataManager = new DataManager();  

    public static GameManager Instance { get { init(); return _instance; } }
    public static InputManager Input { get { return Instance._inputManager; } }
    public static SoundManager Sound { get { return Instance._soundManager; } }
    public static ResourceManager Resource { get { return Instance._resourceManager; } }
    public static SkillManager Skill { get { return Instance._skillManager; } }
    public static UIManager UI { get { return Instance._uIManager; } }
    public static PoolingManager Pooling { get { return Instance._poolingManager; } }
    public static SceneManager Scene { get { return Instance._sceneManager; } }
    public static DataManager Data { get { return Instance._dataManager; } }
    #endregion Managers
    static void init()
    {

        if (_instance == null)
        {

            GameObject gm = GameObject.Find("GameManager");
            if(gm == null)
            {
                gm = new GameObject { name = "GameManager" };
                
                gm.AddComponent<GameManager>();
            }
            DontDestroyOnLoad(gm);
            _instance = gm.GetComponent<GameManager>();

            _instance._dataManager.Init();
            _instance._resourceManager.Init();
            _instance._soundManager.Init();
            _instance._inputManager.Init();
            

            
           
        }
    }

    // Start is called before the first frame update
    void Awake()
    {
        init();
    }

    // Update is called once per frame
    void Update()
    {
    }
    

    public static void GameOver()
    {
        
        Sound.Play("Effect/gameover");
        if (Data.MaxPoint < Data.NowPoint)
        {
            Data.MaxPoint = Data.NowPoint;
        }

        Pause();

    }
    public void Clear()
    {
        _instance._poolingManager.Clear();
    }


    public static void QuitApp()
    {
        Application.Quit();
    } 
    public static void Pause()
    {
        Time.timeScale = 0;
    }
    public static void UnPause()
    {
        Time.timeScale = 1;
    }

}
