using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    float MonsterToMonster = 1.0f;
    float WaveToWave = 5.0f;

    bool _endWave = true;
    int thisWaveNum = 0;
    GameObject startPosition;

    public static Vector3 StartPos { get; set; }

    private void Start()
    {
        startPosition = GameObject.Find("ReMonster");
        StartPos = transform.position;
        StartCoroutine(MonsterWave());
        //GameManager.Input.KeyAction += MonsterRegen;
    }
    float MonsterToMonsterTime()
    {
        return 16.0f / (15 + GameManager.Instance.Wave);
    }
    void MonsterRegen()
    {
        int MonsterIDX = Random.Range(0, 3);
        
        GameObject mob = GameManager.Resource.InstantiateMonster((Define.Properties)MonsterIDX);
        //mob.GetComponent<Monster_Controller>().checkBox = 0;
        mob.transform.position = StartPos;
    }

    IEnumerator MonsterWave()
    {
        while (true)
        {
            MonsterRegen();
            thisWaveNum += 1;

            if (thisWaveNum >= GameManager.Instance.Wave * 2)
            {
                if (GameManager.Instance.Wave % 10 == 0)
                {
                    GameManager.Instance.MonsterHP *= 2;
                }
                thisWaveNum = 0;
                yield return new WaitForSeconds(WaveToWave);
                
                GameManager.Sound.Play("Effect/wavestart");

                GameManager.Instance.Wave += 1;
                MonsterToMonster = MonsterToMonsterTime();
            }
            else
            {
                yield return new WaitForSeconds(MonsterToMonster);

            }
        }

    }
    






}
