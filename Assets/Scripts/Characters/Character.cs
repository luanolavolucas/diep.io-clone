using System.Collections;
using System;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using DG.Tweening;
using Photon.Pun;
using UnityEngine.Events;

[RequireComponent(typeof(Rigidbody2D))]
public class Character : MonoBehaviourPunCallbacks, IDamageable, IWeaponEquippable, IScoreCollector
{
    [SerializeField]
    float health;
    public float Health
    {
        get { return health; }
        protected set
        {
            health = value;
        }
    }
    [field: SerializeField]
    public float Score { get; protected set; }

    public WeaponSlot WeaponSlot { get; set; }

    public CharacterData characterData;
    public Team team;

    public UnityEvent<Character> OnCharacterDamaged, OnCharacterDestroyed, OnCharacterMoved;

    Rigidbody2D rb;
    SpriteRenderer sr;
    Tween brake;

    void Awake()
    {
        Health = characterData.health;
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
        rb.velocity = new Vector2(directionX, directionY) * characterData.speed;
        OnCharacterMoved.Invoke(this);
    }

    public void Brake(float time)
    {
        brake = DOTween.To(() => rb.velocity, x => rb.velocity = x, new Vector2(0, 0), time);
    }

    public void Damage(float damageValue, GameObject responsible = null)
    {
        PhotonView pv = responsible?.GetComponent<PhotonView>();
        //if a photon view caused the damage, send it's id (used to calculate the score), else send -1
        photonView.RPC("Dmg", RpcTarget.All, damageValue, pv ? pv.ViewID : -1);

    }

    [PunRPC]
    void Dmg(float dmg, int viewId = -1)
    {
        Health = Mathf.Max(Health - dmg, 0);
        print(Health);
        OnCharacterDamaged.Invoke(this);
        if (Health == 0)
        {
            if (viewId != -1)
            {
                Debug.LogFormat("Photon View with the id {0} destroyed an object", viewId);
                PhotonView.Find(viewId).GetComponent<IScoreCollector>().AddToScore(characterData.scoreAwardedWhenDestroyed);
            }

            brake.Kill();
            print("On CharacterDestroyed");
            OnCharacterDestroyed.Invoke(this);
            if (photonView.IsMine)
                PhotonNetwork.Destroy(this.gameObject);
        }
    }

    public void Aim(Vector3 target)
    {
        Vector3 difference = target - transform.position;
        float rotationZ = Mathf.Atan2(difference.y, difference.x) * Mathf.Rad2Deg;
        WeaponSlot.transform.parent.rotation = Quaternion.Euler(0.0f, 0.0f, rotationZ);
    }
    [PunRPC]
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
