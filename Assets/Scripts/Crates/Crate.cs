using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
public class Crate : MonoBehaviour, IDamageable
{
    public float Health { get; protected set; }

    public GameObject powerUpPrefab;
    public CrateData crateData;
    public Action<Crate> onCrateKill;
    void Awake()
    {
        Health = crateData.health;
    }

    public void Damage(float dmg, IScoreCollector responsible = null)
    {
        Health-= dmg;
        if(Health <= 0)
        {
            if (responsible != null)
                responsible.AddToScore(crateData.scoreAwardedWhenDestroyed);
            Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
            onCrateKill?.Invoke(this);
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
