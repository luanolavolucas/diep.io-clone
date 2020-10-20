using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : MonoBehaviour
{
    public GameObject weaponPrefab;
    public Weapon Weapon { get; private set; }

    private void Start()
    {
        EquipWeapon(weaponPrefab);
    }
    public void EquipWeapon(GameObject weaponPrefab)
    {
        if(weaponPrefab != null)
        {

            if(Weapon == null)
            {
                Weapon = Instantiate(weaponPrefab, transform).GetComponent<Weapon>();
            }
            else
            {
                Destroy(Weapon);
                Weapon = Instantiate(weaponPrefab, transform).GetComponent<Weapon>();
            }
                
        }

    }
}
