using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WeaponSlot : MonoBehaviour
{
    public Weapon Weapon { get; private set; }

    public Character owner;
    public GameObject defaultWeapon;

    public UnityEvent OnWeaponEquipped;

    void Awake()
    {
        EquipWeapon(defaultWeapon);
    }

    public void EquipWeapon(GameObject weaponPrefab)
    {
        if (weaponPrefab != null)
        {

            if (Weapon != null)
            {
                Destroy(Weapon.gameObject);
            }
            Weapon = Instantiate(weaponPrefab, transform).GetComponent<Weapon>();
            Weapon.Owner = owner;
            OnWeaponEquipped.Invoke();

        }

    }
}
