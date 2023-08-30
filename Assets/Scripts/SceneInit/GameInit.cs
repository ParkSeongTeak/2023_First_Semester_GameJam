using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GameInit : MonoBehaviour
{

    private void Awake()
    {
        Application.targetFrameRate = 60;
    }
    // Start is called before the first frame update
    void Start()
    {
        GameManager.UI.ShowSceneUI<GameUI>().Init();
        GameManager.Data.StartGameScene();

        GameManager.Pooling.Init();

        StartCoroutine(MoneyGet());

        GameManager.Sound.Play("BGM/GAMEPLAY", Define.Sounds.BGM);
        
        GameObject.Find("StartPoint").GetComponent<StartPoint>().StartMonsterWave();



    }
    IEnumerator MoneyGet()
    {
        while (true)
        {
            yield return new WaitForSeconds(1.0f);
            GameManager.Data.Money += GameManager.Data.GETMONEY[GameManager.Data.LV[(int)Define.LV.Money]];
        }
    }
    // Update is called once per frame
    void Update()
    {
        
    }
}
