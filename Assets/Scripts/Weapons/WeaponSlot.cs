using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    public Weapon Weapon { get; private set; }

    public Ship owner;
    public GameObject defaultWeapon;

    void Awake()
    {
        EquipWeapon(defaultWeapon);
    }

    public void EquipWeapon(GameObject weaponPrefab)
    {
        if(weaponPrefab != null)
        {

            if(Weapon == null)
            {
                Weapon = Instantiate(weaponPrefab, transform).GetComponent<Weapon>();
                Weapon.Owner = owner;
            }
            else
            {
                Destroy(Weapon.gameObject);
                Weapon = Instantiate(weaponPrefab, transform).GetComponent<Weapon>();
                Weapon.Owner = owner;
            }
                
        }

    }
}
