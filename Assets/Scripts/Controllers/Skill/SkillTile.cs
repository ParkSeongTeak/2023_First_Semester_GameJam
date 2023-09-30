using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTile : MonoBehaviour
{
    GameObject Skill;
    [SerializeField]
    Define.Skills Myskill;

    Monster_Controller _controllerIn;

    static string Mob = "Mob";

    static WaitForSeconds SKILLExplosionTime = new WaitForSeconds(0.4f);

    static WaitForSeconds[] SKILLEXISTTIME;

    static void SETSKILLEXISTTIME()
    {
        if (SKILLEXISTTIME == null)
        {
            SKILLEXISTTIME = new WaitForSeconds[5];
            for (int i = 0; i < 5; i++)
            {
                SKILLEXISTTIME[i] = new WaitForSeconds(GameManager.Data.SKILLEXISTTIME[i]);
            }

        }
    }

    private void Start()
    {
        switch (Myskill)
        {
            case Define.Skills.Explosion:
                GameManager.Sound.Play("Effect/meteor");
                break;
            case Define.Skills.Slow:
                GameManager.Sound.Play("Effect/slow");
                break;
            case Define.Skills.Neutralize:
                GameManager.Sound.Play("Effect/splash2");
                break;

        }
        
        this.transform.localScale = new Vector3(0.6482642f, 0.6180149f, 1f);

    }

    //스킬 범위내 몬스터 추가
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == Mob)//몬스터이고
        {
            _controllerIn = other.GetComponent<Monster_Controller>();
            switch (Myskill) {

                case Define.Skills.Explosion:
                    _controllerIn.beAttacked(300);
                    break;
                case Define.Skills.Slow:
                    if (_controllerIn.stickyCount == 0)//장판에 처음 들어옴
                    {
                        _controllerIn.Speed = _controllerIn.DEFAULTSPEED / 2;//속도 감소
                    }
                    _controllerIn.stickyCount++;
                    break;
                case Define.Skills.Neutralize:
                    if (_controllerIn.nullCount == 0)//장판에 처음 들어옴
                    {
                        _controllerIn.Property = Define.Properties.None;
                    }
                    _controllerIn.nullCount++;
                    break;
            }


        }
    }
    // 슬로우가 장판을 나가면 정상 속도로 변한다면 장판을 겹치게 이어놨을경우 첫 번째 장판을 나가자마자 정상 속도로 돌아올 것이다 두 번째 장판 효력 x
    // 이 문제를 업데이트 안 쓰고 처리할 방안을 못 찾음
   
    //스킬 범위내 몬스터 삭제
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == Mob)//몬스터이면
        {
            _controllerIn = other.GetComponent<Monster_Controller>();
            switch (Myskill)
            {

                case Define.Skills.Explosion:
                    break;
                case Define.Skills.Slow:
                    if (_controllerIn.stickyCount == 1)//마지막 장판을 나감
                    {
                        _controllerIn.Speed = _controllerIn.DEFAULTSPEED;//원래대로
                    }
                    _controllerIn.stickyCount--;
                    break;
                case Define.Skills.Neutralize:
                    
                    if (_controllerIn.nullCount == 1)//마지막 장판을 나감
                    {
                        _controllerIn.Property = _controllerIn.BornProperty;//원래대로
                    }
                    _controllerIn.nullCount--;
                    break;

                default:
                    break;
            }
        }
    }

    public void SkillExistTime()
    {
        StartCoroutine(ExistTime());
    }
    IEnumerator ExistTime()
    {

        Debug.Log("Enter");
        if(Myskill == Define.Skills.Explosion)
        {
            yield return SKILLExplosionTime;
        }
        else
        {
            if (SKILLEXISTTIME == null)
            {
                SETSKILLEXISTTIME();
                yield return SKILLEXISTTIME[GameManager.Data.LV[(int)Myskill + 5]];
            }
            else
            {
                yield return SKILLEXISTTIME[GameManager.Data.LV[(int)Myskill + 5]];

            }
        }

        Destroy(this.gameObject);
    }
}
