using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    const float maxLifetime = 5;
    public BulletData bulletData;

    [HideInInspector]
    public Weapon weapon;
    
    Rigidbody2D rb;
    float startTime = 0;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        startTime = Time.time;
    }

    void FixedUpdate()
    {
        Move();

        if (Time.time - startTime > maxLifetime)
            gameObject.SetActive(false);
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
            gameObject.SetActive(false);
        }
    }
}
