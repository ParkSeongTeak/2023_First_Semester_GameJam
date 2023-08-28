using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Controller : MonoBehaviour
{
    float FullDist_x, FullDist_y,FullDist;
    GameObject Projectile;
    Vector3 target;
    public LinkedList<GameObject> inRangeMonster { get; set; } = new LinkedList<GameObject>();
    public void RemoveNode(GameObject value)
    {
        inRangeMonster.Remove(value);
    }
    bool isDelay;

    [SerializeField]
    Define.Properties property = Define.Properties.Fire;

    Vector3[] _direction = new Vector3[9];
    // Start is called before the first frame update
    void Start()
    {
        _direction = GameManager.Instance.Direction;
        Projectile = Resources.Load<GameObject>($"Prefabs/Projectile/Projectile{(int)property}");
        isDelay = false;
        StartCoroutine(ContinueShoot());

    }


    public void RemoveMonster(GameObject Monster)
    {
        inRangeMonster.Remove(Monster);
    }
    public void AddMonster(GameObject Monster)
    {
        inRangeMonster.AddLast(Monster);
    }



    public Vector3 NearestMonster()
    {
        FullDist_x = Mathf.Abs(_direction[1].x - _direction[2].x) * 4;
        FullDist_y = Mathf.Abs(_direction[0].y - _direction[1].y) * 4;
        FullDist = FullDist_x + FullDist_y + Mathf.Abs(GameObject.Find("StartPoint").transform.position.x - GameObject.Find("EndPoint").transform.position.x);


        if (inRangeMonster.Count == 0)
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
                Debug.Log("inRangeMonster.Count   "+inRangeMonster.Count);
                isDelay = true;
                if (inRangeMonster.First.Value != null)
                {
                    GameObject go = GameManager.Resource.InstantiateProjectile(property);
                    go.transform.position = this.transform.position;
                    go.GetComponent<Projectile_Controller>().setTarget(inRangeMonster.First.Value.transform.position);
                    go.SetActive(true);
                    GameManager.Sound.Play("Effect/gun2");

                }
                else
                {
                    Debug.Log("NUL???");
                }

                
                yield return new WaitForSeconds(GameManager.SHOOTSPEED[GameManager.Instance.LV[(int)Define.LV.AttackSpeed]]);
                isDelay = false;
            }
            else
            {
                yield return new WaitForSeconds(GameManager.SHOOTSPEED[GameManager.Instance.LV[(int)Define.LV.AttackSpeed]]);
            }
        }
    } 
   

    public void SelfDestroy()
    {
        transform.parent.GetComponent<Tile_Controller>().TowerNum = 0;
        Destroy(this.gameObject);
    } 
}
