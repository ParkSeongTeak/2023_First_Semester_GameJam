using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile_Controller : MonoBehaviour
{
    float _proj_Dmg;
    float _proj_Spead;
    
    Vector3 targetPos;//타겟 위치
    float _projSpeed = 15.0f;
    public float ProjSpeed { get { return _projSpeed; } }
    Vector3 Tpoint;//목표 위치

    [SerializeField]
    Define.Property property = Define.Property.Fire;

    Vector3[] _direction = new Vector3[9];

    GameManager Target;

    public float Proj_Dmg { get { return _proj_Dmg; } set { _proj_Dmg = value; } }
    public float Proj_Spead { get { return _proj_Spead; } set { _proj_Spead = value; } }

    private void OnEnable ()
    {
        Fly();
    }
    
    public Define.Property ProjProp()
    {
        return property;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.tag == "Mob")//몹이랑 닿으면 없어짐
        {
            GameManager.Resource.DestroyProjectile(property, this.gameObject);
        }
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
    public void Shoot(Vector3 point,Vector3 startPos)//목표 지점, 시작 지점
    {
        Target = new GameManager();
        gameObject.transform.LookAt(point);
        targetPos = point;
        
    }

    // Update is called once per frame
    
}
