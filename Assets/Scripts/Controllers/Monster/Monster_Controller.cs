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
    public float DEFAULTSPEED { get { return 3.5f; } }
    
    /// <summary> 몬스터의 현재 속도 </summary>
    float _speed { get; set; } = 3.5f;

    /// <summary> x축 </summary>
    public float xpos { get; set; }
    /// <summary> y축 </summary>
    public float ypos { get; set; }
    /// <summary> 다음에 가야 할 위치 </summary>
    static Vector3[] direction;

    public float horizon { get; set; } = 1.5f;
    public float yzero { get; set; } = 0.1f;
    public float yEnd { get; set; } = 3.0f;
    /// <summary> 얘가 살아있나?  </summary>
    public bool Live { get { return _live; } set { _live = value; } }
    /// <summary> 속도 (슬로우 & 속도 정상화에 이용) </summary>
    public float Speed { get { return _speed; } set { _speed = value; } }
    /// <summary> 슬로우 장판 겹치는 횟수 </summary>
    public int stickyCount { get; set; }
    /// <summary> 속성 무효 장판 겹치는 횟수 </summary>
    public int nullCount { get; set; }
    int _checkBox = 0;

    public int checkBox { get { return _checkBox; } set { _checkBox = value; } }
    public float dist { get; set; }

    /// <summary>
    /// pooling 기능에 문제가 있어 버그잡는용
    /// </summary>
    int pooling { get; set; }

    /// <summary> 나의 태어날 때 속성(불,물,풀) </summary>
    Define.Properties _bornproperty;
    [SerializeField]
    /// <summary> 나의 지금 속성 (불,물,풀 <-> 무속성 ) </summary>
    Define.Properties _property = Define.Properties.Fire;
    /// <summary> 나의 지금 속성 (불,물,풀 <-> 무속성 ) </summary>
    public Define.Properties Property { get { return _property; } set { _property = value; } }
    /// <summary> 나의 태어날 때 속성(불,물,풀) </summary>
    public Define.Properties BornProperty { get { return _bornproperty; }}

    /// <summary> 나의 현재 HP </summary>
    public float HP { get { return _HP;} set { _HP = value; } }
    public LinkedList<GameObject> inRangeTower = new LinkedList<GameObject>();
    

    void RemoveAll()
    {
        LinkedListNode<GameObject> node = inRangeTower.First; // 리스트의 처음부터 시작

        while (node != null)
        {
            if(node.Value != null)
            {
                node.Value.GetComponent<T_Controller>()?.RemoveMonster(gameObject);
            }
            
            node = node.Next; // 다음 노드로 이동
        }
    }

    /// <summary>
    /// 시작 혹은 pooling 이후의 초기화
    /// </summary>
    void OnEnable()
    {
        inRangeTower.Clear();
        _live = true;
        _HP = 10;
        _speed = DEFAULTSPEED;
        horizon = 1.5f;
        yzero = 0.1f;
        yEnd = 3.0f;
        stickyCount = 0;
        nullCount = 0;
        _checkBox = 0;
        dist = 0;
        transform.position = StartPoint.StartPos;
        
        checkBox = 0;
        HP = GameManager.Data.StartHP + (GameManager.Data.Wave - 1) * (GameManager.Data.Wave - 1) * (GameManager.Data.WaveHPPlus)*(1.0f + GameManager.Data.Wave/15.0f) * (1.0f + GameManager.Data.Wave / 15.0f);


    }


    public void beAttacked( Define.Properties Property) //데미지, 투사체 속성
    {
        float DMG = GameManager.Data.DMGTABLE[GameManager.Data.LV[(int)Property]];
        

        //속성 검사
        if ((this._property == Define.Properties.Fire && Property == Define.Properties.Water) ||
            ((this._property == Define.Properties.Water && Property == Define.Properties.Grass)) ||
            (this._property == Define.Properties.Grass && Property == Define.Properties.Fire))//증폭
        {
            
            DMG *= 1.5f;
        }
        if ((this._property == Define.Properties.Fire && Property == Define.Properties.Grass) ||
            ((this._property == Define.Properties.Water && Property == Define.Properties.Fire)) ||
            (this._property == Define.Properties.Grass && Property == Define.Properties.Water))//반감
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
                case Define.Properties.Fire:
                    GameManager.Sound.Play("Effect/firedie");
                    break;
                case Define.Properties.Water:
                    GameManager.Sound.Play("Effect/waterdie");
                    break;
                case Define.Properties.Grass:
                    GameManager.Sound.Play("Effect/plantdie");
                    break;

            }
            GameManager.Data.Money += GameManager.Data.Wave * 2;
            GameManager.Data.NowPoint += 1;
        }
        RemoveAll();
        transform.position = StartPoint.StartPos;
        checkBox = 0;
        _speed = 0;
        stickyCount = 0;
        nullCount = 0;
        GameManager.Resource.DestroyMonster(_bornproperty, this.gameObject);
    }

    // Start is called before the first frame update
    void Awake()
    {
        pooling = 0;
        if(direction == null)
        {
            direction = GameManager.Data.Direction;

        }
        _bornproperty = _property;
        Transform MonsterPool;
        string name = $"Monster{Enum.GetName(typeof(Define.Properties), _property)}";
        GameManager.Pooling.PoolingRoot.TryGetValue(name, out MonsterPool);
        transform.parent = MonsterPool;
    }

    
    private void Update()
    {
        xpos = transform.position.x;
        ypos = transform.position.y;
        transform.position = Vector3.MoveTowards(transform.position, direction[checkBox], _speed * Time.deltaTime);

        this.dist += this._speed;
    }



}
