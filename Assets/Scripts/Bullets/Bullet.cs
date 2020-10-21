using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletData bulletData;

    [HideInInspector]
    public Weapon weapon;
    
    Rigidbody2D rb;

    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float moveAmount = bulletData.speed* Time.deltaTime;
        transform.Translate(new Vector3(moveAmount,0,0));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(bulletData.damage, weapon.Owner);
            Destroy(this.gameObject);
        }
    }

    //void OnCollisionEnter2D(Collision2D collision)
    //{
    //    IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
    //    if (damageable != null)
    //    {
    //        damageable.Damage(bulletData.damage);
    //    }
    //}

}
