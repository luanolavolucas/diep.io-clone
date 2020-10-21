using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour, IDamageable
{
    public float Health { get; protected set; }

    public GameObject powerUpPrefab;
    public CrateData crateData;

    // Start is called before the first frame update
    void Start()
    {
        Health = crateData.health;
    }

    void OnDestroy()
    {
        Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
    }


    public void Damage(float dmg, IScoreCollector responsible = null)
    {
        Health-= dmg;
        if(Health <= 0)
        {
            if (responsible != null)
                responsible.AddToScore(crateData.scoreAwardedWhenDestroyed);
            Destroy(this.gameObject);
        }
    }
    void OnCollisionEnter2D(Collision2D collision)
    {
        print(collision.gameObject.name);
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>() ;
        if(damageable != null)
        {
            damageable.Damage(1);
        }
    }
}
