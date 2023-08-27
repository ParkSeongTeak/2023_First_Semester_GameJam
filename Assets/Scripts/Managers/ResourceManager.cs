using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ResourceManager 
{
    Action MonsterMove = null;

    GameObject[] _projectile = new GameObject[3];

    GameObject[] _monster = new GameObject[3];
    
    public List<GameObject> _monster_List = new List<GameObject>();
    public List<GameObject> Monster_List { get { return _monster_List; }set { _monster_List = value; } }

    public List<GameObject> InAreaMonster_List = new List<GameObject>();//3*3범위 몬스터

    int idx;


    //List 몬스터;
    //List 투사체;

    public void init()
    {
        for (int i = 0; i < 3; i++)
        {
            _monster[i] = Resources.Load<GameObject>($"Prefabs/Monster/Monster{i}");
        }
        for (int i = 0; i < 3; i++)
        {
            _projectile[i] = Resources.Load<GameObject>($"Prefabs/Projectile/Projectile{i}");

            if(_projectile[i] == null)
            {
                Debug.Log($"음슴{i}");
            }
            else
            {
                Debug.Log($"{_projectile[i].name}");

            }
        }
    }
    public GameObject InstantiateMonster(Define.Property property)
    {
        GameObject monster = GameManager.Pooling.GetPoolMonster(property);
        if (monster == null)
        {
            return UnityEngine.Object.Instantiate(_monster[(int)property]);
        }
        else
        {
            monster.SetActive(true);
            return monster;
        }
    }
    public GameObject InstantiateProjectile(Define.Property property)
    {

        GameObject projectile = GameManager.Pooling.GetPoolProjectile(property);
        if (projectile == null)
        {

            string name = $"Monster{Enum.GetName(typeof(Define.Property), property)}";
            //destroyObj.transform.parent = MonsterPool;

            return UnityEngine.Object.Instantiate(_projectile[(int)property]);
        }
        else
        {
            return projectile;
        }

    }
    public void DestroyMonster(Define.Property property, GameObject destroyObj)
    {

        //UnityEngine.Object.Destroy(destroyObj);
        //return;


        destroyObj.SetActive(false);
        GameManager.Pooling.SetPoolMonster(property, destroyObj);

    }
    public void DestroyProjectile(Define.Property property, GameObject destroyObj)
    {

        //UnityEngine.Object.Destroy(destroyObj);
        //return;

        destroyObj.SetActive(false);
        GameManager.Pooling.SetPoolProjectile(property, destroyObj);

    }


    public void OnUpdate()
    {
        // Null?? 죽어서 제거했음에도 계속 대행자가 부른다 왜?
        //MonsterMove.Invoke();
    }

    // Start is called before the first frame update
   
}
