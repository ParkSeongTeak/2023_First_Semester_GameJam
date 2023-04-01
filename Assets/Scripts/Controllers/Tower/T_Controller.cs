using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Controller : MonoBehaviour
{
    public List<Monster_Controller> InAreaMonster;
    float FullDist_x, FullDist_y,FullDist;
    float xpos, ypos;
    float mobDist;
    float arrowSpeed = 1f;
    float closeDist = 100;
    GameObject Projectile;
    GameObject _deleteButton;
    GameObject myTarget;
    Vector3 target;
    public List<GameObject> inRangeMonster; 

    bool isDelay;
    bool arrowFlag;

    [SerializeField]
    Define.Property property = Define.Property.Fire;

    Vector3[] _direction = new Vector3[9];
    // Start is called before the first frame update
    void Start()
    {
        _direction = GameManager.Instance.Direction;
        _deleteButton = transform.Find("Delete").gameObject;
        InAreaMonster = new List<Monster_Controller>();
        Projectile = Resources.Load<GameObject>($"Prefabs/Projectile/Projectile{(int)property}");
        myTarget = GameObject.FindWithTag("Arrow");
        isDelay = false;
        arrowFlag = false;
        StartCoroutine(ContinueShoot());

        //현재 타워 좌표
        xpos = this.transform.position.x;
        ypos = this.transform.position.y;
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //공격할 몬스터 넣기
        if (other.gameObject.tag == "Mob")
        {
            inRangeMonster.Add(other.gameObject);


            //GameObject tempMob = other.gameObject;
            //Monster_Controller M = other.gameObject.GetComponent<Monster_Controller>();
            //InAreaMonster.Add(M);
            //target = M.gameObject.transform.position;
            //myTarget.GetComponent<Projectile_Controller>().setTarget((Vector3)tempMob.transform.position);
            
        }
    }

    private void OnTriggerExit2D(Collider2D other)
    {
        if (other.gameObject.tag == "Mob")
        {
            if (inRangeMonster.Count > 0)
            {
                inRangeMonster.Remove(other.gameObject);
            }
           
        }
        
    }

    public Vector3 NearestMonster()
    {
        //전체거리
        //FullDist_x = Mathf.Abs(GameObject.Find("dir2").transform.position.x - GameObject.Find("dir3").transform.position.x) * 4;
        //FullDist_y = Mathf.Abs(GameObject.Find("dir1").transform.position.y - GameObject.Find("dir2").transform.position.y) * 4;
        //FullDist = FullDist_x + FullDist_y + Mathf.Abs(GameObject.Find("StartPoint").transform.position.x - GameObject.Find("EndPoint").transform.position.x);

        FullDist_x = Mathf.Abs(_direction[1].x - _direction[2].x) * 4;
        FullDist_y = Mathf.Abs(_direction[0].y - _direction[1].y) * 4;

        FullDist = FullDist_x + FullDist_y + Mathf.Abs(GameObject.Find("StartPoint").transform.position.x - GameObject.Find("EndPoint").transform.position.x);

        //int idx = 0;

        if (InAreaMonster.Count == 0)
        {
            target = this.transform.position;
            return target;
        }
        /*
        for (int i = 0; i < InAreaMonster.Count; i++)
        {
            mobDist = InAreaMonster[i].dist;//몬스터 이동 거리
            if (FullDist - mobDist < closeDist)//더 작은 거리
            {
                idx = i;
            }
        }
        */
      
        //target = new Vector3(InAreaMonster[0].xpos, InAreaMonster[0].ypos, 0);//맨앞
        return target;
    }
    void Attack()
    {

    }
   
    IEnumerator ContinueShoot()
    {
        while (true)
        {
            if (isDelay == false && inRangeMonster.Count > 0)
            {
          
                isDelay = true;
                if (Time.timeScale == 1)
                {
                    GameObject go = Instantiate(Projectile);//화살 생성(매개변수로 프리팹 전달),GameObject로 강제 형 변환
                    go.transform.position = this.transform.position;
                    Vector3 point = NearestMonster();
                    go.GetComponent<Projectile_Controller>().Shoot(point, this.transform.position);
                    GameManager.Sound.Play("Effect/gun2");
                    go.GetComponent<Projectile_Controller>().setTarget(FindTarget(inRangeMonster).transform.position);
                    Destroy(go, 2f);
                }
                yield return new WaitForSecondsRealtime(GameManager.SHOOTSPEED[GameManager.Instance.LV[(int)Define.LV.ShootSpeed]]);
                isDelay = false;
            }
            else
            {
                yield return null;
            }
        }
    } 
   

    GameObject FindTarget(List<GameObject> mons)
    {
        float maxTime = mons[0].GetComponent<Monster_Controller>().LifeTime;
            int nowIdx = 0;
        for(int i = 1; i < mons.Count; i++)
        {
            if(maxTime< mons[i].GetComponent<Monster_Controller>().LifeTime)
            {
                maxTime = mons[i].GetComponent<Monster_Controller>().LifeTime;
                nowIdx = i;
            }
        }
        return mons[nowIdx];
    }

    public void SelfDestroy()
    {
        transform.parent.GetComponent<Tile_Controller>().TowerNum = 0;
        Destroy(this.gameObject);
    } 
}
