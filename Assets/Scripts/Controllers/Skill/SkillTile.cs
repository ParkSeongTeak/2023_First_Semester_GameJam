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

    //��ų ������ ���� �߰�
    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.tag == Mob)//�����̰�
        {
            _controllerIn = other.GetComponent<Monster_Controller>();
            switch (Myskill) {

                case Define.Skills.Explosion:
                    _controllerIn.beAttacked(300);
                    break;
                case Define.Skills.Slow:
                    if (_controllerIn.stickyCount == 0)//���ǿ� ó�� ����
                    {
                        _controllerIn.Speed = _controllerIn.DEFAULTSPEED / 2;//�ӵ� ����
                    }
                    _controllerIn.stickyCount++;
                    break;
                case Define.Skills.Neutralize:
                    if (_controllerIn.nullCount == 0)//���ǿ� ó�� ����
                    {
                        _controllerIn.Property = Define.Properties.None;
                    }
                    _controllerIn.nullCount++;
                    break;
            }


        }
    }
    // ���ο찡 ������ ������ ���� �ӵ��� ���Ѵٸ� ������ ��ġ�� �̾������� ù ��° ������ �����ڸ��� ���� �ӵ��� ���ƿ� ���̴� �� ��° ���� ȿ�� x
    // �� ������ ������Ʈ �� ���� ó���� ����� �� ã��
   
    //��ų ������ ���� ����
    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == Mob)//�����̸�
        {
            _controllerIn = other.GetComponent<Monster_Controller>();
            switch (Myskill)
            {

                case Define.Skills.Explosion:
                    break;
                case Define.Skills.Slow:
                    if (_controllerIn.stickyCount == 1)//������ ������ ����
                    {
                        _controllerIn.Speed = _controllerIn.DEFAULTSPEED;//�������
                    }
                    _controllerIn.stickyCount--;
                    break;
                case Define.Skills.Neutralize:
                    
                    if (_controllerIn.nullCount == 1)//������ ������ ����
                    {
                        _controllerIn.Property = _controllerIn.BornProperty;//�������
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
