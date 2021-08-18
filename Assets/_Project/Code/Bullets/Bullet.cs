using UnityEngine;

//TODO: Refactor Bullet to use the new object pooling plugin.
public class Bullet : MonoBehaviour
{
    private const float maxLifetime = 5;
    public BulletData bulletData;

    [HideInInspector]
    public Weapon weapon;

    public TrailRenderer trail;

    private Rigidbody2D rb;
    float startTime = 0;
    void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    void OnEnable()
    {
        startTime = Time.time;
        trail.Clear();
    }

    void FixedUpdate()
    {
        Move();

        if (Time.time - startTime > maxLifetime)
            gameObject.SetActive(false);
    }

    void Move()
    {
        float moveAmount = bulletData.speed * Time.deltaTime;
        transform.Translate(new Vector3(moveAmount, 0, 0));
    }

    void OnTriggerEnter2D(Collider2D collision)
    {
        // print("Bullet entered.");
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>();
        if (damageable != null)
        {
            // print("Found IDamageable.");
            if (weapon.Owner)
            {
                //  print("Trying to damage.");
                damageable.Damage(bulletData.damage, weapon.Owner.gameObject);
            }
            gameObject.SetActive(false);
        }
    }
}
