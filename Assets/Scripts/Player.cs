using System.Collections;
using System.Collections.Generic;
using UnityEditor.Experimental.GraphView;
using UnityEngine;

public class Player : Ship, IWeaponEquippable
{
    public WeaponSlot WeaponSlot { get; set; }

    // Start is called before the first frame update
    void Start()
    {
        WeaponSlot = GetComponentInChildren<WeaponSlot>();
        print(WeaponSlot);
        print(WeaponSlot.name);
    }

    // Update is called once per frame
    void Update()
    {
        Move(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));

        if (Input.GetButtonDown("Fire1"))
        {
            print("FIRE");
            Fire();
        }
    }

    void Fire()
    {
        print(WeaponSlot.name + " " + WeaponSlot.Weapon.name );
        WeaponSlot.Weapon.Fire();
    }




}
