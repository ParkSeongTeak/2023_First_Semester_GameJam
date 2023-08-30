using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T_Controller : MonoBehaviour
{
    float FullDist_x, FullDist_y,FullDist;
    GameObject Projectile;
    Vector3 target;
    //public LinkedList<GameObject> inRangeMonster = new LinkedList<GameObject>();
    public List<GameObject> inRangeMonster = new List<GameObject>();

    bool isDelay;

    [SerializeField]
    Define.Properties property = Define.Properties.Fire;

    Vector3[] _direction = new Vector3[9];
    // Start is called before the first frame update
    void Start()
    {
        _direction = GameManager.Data.Direction;
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
        //inRangeMonster.AddLast(Monster);
        inRangeMonster.Add(Monster);
    }


    IEnumerator ContinueShoot()
    {
        while (true)
        {
            if (isDelay == false && inRangeMonster.Count > 0)
            {
                //Debug.Log("inRangeMonster.Count   "+inRangeMonster.Count);
                isDelay = true;
                if (inRangeMonster[0] != null)
                {
                    if (!inRangeMonster[0].activeSelf)
                    {
                        RemoveMonster(inRangeMonster[0]);
                        continue;
                    }
                    GameObject go = GameManager.Resource.InstantiateProjectile(property);
                    go.transform.position = this.transform.position;
                    //go.GetComponent<Projectile_Controller>().setTarget(inRangeMonster.First.Value.transform.position);
                    go.GetComponent<Projectile_Controller>().setTarget(inRangeMonster[0].transform.position);
                    go.SetActive(true);
                    GameManager.Sound.Play("Effect/gun2");

                }
                else
                {
                    Debug.Log("NUL???");
                }

                
                yield return new WaitForSeconds(GameManager.Data.SHOOTSPEED[GameManager.Data.LV[(int)Define.LV.AttackSpeed]]);
                isDelay = false;
            }
            else
            {
                yield return new WaitForSeconds(GameManager.Data.SHOOTSPEED[GameManager.Data.LV[(int)Define.LV.AttackSpeed]]);
            }
        }
    } 
   

    public void SelfDestroy()
    {
        transform.parent.GetComponent<Tile_Controller>().TowerNum = 0;
        Destroy(this.gameObject);
    } 
}
