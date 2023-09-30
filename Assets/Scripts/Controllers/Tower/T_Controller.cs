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

    static string Effectgun2 = "Effect/gun2";
    static WaitForSeconds[] SHOOTSPEED;
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
        
        if (SHOOTSPEED == null)
        {
            SHOOTSPEED = new WaitForSeconds[5];
            for (int i = 0; i < 5; i++)
            {
                SHOOTSPEED[i] = new WaitForSeconds(GameManager.Data.SHOOTSPEED[i]);

            }
        }

        StartCoroutine(ContinueShoot());

        

    }


    public void RemoveMonster(GameObject Monster)
    {
        inRangeMonster.Remove(Monster);
    }
    public void AddMonster(GameObject Monster)
    {
        inRangeMonster.Add(Monster);
    }


    IEnumerator ContinueShoot()
    {
        while (true)
        {
            if (isDelay == false && inRangeMonster.Count > 0)
            {
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
                    go.GetComponent<Projectile_Controller>().setTarget(inRangeMonster[0].transform.position);
                    go.SetActive(true);
                    GameManager.Sound.Play(Effectgun2);

                }

                
                yield return SHOOTSPEED[GameManager.Data.LV[(int)Define.LV.AttackSpeed]];
                isDelay = false;
            }
            else
            {
                yield return SHOOTSPEED[GameManager.Data.LV[(int)Define.LV.AttackSpeed]];
            }
        }
    } 
   

    public void SelfDestroy()
    {
        transform.parent.GetComponent<Tile_Controller>().TowerNum = 0;
        Destroy(this.gameObject);
    } 
}
