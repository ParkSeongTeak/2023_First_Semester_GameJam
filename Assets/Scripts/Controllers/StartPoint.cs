using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartPoint : MonoBehaviour
{
    float MonsterToMonster = 1.0f;
    const float WaveToWave = 5.0f;

    bool _endWave = true;
    int thisWaveNum = 0;
    GameObject startPosition;
    string Effectwavestart = "Effect/wavestart";
    public static Vector3 StartPos { get; set; }
    private void Awake()
    {
        StartPos = transform.position; 
    }


    public void StartMonsterWave()
    {
        StartCoroutine(MonsterWave());
    }
    float MonsterToMonsterTime()
    {
        return 16.0f / (15 + GameManager.Data.Wave);
    }
    void MonsterRegen()
    {
        int MonsterIDX = Random.Range(0, 3);
        
        GameObject mob = GameManager.Resource.InstantiateMonster((Define.Properties)MonsterIDX);
    }
    WaitForSeconds waveToWave = new WaitForSeconds(WaveToWave);
    WaitForSeconds monsterToMonster = new WaitForSeconds(1.0f);

    IEnumerator MonsterWave()
    {
        while (true)
        {
            MonsterRegen();
            thisWaveNum += 1;

            if (thisWaveNum >= GameManager.Data.Wave * 2)
            {
                if (GameManager.Data.Wave % 10 == 0)
                {
                    GameManager.Data.MonsterHP *= 2;
                }
                thisWaveNum = 0;
                yield return waveToWave;
                
                GameManager.Sound.Play(Effectwavestart);

                GameManager.Data.Wave += 1;
                MonsterToMonster = MonsterToMonsterTime();
                monsterToMonster = new WaitForSeconds(MonsterToMonster);
            }
            else
            {
                yield return monsterToMonster;

            }
        }

    }
    






}
