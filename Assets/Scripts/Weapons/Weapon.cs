using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : MonoBehaviour
{
    public float Ammo { get; protected set; }

    [Header("Set in Inspector")]
    public BulletExitPoint[] bulletExitPoints;
    public WeaponData weaponData;
    public GameObject bulletPool;

    // Start is called before the first frame update
    void Start()
    {
        bulletExitPoints = GetComponentsInChildren<BulletExitPoint>();
        Ammo = weaponData.startingAmmo;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public virtual void Fire()
    {
        foreach(BulletExitPoint bep in bulletExitPoints)
        {
           GameObject bullet = Instantiate(weaponData.bulletPrefab, bep.transform.position, bep.transform.rotation);
            //TODO:  Create bullet pool and bullet pool object
            //bullet.transform.parent = bulletPool.transform;
            Ammo--;
        }

    }
}
