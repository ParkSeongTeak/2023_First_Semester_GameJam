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



    public void Init()
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
    public GameObject InstantiateMonster(Define.Properties property)
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
    public GameObject InstantiateProjectile(Define.Properties property)
    {

        GameObject projectile = GameManager.Pooling.GetPoolProjectile(property);
        if (projectile == null)
        {

            string name = $"Monster{Enum.GetName(typeof(Define.Properties), property)}";
            //destroyObj.transform.parent = MonsterPool;

            return UnityEngine.Object.Instantiate(_projectile[(int)property]);
        }
        else
        {
            return projectile;
        }

    }
    public void DestroyMonster(Define.Properties property, GameObject destroyObj)
    {

        //UnityEngine.Object.Destroy(destroyObj);
        //return;


        destroyObj.SetActive(false);
        GameManager.Pooling.SetPoolMonster(property, destroyObj);

    }
    public void DestroyProjectile(Define.Properties property, GameObject destroyObj)
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
    Dictionary<string, UnityEngine.Object> Pool = new Dictionary<string, UnityEngine.Object>();

    public T Load<T>(string path) where T : UnityEngine.Object
    {
        if (!Pool.ContainsKey(path))
        {
            Pool.Add(path, Resources.Load<T>(path));
        }

        return Pool[path] as T;
    }

    /// <summary> GameObject 생성 </summary>
    public GameObject Instantiate(string path, Transform parent = null) => Instantiate<GameObject>(path, parent);
    /// <summary> T type object 생성 </summary>
    public T Instantiate<T>(string path, Transform parent = null) where T : UnityEngine.Object
    {
        T prefab = Load<T>($"Prefabs/{path}");
        if (prefab == null)
        {
            Debug.LogError($"Failed to load prefab : {path}");
            return null;
        }

        T instance = UnityEngine.Object.Instantiate<T>(prefab, parent);
        instance.name = prefab.name;

        return instance;
    }
    public void Destroy(GameObject go)
    {
        if (go == null)
            return;

        UnityEngine.Object.Destroy(go);
    }
    public void Clear()
    {
        Pool.Clear();
    }
}
