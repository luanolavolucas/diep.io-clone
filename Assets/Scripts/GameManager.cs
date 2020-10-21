using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public GameManager Instance {
        get{
            if(instance == null)
            {
                instance = this;
                return this;
            }
            return instance;
        }
    }
    [Header("Set in Inspector")]
    public GameObject playerPrefab;
    public UIController ui;
    public GameObject aiPrefab;
    public GameObject bulletPool;
    public Action onGameStart;
    public Action onGameEnd;

    Player playerInstance;
    ShipSpawnPoint[] shipSpawnPoints;


    void Start()
    {
        shipSpawnPoints = FindObjectsOfType<ShipSpawnPoint>();
        StartGame();
    }

    void Update()
    {
        UpdateUI();
    }

    void UpdateUI()
    {
        string health = playerInstance.ship.Health.ToString();
        string score = playerInstance.ship.Score.ToString();
        string weaponName = playerInstance.ship.WeaponSlot.Weapon.weaponData.weaponName;
        string ammo = 
        playerInstance.ship.WeaponSlot.Weapon.weaponData.infiniteAmmo? "" : playerInstance.ship.WeaponSlot.Weapon.Ammo.ToString();

        ui.SetDisplays(health, score, weaponName, ammo);
    }

    void StartGame()
    {
        SpawnPlayer();
        AttachCameraToPlayer();
        //SpawnAI();
       // onGameStart.Invoke();
    }

    void AttachCameraToPlayer()
    {
        CinemachineVirtualCamera vc = FindObjectOfType<CinemachineVirtualCamera>();
        if(vc == null)
        {
            Debug.LogError("Cinemachine Virtual Camera not found in the scene.");
        }
        else
        {
            vc.Follow = playerInstance.transform;
        }
    }

    void SpawnAI()
    {
        throw new NotImplementedException();
    }

    void SpawnPlayer()
    {
        Player p = FindObjectOfType<Player>();

        if (p != null)
        {
            playerInstance = p;
            return;
        }

        foreach (ShipSpawnPoint ssp in shipSpawnPoints)
        {
            if (ssp.CanSpawn)
            {
                playerInstance = Instantiate(playerPrefab).GetComponent<Player>();
                playerInstance.transform.position = ssp.transform.position;
            }
        }
    }
}
