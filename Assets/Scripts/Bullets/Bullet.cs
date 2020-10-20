using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    public BulletData bulletData;

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        Move();
    }

    void Move()
    {
        float moveAmount = bulletData.speed* Time.deltaTime;
        transform.Translate(new Vector3(moveAmount,0,0));
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        print("BULLET");
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(bulletData.damage);
        }
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            damageable.Damage(bulletData.damage);
            Destroy(this.gameObject);
        }
    }

}
