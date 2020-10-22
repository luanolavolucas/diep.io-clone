using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance {
        get{
            return instance;
        }
    }
    [Header("Set in Inspector")]
    public MatchSetupData matchSetupData;
    public GameObject playerPrefab;
    public UIController ui;
    public GameObject aiPrefab;
    public GameObject bulletPools;
    public Action onGameStart;
    public Action onGameEnd;

    Player playerInstance;
    ShipSpawnPoint[] shipSpawnPoints;
    [SerializeField]
    List<Ship> ais;
    float aiSpawnTimer = 0;

    void Awake()
    {
        if (instance != null && instance != this)
        {
            Destroy(this.gameObject);
        }
        else
        {
            instance = this;
        }

        shipSpawnPoints = FindObjectsOfType<ShipSpawnPoint>();
        ais = new List<Ship>();
    }
    void Start()
    {
        StartGame();
    }

    void Update()
    {
        UpdateUI();
        UpdateSpawns();
    }

    void UpdateSpawns()
    {
        aiSpawnTimer += Time.deltaTime;
        if(aiSpawnTimer > matchSetupData.timeBetweenSpawns
            && ais.Count <matchSetupData.maxShips)
        {
            int randomSpawnPoint = UnityEngine.Random.Range(0, shipSpawnPoints.Length);
            SpawnAI(shipSpawnPoints[randomSpawnPoint]);
        }

        void SpawnAI(ShipSpawnPoint ssp)
        {
            if (ssp.playerSpawnPoint)
                return;
            GameObject ai = Instantiate(aiPrefab, ssp.transform.position, Quaternion.identity);
            Ship s = ai.GetComponent<Ship>();
            s.onShipKill += RemoveShip;
            aiSpawnTimer = 0;
            ais.Add(s);
        }

        void RemoveShip(Ship s)
        {
            s.onShipKill -= RemoveShip;
            ais.Remove(s);
        }
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
            if (ssp.CanSpawn && ssp.playerSpawnPoint)
            {
                playerInstance = Instantiate(playerPrefab).GetComponent<Player>();
                playerInstance.transform.position = ssp.transform.position;
            }
        }
    }
}
