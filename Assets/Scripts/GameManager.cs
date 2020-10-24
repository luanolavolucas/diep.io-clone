using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    static GameManager instance;
    public static GameManager Instance {
        get{
            return instance;
        }
    }
    public ShipSpawnPoint[] ShipSpawnPoints { get; private set; }

    [Header("Set in Inspector")]
    public GameArea GameArea;
    public MatchSetupData matchSetupData;
    public GameObject playerPrefab;
    public UIController ui;
    public GameObject bulletPools;

    //Not used yet:
    public Action onGameStart;
    public Action onGameEnd;
    Player playerInstance;

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

        ShipSpawnPoints = FindObjectsOfType<ShipSpawnPoint>();
    }
    void Start()
    {
        StartGame();
    }

    void Update()
    {
        UpdateUI();

        //Quick "quit game" hack, just making test easier.
        if (Input.GetKeyDown(KeyCode.Escape))
            SceneManager.LoadScene(0);
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

        foreach (ShipSpawnPoint ssp in ShipSpawnPoints)
        {
            if (ssp.CanSpawn && ssp.playerSpawnPoint)
            {
                playerInstance = Instantiate(playerPrefab).GetComponent<Player>();
                playerInstance.transform.position = ssp.transform.position;
            }
        }
        playerInstance.ship.onShipKill += GameOver;
    }

    void GameOver(Ship s)
    {
        ui.ShowGameOver(s.Score.ToString());
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
