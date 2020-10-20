using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Crate : MonoBehaviour, IDamageable
{
    //public GameObject weaponToSpawn;
    public CrateData crateData;
    public float Health { get; protected set; }
    // Start is called before the first frame update
    void Start()
    {
        Health = crateData.health;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        
    }

    public void Damage(float dmg)
    {
        Health-= dmg;
        if(Health <= 0)
        {
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
