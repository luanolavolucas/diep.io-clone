using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public abstract class Weapon : MonoBehaviourPun
{
    [field: SerializeField]
    public int Ammo { get;  set; }
    public Ship Owner { get; set; }

    [Header("Set in Inspector")]
    public WeaponData weaponData;

    protected ObjectPool bulletPool;
    protected BulletExitPoint[] bulletExitPoints;

    void Awake()
    {
        bulletExitPoints = GetComponentsInChildren<BulletExitPoint>();
        Ammo = weaponData.startingAmmo;
        CreateBulletPool();
    }

    [PunRPC]
    void CreateBulletPool()
    {
        GameObject bulletPoolGO = new GameObject();
        bulletPool = bulletPoolGO.AddComponent<ObjectPool>();
        bulletPool.Init(weaponData.bulletPrefab, 20);
    }

    void OnDestroy()
    {
        bulletPool.SetDestructionFlag(true);
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
            GameObject bullet = bulletPool.Instantiate(bep.transform.position, bep.transform.rotation);
            
            bullet.GetComponent<Bullet>().weapon = this;
            Ammo -= weaponData.ammoSpentPerShot;
        }
    }
}
