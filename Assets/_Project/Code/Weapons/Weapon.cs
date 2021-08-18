using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
using UnityEngine.Events;

public abstract class Weapon : MonoBehaviourPun
{
    [field: SerializeField]
    public int Ammo { get; set; }
    public Character Owner { get; set; }

    public UnityEvent OnWeaponFire;

    [Header("Set in Inspector")]
    public WeaponData weaponData;

    protected ObjectPool bulletPool;
    protected BulletExitPoint[] bulletExitPoints;

    private float lastTimeFired;

    private void Awake()
    {
        bulletExitPoints = GetComponentsInChildren<BulletExitPoint>();
        Ammo = weaponData.startingAmmo;
        CreateBulletPool();
    }

    [PunRPC]
    private void CreateBulletPool()
    {
        GameObject bulletPoolGO = new GameObject();
        bulletPool = bulletPoolGO.AddComponent<ObjectPool>();
        bulletPool.Init(weaponData.bulletPrefab, 20);
    }

    private void OnDestroy() => bulletPool.SetDestructionFlag(true);


    public virtual void Fire()
    {
        if (Time.time - lastTimeFired > weaponData.timeBetweenShots)
        {
            foreach (BulletExitPoint bep in bulletExitPoints)
        {
            if (Ammo <= 0)
            {
                //TODO: Find a better place for this logic, probably inside Ship instead.
                Owner.WeaponSlot.EquipWeapon(Owner.WeaponSlot.defaultWeapon);
                return;
            }
            GameObject bullet = bulletPool.Instantiate(bep.transform.position, bep.transform.rotation);

            bullet.GetComponent<Bullet>().weapon = this;
            Ammo -= weaponData.ammoSpentPerShot;
            lastTimeFired = Time.time;
        }
        print("On Weapon Fire.");
        OnWeaponFire.Invoke();
        }  
    }
}
