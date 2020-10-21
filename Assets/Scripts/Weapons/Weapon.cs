using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Weapon : MonoBehaviour
{
    [field: SerializeField]
    public int Ammo { get;  set; }
    public Ship Owner { get; set; }

    [Header("Set in Inspector")]
    public BulletExitPoint[] bulletExitPoints;
    public WeaponData weaponData;

    //TODO: Use bullet pool:
    protected List<Bullet> bulletPool;

    void Start()
    {
        bulletExitPoints = GetComponentsInChildren<BulletExitPoint>();
        Ammo = weaponData.startingAmmo;
    }

    public virtual void Fire()
    {        
        foreach(BulletExitPoint bep in bulletExitPoints)
        {
            if (Ammo<=0)
            {
                //TODO: Find a better place for this logic, probably inside Ship instead.
                Owner.WeaponSlot.EquipWeapon(Owner.WeaponSlot.defaultWeapon);
                return;
            }
            GameObject bullet = Instantiate(weaponData.bulletPrefab, bep.transform.position, bep.transform.rotation);
            
            bullet.GetComponent<Bullet>().weapon = this;
            Ammo -= weaponData.ammoSpentPerShot;
        }
    }
}
