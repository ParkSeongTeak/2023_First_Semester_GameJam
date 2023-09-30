//Checking for stuttering issues
#define Pooling

using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;


public class PoolingManager
{
    Dictionary<string, Transform> _poolingRoot;
    public Dictionary<string, Transform> PoolingRoot { get { return _poolingRoot; } }

    Dictionary<string, Stack<GameObject>> _pool;
    static Dictionary<Define.Properties, string> _projectileNameForPool { get; set; }
    static Dictionary<Define.Properties, string> _monsterNameForPool { get; set; }

    GameObject root = null;

    public void Init()
    {
        _poolingRoot = new Dictionary<string, Transform>();
        _pool = new Dictionary<string, Stack<GameObject>>();
        if (root == null )
        {
            if(_projectileNameForPool == null)
            {
                _projectileNameForPool = new Dictionary<Define.Properties, string>();
                _projectileNameForPool.Add(Define.Properties.Fire, "MonsterFire");
                _projectileNameForPool.Add(Define.Properties.Water, "MonsterWater");
                _projectileNameForPool.Add(Define.Properties.Grass, "MonsterGrass");

            }
            if (_monsterNameForPool == null)
            {
                _monsterNameForPool = new Dictionary<Define.Properties, string>();
                _monsterNameForPool.Add(Define.Properties.Fire, "ProjectileFire");
                _monsterNameForPool.Add(Define.Properties.Water, "ProjectileWater");
                _monsterNameForPool.Add(Define.Properties.Grass, "ProjectileGrass");
            }

            root =  new GameObject ("root");

            GameObject Monster = new GameObject ("MonsterFire" );
            _poolingRoot.Add("MonsterFire", Monster.transform);
            _pool.Add("MonsterFire", new Stack<GameObject>());
            Monster.transform.parent = root.transform;

            Monster = new GameObject ("MonsterWater" );
            _poolingRoot.Add("MonsterWater", Monster.transform);
            _pool.Add("MonsterWater", new Stack<GameObject>());

            Monster.transform.parent = root.transform;

            Monster = new GameObject ("MonsterGrass" );
            _poolingRoot.Add("MonsterGrass", Monster.transform);
            _pool.Add("MonsterGrass", new Stack<GameObject>());

            Monster.transform.parent = root.transform;


            GameObject Projectile = new GameObject ("ProjectileFire" );
            _poolingRoot.Add("ProjectileFire", Projectile.transform);
            _pool.Add("ProjectileFire", new Stack<GameObject>());

            Projectile.transform.parent = root.transform;

            Projectile =new GameObject ("ProjectileWater");
            _poolingRoot.Add("ProjectileWater", Projectile.transform);
            _pool.Add("ProjectileWater", new Stack<GameObject>());

            Projectile.transform.parent = root.transform;

            Projectile = new GameObject ("ProjectileGrass" );
            _poolingRoot.Add("ProjectileGrass", Projectile.transform);
            _pool.Add("ProjectileGrass", new Stack<GameObject>());

            Projectile.transform.parent = root.transform;

            GameObject Play = new GameObject("PlayProjectile");
            _poolingRoot.Add("PlayProjectile", Play.transform);
            _pool.Add("PlayProjectile", new Stack<GameObject>());

            Play.transform.parent = root.transform;

            Play = new GameObject("PlayMonster");
            _poolingRoot.Add("PlayMonster", Play.transform);
            _pool.Add("PlayMonster", new Stack<GameObject>());

            Play.transform.parent = root.transform;

        }

    }

    public GameObject GetPoolMonster(Define.Properties property)
    {
        if (_pool[_monsterNameForPool[property]].Count == 0)
        {
            return null;
        }
        else
        {
            GameObject pool = _pool[_monsterNameForPool[property]].Pop();
            return pool;
        }

    }
    public GameObject GetPoolProjectile(Define.Properties property)
    { 
        
        if (_pool[_projectileNameForPool[property]].Count == 0)
        {
            return null;
        }
        else
        {
            return _pool[_projectileNameForPool[property]].Pop();
        }
    }
    public void SetPoolMonster(Define.Properties property, GameObject destroyObj)
    {

#if Pooling
        Transform MonsterPool;

        _poolingRoot.TryGetValue(_monsterNameForPool[property], out MonsterPool);
        
        _pool[_monsterNameForPool[property]].Push(destroyObj);
#else

        UnityEngine.Object.Destroy(destroyObj);
        return;
#endif

    }
    public void SetPoolProjectile(Define.Properties property, GameObject destroyObj)
    {
#if Pooling
        Transform Projectile;

        _poolingRoot.TryGetValue(_projectileNameForPool[property], out Projectile);
        //destroyObj.transform.parent = Projectile;

        _pool[_projectileNameForPool[property]].Push(destroyObj);
#else
        UnityEngine.Object.Destroy(destroyObj);
        return;
#endif

    }
    public void Clear()
    {
        _poolingRoot.Clear();
        _pool.Clear();

        UnityEngine.Object.Destroy(root);
    }
}
