using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PoolingManager 
{
    Dictionary<string, Transform> _pooling = new Dictionary<string, Transform>();

    GameObject root = null;

    public void init()
    {
        
        if (root == null )
        {
            root =  new GameObject ("root");

            GameObject Monster = new GameObject ("MonsterFire" );
            _pooling.Add("MonsterFire", Monster.transform);
            Monster.transform.parent = root.transform;

            Monster = new GameObject ("MonsterWater" );
            _pooling.Add("MonsterWater", Monster.transform);
            Monster.transform.parent = root.transform;

            Monster = new GameObject ("MonsterGrass" );
            _pooling.Add("MonsterGrass", Monster.transform);
            Monster.transform.parent = root.transform;


            GameObject Projectile = new GameObject ("ProjectileFire" );
            _pooling.Add("ProjectileFire", Projectile.transform);
            Projectile.transform.parent = root.transform;

            Projectile =new GameObject ("ProjectileWater");
            _pooling.Add("ProjectileWater", Projectile.transform);
            Projectile.transform.parent = root.transform;

            Projectile = new GameObject ("ProjectileGrass" );
            _pooling.Add("ProjectileGrass", Projectile.transform);
            Projectile.transform.parent = root.transform;

            GameObject Play = new GameObject("PlayProjectile");
            _pooling.Add("PlayProjectile", Play.transform);
            Play.transform.parent = root.transform;

            Play = new GameObject("PlayMonster");
            _pooling.Add("PlayMonster", Play.transform);
            Play.transform.parent = root.transform;



        }

    }

    public GameObject GetPoolMonster(Define.Property property)
    {
        Transform MonsterPool;
        string name = $"Monster{Enum.GetName(typeof(Define.Property), property) }";
        _pooling.TryGetValue(name, out MonsterPool);
        if(MonsterPool.childCount == 0)
        {
            return null;
        }
        else
        {

            GameObject pool = MonsterPool.GetChild(0).gameObject;
            _pooling.TryGetValue("PlayMonster", out MonsterPool);
            pool.transform.parent = MonsterPool;
            return pool;
        }

    }
    public GameObject GetPoolProjectile(Define.Property property)
    {

        Transform ProjectilePool;
        string name = $"Projectile{Enum.GetName(typeof(Define.Property), property) }";
        _pooling.TryGetValue(name, out ProjectilePool);
        if (ProjectilePool.childCount == 0)
        {
            return null;
        }
        else
        {
            GameObject pool = ProjectilePool.GetChild(0).gameObject;
            _pooling.TryGetValue("PlayProjectile", out ProjectilePool);
            pool.transform.parent = ProjectilePool;
            return pool;
        }
    }
    public void SetPoolMonster(Define.Property property, GameObject destroyObj)
    {
        Transform MonsterPool;

        string name = $"Monster{Enum.GetName(typeof(Define.Property), property) }";
        _pooling.TryGetValue(name, out MonsterPool);
        destroyObj.transform.parent = MonsterPool;

    }
    public void SetPoolProjectile(Define.Property property, GameObject destroyObj)
    {

        Transform Projectile;

        string name = $"Projectile{Enum.GetName(typeof(Define.Property), property) }";
        _pooling.TryGetValue(name, out Projectile);
        destroyObj.transform.parent = Projectile;

    }
    public void Clear()
    {
        _pooling.Clear();
        UnityEngine.Object.Destroy(root);
    }
}
