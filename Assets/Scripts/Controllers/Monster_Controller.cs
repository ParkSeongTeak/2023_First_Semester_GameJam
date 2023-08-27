using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Monster_Controller : MonoBehaviour
{

    /// <summary> 얘가 살아있나? </summary>
    bool _live = true;
    /// <summary> 몬스터의 HP 살아있나? </summary>
    float _HP = 10;
    /// <summary> 몬스터의 기본 속도 </summary>
    public float DEFAULTSPEED { get { return 0.1f; } }
    /// <summary> 몬스터의 현재 속도 </summary>
    float _speed = 0.1f;

    /// <summary> x축 </summary>
    public float xpos { get; set; }
    /// <summary> y축 </summary>
    public float ypos { get; set; }
    [SerializeField]
    /// <summary> 다음에 가야 할 위치 </summary>
    Vector3[] direction = new Vector3[9];

    public float horizon = 1.5f;
    public float yzero = 0.1f;
    public float yEnd = 3.0f;
    /// <summary> 얘가 살아있나?  </summary>
    public bool Live { get { return _live; } set { _live = value; } }
    /// <summary> 속도 (슬로우 & 속도 정상화에 이용) </summary>
    public float Speed { get { return _speed; } set { _speed = value; } }
    /// <summary> 살아있는 시간 ( 공격 우선 순위에 영향) </summary>
    public float LifeTime { get; set; }
    /// <summary> 슬로우 장판 겹치는 횟수 </summary>
    public int stickyCount { get; set; }
    /// <summary> 속성 무효 장판 겹치는 횟수 </summary>
    public int nullCount { get; set; }
    [SerializeField]
    int _checkBox = 0;
    int checkBox { get { return _checkBox; } set { _checkBox = value; } }
    public float dist { get; set; }

    /// <summary>
    /// pooling 기능에 문제가 있어 버그잡는용
    /// </summary>
    int pooling { get; set; }

    /// <summary> 나의 태어날 때 속성(불,물,풀) </summary>
    Define.Property _bornproperty;
    [SerializeField]
    /// <summary> 나의 지금 속성 (불,물,풀 <-> 무속성 ) </summary>
    Define.Property _property = Define.Property.Fire;
    /// <summary> 나의 지금 속성 (불,물,풀 <-> 무속성 ) </summary>
    public Define.Property Property { get { return _property; } set { _property = value; } }
    /// <summary> 나의 태어날 때 속성(불,물,풀) </summary>
    public Define.Property BornProperty { get { return _bornproperty; }}

    /// <summary> 나의 현재 HP </summary>
    public float HP { get { return _HP;} set { _HP = value; } }
    
    /// <summary>
    /// 시작 혹은 pooling 이후의 초기화
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


public void beAttacked( Define.Property Property) //데미지, 투사체 속성
    {
        float DMG = GameManager.DMGTABLE[GameManager.Instance.LV[(int)Property]];
        

        //속성 검사
        if ((this._property == Define.Property.Fire && Property == Define.Property.Water) ||
            ((this._property == Define.Property.Water && Property == Define.Property.Grass)) ||
            (this._property == Define.Property.Grass && Property == Define.Property.Fire))//증폭
        {
            
            DMG *= 1.5f;
        }
        if ((this._property == Define.Property.Fire && Property == Define.Property.Grass) ||
            ((this._property == Define.Property.Water && Property == Define.Property.Fire)) ||
            (this._property == Define.Property.Grass && Property == Define.Property.Water))//반감
        {
            DMG *= 0.5f;
        }

        this.HP -= DMG;
        if (this.HP <= 0)//사망
        {
            _live = false;
            Dead();
        }
    }
    public void beAttacked(float DMG) //데미지, 투사체 속성
    {
        this.HP -= DMG;
        if (this.HP <= 0)//사망
        {
            _live = false; 
            Dead();

        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //투사체에 맞음
        if (collision.gameObject.tag == "Arrow")
        {
            beAttacked(collision.gameObject.GetComponent<Projectile_Controller>().ProjProp());

        }
        //이정표와 충돌
        if (collision.gameObject.tag == "Dir")
        {
            checkBox++;
        }
    }
    
    

    public void Dead()  //죽음
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
        //Find는 모든 몬스터가 생성될 때마다 사용하기에는 좀 무거움 + 바뀌는 OBJ가 아님으로 여기에서 새로 찾을 이유가 x
        //Manager로 옮기는것이 합리적
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
