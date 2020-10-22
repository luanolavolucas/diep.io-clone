using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;

[RequireComponent(typeof(Rigidbody2D))]
public class Ship : MonoBehaviour, IDamageable, IWeaponEquippable, IScoreCollector
{
    [SerializeField]
    float health;
    public float Health {
        get { return health; }
        protected set {
           health = value;
        }
    }
    [field: SerializeField]
    public float Score { get; protected set; }

    public WeaponSlot WeaponSlot { get; set; }

    public ShipData shipData;
    public Team team;

    public Action<Ship> onShipKill;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Tween brake;

    void Awake()
    {
        Health = shipData.health;
        WeaponSlot = GetComponentInChildren<WeaponSlot>();
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
        sr.color = team.teamColor;
    }

    public void MoveTowards(Vector3 target)
    {
        Vector3 direction = Vector3.Normalize(target - transform.position);
        Move(direction.x, direction.y);
    }

    public void Move(float directionX, float directionY)
    {
        rb.velocity = new Vector2(directionX,directionY) * shipData.speed;
    }

    public void Brake(float time)
    {
        brake = DOTween.To(() => rb.velocity, x => rb.velocity = x, new Vector2(0, 0), time);
    }

    public void Damage(float dmg, IScoreCollector responsible = null)
    {
        Health -= dmg;

        if (Health <= 0)
        {
            if (responsible != null)
                responsible.AddToScore(shipData.scoreAwardedWhenDestroyed);
            brake.Kill();
            onShipKill?.Invoke(this);

            Destroy(this.gameObject);
        }
    }

    public void Aim(Vector3 target)
    {
        Vector3 difference = target - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }

    public void Fire()
    {
        WeaponSlot.Weapon.Fire();
    }
    public void AddToScore(float value)
    {
        Score += value;
    }

    public void ResetScore()
    {
        Score = 0.0f;
    }
}
