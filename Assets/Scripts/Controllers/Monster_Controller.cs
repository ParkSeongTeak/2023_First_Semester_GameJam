using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Controller : MonoBehaviour
{

    /// <summary> �갡 ����ֳ�? </summary>
    bool _live = true;
    /// <summary> ������ HP ����ֳ�? </summary>
    float _HP = 10;
    /// <summary> ������ �⺻ �ӵ� </summary>
    public float DEFAULTSPEED { get { return 0.1f; } }
    /// <summary> ������ ���� �ӵ� </summary>
    float _speed = 0.1f;

    /// <summary> x�� </summary>
    public float xpos { get; set; }
    /// <summary> y�� </summary>
    public float ypos { get; set; }
    [SerializeField]
    /// <summary> ������ ���� �� ��ġ </summary>
    Vector3[] direction = new Vector3[9];

    public float horizon = 1.5f;
    public float yzero = 0.1f;
    public float yEnd = 3.0f;
    /// <summary> �갡 ����ֳ�?  </summary>
    public bool Live { get { return _live; } set { _live = value; } }
    /// <summary> �ӵ� (���ο� & �ӵ� ����ȭ�� �̿�) </summary>
    public float Speed { get { return _speed; } set { _speed = value; } }
    /// <summary> ����ִ� �ð� ( ���� �켱 ������ ����) </summary>
    public float LifeTime { get; set; }
    /// <summary> ���ο� ���� ��ġ�� Ƚ�� </summary>
    public int stickyCount { get; set; }
    /// <summary> �Ӽ� ��ȿ ���� ��ġ�� Ƚ�� </summary>
    public int nullCount { get; set; }
    [SerializeField]
    int _checkBox = 0;
    int checkBox { get { return _checkBox; } set { _checkBox = value; } }
    public float dist { get; set; }

    /// <summary>
    /// pooling ��ɿ� ������ �־� ������¿�
    /// </summary>
    int pooling { get; set; }

    /// <summary> ���� �¾ �� �Ӽ�(��,��,Ǯ) </summary>
    Define.Property _bornproperty;
    [SerializeField]
    /// <summary> ���� ���� �Ӽ� (��,��,Ǯ <-> ���Ӽ� ) </summary>
    Define.Property _property = Define.Property.Fire;
    /// <summary> ���� ���� �Ӽ� (��,��,Ǯ <-> ���Ӽ� ) </summary>
    public Define.Property Property { get { return _property; } set { _property = value; } }
    /// <summary> ���� �¾ �� �Ӽ�(��,��,Ǯ) </summary>
    public Define.Property BornProperty { get { return _bornproperty; }}

    /// <summary> ���� ���� HP </summary>
    public float HP { get { return _HP;} set { _HP = value; } }
    
    /// <summary>
    /// ���� Ȥ�� pooling ������ �ʱ�ȭ
    /// </summary>
    void OnEnable()
    {
        _live = true;
        _HP = 10;
        _speed = DEFAULTSPEED;
        horizon = 1.5f;
        yzero = 0.1f;
        yEnd = 3.0f;
        LifeTime = 0;
        stickyCount = 0;
        nullCount = 0;
        _checkBox = 0;
        dist = 0;
        transform.position = GameManager.Instance.StartPoint.transform.position;
        _bornproperty = _property;
        checkBox = 0;
        HP = GameManager.Instance.StartHP + (GameManager.Instance.Wave - 1) * (GameManager.Instance.Wave - 1) * (GameManager.Instance.WaveHPPlus);


    }


public void beAttacked( Define.Property Property) //������, ����ü �Ӽ�
    {
        float DMG = GameManager.DMGTABLE[GameManager.Instance.LV[(int)Property]];
        

        //�Ӽ� �˻�
        if ((this._property == Define.Property.Fire && Property == Define.Property.Water) ||
            ((this._property == Define.Property.Water && Property == Define.Property.Grass)) ||
            (this._property == Define.Property.Grass && Property == Define.Property.Fire))//����
        {
            
            DMG *= 1.5f;
        }
        if ((this._property == Define.Property.Fire && Property == Define.Property.Grass) ||
            ((this._property == Define.Property.Water && Property == Define.Property.Fire)) ||
            (this._property == Define.Property.Grass && Property == Define.Property.Water))//�ݰ�
        {
            DMG *= 0.5f;
        }

        this.HP -= DMG;
        if (this.HP <= 0)//���
        {
            _live = false;
            Dead();
        }
    }
    public void beAttacked(float DMG) //������, ����ü �Ӽ�
    {
        this.HP -= DMG;
        if (this.HP <= 0)//���
        {
            _live = false; 
            Dead();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //����ü�� ����
        if (collision.gameObject.tag == "Arrow")
        {
            beAttacked(collision.gameObject.GetComponent<Projectile_Controller>().ProjProp());

        }
        //����ǥ�� �浹
        if (collision.gameObject.tag == "Dir")
        {
            checkBox++;
        }
    }
    
    

    public void Dead()  //����
    {
        if (!_live)
        {
            switch (_bornproperty) 
            {
                case Define.Property.Fire:
                    GameManager.Sound.Play("Effect/firedie");
                    break;
                case Define.Property.Water:
                    GameManager.Sound.Play("Effect/waterdie");
                    break;
                case Define.Property.Grass:
                    GameManager.Sound.Play("Effect/plantdie");
                    break;

            }
            GameManager.Instance.Money += GameManager.Instance.Wave * 2;
            GameManager.Instance.NowPoint += 1;
        }
        transform.position = GameManager.Instance.StartPoint.transform.position;
        _bornproperty = _property;
        LifeTime = 0;
        checkBox = 0;
        _speed = 0;
        stickyCount = 0;
        nullCount = 0;
        GameManager.Resource.DestroyMonster(_property, this.gameObject);
    }

    // Start is called before the first frame update
    void Awake()
    {
        //Find�� ��� ���Ͱ� ������ ������ ����ϱ⿡�� �� ���ſ� + �ٲ�� OBJ�� �ƴ����� ���⿡�� ���� ã�� ������ x
        //Manager�� �ű�°��� �ո���
        pooling = 0;
        direction = GameManager.Instance.Direction;

    }

    
    void ThisMove()
    {
        xpos = transform.position.x; 
        ypos = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, direction[checkBox], this._speed);
        
        this.dist += this._speed;

    }

    
    private void FixedUpdate()
    {
        LifeTime += Time.deltaTime;
        ThisMove();
    }



}
