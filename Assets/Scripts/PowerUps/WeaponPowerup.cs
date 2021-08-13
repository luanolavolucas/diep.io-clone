using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponPowerup : PowerUp
{
    public GameObject weaponPrefab;
    Weapon weapon;

    void Awake()
    {
        weapon = weaponPrefab.GetComponent<Weapon>();
    }
    protected override void ApplyPowerUp(Character s)
    {
        if (weapon.weaponData == s.WeaponSlot.Weapon.weaponData)
        {
            s.WeaponSlot.Weapon.Ammo += weapon.weaponData.startingAmmo;
            return;
        }
        s.WeaponSlot.EquipWeapon(weaponPrefab.gameObject);
    }
}
