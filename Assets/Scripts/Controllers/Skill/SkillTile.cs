using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SkillTile : MonoBehaviour
{
    GameObject Skill;
    [SerializeField]
    Define.Skills Myskill;

    //float[] _exitTime = new float[3] { 0.5f,5f,5f};

    public void InstanceSkill(Define.Skills skill)// 위 함수는 Tile_Controller에 가는게 맞습니다
    {

        Skill = Instantiate(GameManager.Instance.Skills[(int)skill]);
        Skill.transform.parent = transform;

        Skill.transform.localPosition = new Vector3(0, 0, 0);
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
        if (other.gameObject.tag == "Mob")//몬스터이고
        {
            /*
            float range = Vector3.Magnitude(this.gameObject.transform.position - other.gameObject.transform.position);
            if (range <= GameManager.SkillRange)//3*3범위 내인가?
            {
                gameObject.GetComponent<ResourceManager>().InAreaMonster_List.Add(other.gameObject);
            }
            */

            // 그냥 Skill타일 Collider의 크기를 3*3 크기로 함

            switch (Myskill) {

                case Define.Skills.Explosion:
                    other.GetComponent<Monster_Controller>().beAttacked(300);
                    break;
                case Define.Skills.Slow:
                    if(other.GetComponent<Monster_Controller>().stickyCount == 0)//장판에 처음 들어옴
                    {
                        other.GetComponent<Monster_Controller>().Speed = other.GetComponent<Monster_Controller>().DEFAULTSPEED / 2;//속도 감소
                    }
                    other.GetComponent<Monster_Controller>().stickyCount++;
                    break;
                case Define.Skills.Neutralize:
                    if (other.GetComponent<Monster_Controller>().nullCount == 0)//장판에 처음 들어옴
                    {
                        other.GetComponent<Monster_Controller>().Property = Define.Properties.None;
                    }
                    other.GetComponent<Monster_Controller>().nullCount++;
                    break;
            }


        }
    }
    // 슬로우가 장판을 나가면 정상 속도로 변한다면 장판을 겹치게 이어놨을경우 첫 번째 장판을 나가자마자 정상 속도로 돌아올 것이다 두 번째 장판 효력 x
    // 이 문제를 업데이트 안 쓰고 처리할 방안을 못 찾음
   
    //스킬 범위내 몬스터 삭제
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Mob")//몬스터이면
        {
            //gameObject.GetComponent<ResourceManager>().InAreaMonster_List.Remove(other.gameObject);

            switch (Myskill)
            {

                case Define.Skills.Explosion:
                    //other.GetComponent<Monster_Controller>().beAttacked(300);
                    break;
                case Define.Skills.Slow:
                    if (other.GetComponent<Monster_Controller>().stickyCount == 1)//마지막 장판을 나감
                    {
                        other.GetComponent<Monster_Controller>().Speed = other.GetComponent<Monster_Controller>().DEFAULTSPEED;//원래대로
                    }
                    other.GetComponent<Monster_Controller>().stickyCount--;
                    break;
                case Define.Skills.Neutralize:
                    if (other.GetComponent<Monster_Controller>().nullCount == 1)//마지막 장판을 나감
                    {
                        other.GetComponent<Monster_Controller>().Property = other.GetComponent<Monster_Controller>().BornProperty;//원래대로
                    }
                    other.GetComponent<Monster_Controller>().nullCount--;
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
        Debug.Log($"LV[{(int)Myskill + 5}] : {GameManager.Instance.LV[(int)Myskill + 5]} time: {GameManager.SKILLEXISTTIME[GameManager.Instance.LV[(int)Myskill + 5]]}");
        yield return new WaitForSeconds(Myskill == Define.Skills.Explosion ? 0.4f : GameManager.SKILLEXISTTIME[GameManager.Instance.LV[(int)Myskill + 5]]);

        Destroy(this.gameObject);
    }
}
