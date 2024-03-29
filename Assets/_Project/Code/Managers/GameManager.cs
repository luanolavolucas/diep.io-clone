﻿using Cinemachine;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;
using UnityEngine.SceneManagement;
using Photon.Pun;
using Photon.Realtime;

public class GameManager : MonoBehaviourPunCallbacks
{
    static GameManager instance;
    public static GameManager Instance {
        get{
            return instance;
        }
    }
    public CharacterSpawnPoint[] CharacterSpawnPoints { get; private set; }
    public PlayerController PlayerInstance { get; private set; }

    [Header("Set in Inspector")]
    public GameArea GameArea;
    public MatchSetupData matchSetupData;
    public GameObject playerPrefab;
    public UIController ui;
    public GameObject bulletPools;

    //Not used yet:
    public Action onGameStart;
    public Action onGameEnd;
    

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

        CharacterSpawnPoints = FindObjectsOfType<CharacterSpawnPoint>();
    }

    void OnDestroy()
    {
        instance = null;   
    }
    void Start()
    {
        StartGame();
    }
    void StartGame()
    {
        SpawnPlayer();
        AttachCameraToPlayer();
        // onGameStart.Invoke();
    }
    void SpawnPlayer()
    {
        if (playerPrefab == null)
        {
            Debug.LogError("<Color=Red><a>Missing</a></Color> playerPrefab Reference. Please set it up in GameObject 'Game Manager'", this);
        }

        else
        {
            foreach (CharacterSpawnPoint ssp in CharacterSpawnPoints)
            {
                if (ssp.CanSpawn && ssp.playerSpawnPoint)
                {
                    PlayerInstance = PhotonNetwork.Instantiate(this.playerPrefab.name, Vector3.zero, Quaternion.identity, 0).GetComponent<PlayerController>();
                    PlayerInstance.transform.position = ssp.transform.position;
                    PlayerInstance.character.OnCharacterDestroyed.AddListener(GameOver);
                    break;
                }
            }
            // we're in a room. spawn a character for the local player. it gets synced by using PhotonNetwork.Instantiate

        }
    }

    void AttachCameraToPlayer()
    {
        CinemachineVirtualCamera vc = FindObjectOfType<CinemachineVirtualCamera>();
        if (vc == null)
        {
            Debug.LogError("Cinemachine Virtual Camera not found in the scene.");
        }
        else
        {
            vc.Follow = PlayerInstance.transform;
        }
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
        string health = PlayerInstance.character.Health.ToString();
        string score = PlayerInstance.character.Score.ToString();
        string weaponName = PlayerInstance.character.WeaponSlot.Weapon.weaponData.weaponName;
        string ammo = 
        PlayerInstance.character.WeaponSlot.Weapon.weaponData.infiniteAmmo? "" : PlayerInstance.character.WeaponSlot.Weapon.Ammo.ToString();

        ui.SetDisplays(health, score, weaponName, ammo);
    }







    void GameOver(Character s)
    {
        ui.ShowGameOver(s.Score.ToString());
        LeaveRoom();
    }

    public override void OnLeftRoom()
    {
        //SceneManager.LoadScene(0);
    }

    public void LeaveRoom()
    {
        PhotonNetwork.LeaveRoom();
    }

    public void ResetScene()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }
}
