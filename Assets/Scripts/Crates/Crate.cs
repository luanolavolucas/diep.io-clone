using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Events;

public class Crate : MonoBehaviour, IDamageable
{
    public float Health { get; protected set; }

    [SerializeField]
    private GameObject powerUpPrefab;
    [SerializeField]
    private CrateData crateData;
    [SerializeField]
    public UnityEvent<Crate> OnCrateDestroyed;

    private void Awake()
    {
        Health = crateData.health;
    }

    public void SetPowerUpPrefab(GameObject prefab)
    {
        powerUpPrefab = prefab;
    }


    public void Damage(float dmg, GameObject responsible = null)
    {
        Health-= dmg;
        if (Health <= 0)
        {
            if (responsible != null)
            {
                IScoreCollector sc = responsible.GetComponent<IScoreCollector>();
                sc.AddToScore(crateData.scoreAwardedWhenDestroyed);
            }
            Instantiate(powerUpPrefab, transform.position, Quaternion.identity);
            OnCrateDestroyed?.Invoke(this);
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        IDamageable damageable = collision.gameObject.GetComponent<IDamageable>() ;
        if(damageable != null)
        {
            damageable.Damage(1);
        }
    }
}
