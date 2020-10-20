using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "New Weapon Data", menuName = "ScriptableObjects/Weapon Data", order = 1)]
public class WeaponData : ScriptableObject
{
    public int startingAmmo;
    public int ammoSpentPerShot;
    public float firingSpeed;
    public GameObject bulletPrefab;
}
