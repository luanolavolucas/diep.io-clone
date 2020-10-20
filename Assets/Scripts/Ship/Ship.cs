using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Ship : MonoBehaviour, IDamageable, IWeaponEquippable
{
    public ShipData shipData;
    public float Health {get; protected set;}
    public WeaponSlot WeaponSlot { get; set; }

    private Rigidbody2D rb;
    // Start is called before the first frame update
    void Start()
    {
        Health = shipData.health;
        WeaponSlot = GetComponentInChildren<WeaponSlot>();
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Move(float directionX, float directionY)
    {
        rb.velocity = new Vector2(directionX,directionY) * shipData.speed;
    }

    public void Damage(float dmg)
    {
        print("SHIP DAMAGED!");
    }

    public void Fire()
    {
        WeaponSlot.Weapon.Fire();
    }
}
