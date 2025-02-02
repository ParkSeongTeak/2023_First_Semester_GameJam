using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Controller : MonoBehaviour
{
    float _proj_Dmg;
    float _proj_Spead;
    
    Vector3 targetPos;//Ÿ�� ��ġ
    float _projSpeed { get; set; } = 15.0f;
    public float ProjSpeed { get { return _projSpeed; } }
    Vector3 Tpoint;//��ǥ ��ġ

    [SerializeField]
    Define.Properties property = Define.Properties.Fire;

    Vector3[] _direction = new Vector3[9];

    GameManager Target;

    public float Proj_Dmg { get { return _proj_Dmg; } set { _proj_Dmg = value; } }
    public float Proj_Spead { get { return _proj_Spead; } set { _proj_Spead = value; } }
    Coroutine Coroutine;

    private void Awake()
    {
        Transform Projectile;

        string name = $"Projectile{Enum.GetName(typeof(Define.Properties), property)}";
        GameManager.Pooling.PoolingRoot.TryGetValue(name, out Projectile);
        transform.parent = Projectile;
    }
    private void Start()
    {
        Fly();
    }
    private void OnEnable ()
    {
        Fly();
        Coroutine = StartCoroutine(DestroyTime());
    }
    
    public Define.Properties ProjProp()
    {
        return property;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mob")//���̶� ������ ������
        {
            GameManager.Resource.DestroyProjectile(property, this.gameObject);
            StopCoroutine(Coroutine);
        }
    }
    static WaitForSeconds waitForSeconds = new WaitForSeconds(2.5f);

    IEnumerator DestroyTime()
    {
        yield return waitForSeconds;
        GameManager.Resource.DestroyProjectile(property, this.gameObject);
    }

    
    public void setTarget(Vector3 point)
    {
        Tpoint = point;
    }
    public void Fly()
    {
        GetComponent<Rigidbody2D>().velocity = (Vector2)(Tpoint - this.transform.position).normalized * ProjSpeed;
    }

    // Start is called before the first frame update
    public void Shoot(Vector3 point,Vector3 startPos)//��ǥ ����, ���� ����
    {
        //Target = new GameManager();
        gameObject.transform.LookAt(point);
        targetPos = point;
        
    }

    // Update is called once per frame
    
}
