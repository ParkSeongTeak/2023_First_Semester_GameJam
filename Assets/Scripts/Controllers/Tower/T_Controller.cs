using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Controller : MonoBehaviour
{
    public List<Monster_Controller> InAreaMonster;
    float FullDist_x, FullDist_y,FullDist;
    GameObject Projectile;
    Vector3 target;
    public List<GameObject> inRangeMonster; 

    bool isDelay;

    [SerializeField]
    Define.Property property = Define.Property.Fire;

    Vector3[] _direction = new Vector3[9];
    // Start is called before the first frame update
    void Start()
    {
        _direction = GameManager.Instance.Direction;
        InAreaMonster = new List<Monster_Controller>();
        Projectile = Resources.Load<GameObject>($"Prefabs/Projectile/Projectile{(int)property}");
        isDelay = false;
        StartCoroutine(ContinueShoot());

    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        //공격할 몬스터 넣기
        if (other.gameObject.tag == "Mob")
        {
            inRangeMonster.Add(other.gameObject);
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
        FullDist_x = Mathf.Abs(_direction[1].x - _direction[2].x) * 4;
        FullDist_y = Mathf.Abs(_direction[0].y - _direction[1].y) * 4;
        FullDist = FullDist_x + FullDist_y + Mathf.Abs(GameObject.Find("StartPoint").transform.position.x - GameObject.Find("EndPoint").transform.position.x);


        if (InAreaMonster.Count == 0)
        {
            target = this.transform.position;
            return target;
        }

        return target;
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
