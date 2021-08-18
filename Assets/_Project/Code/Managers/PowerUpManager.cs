using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;

public class PowerUpManager : MonoBehaviourPun
{
    [Header("Set in Inspector")]
    public GameObject cratePrefab;
    public GameObject[] powerUpPrefabs;
    [SerializeField]
    private List<Crate> crates;

    [SerializeField]
    private LayerMask layersToAvoidWhenSpawningCrates;
    private CrateSpawnPoint[] crateSpawnPoints;
    private MatchSetupData matchSetupData;
    private float crateSpawnTimer = 0;

    private void Awake()
    {
        crateSpawnPoints = FindObjectsOfType<CrateSpawnPoint>();
        crates = new List<Crate>();
    }
    private void Start()
    {
        matchSetupData = GameManager.Instance.matchSetupData;
    }

    // Update is called once per frame
    private void Update()
    {

        if (crates.Count < matchSetupData.maxPowerUps)
        {
            crateSpawnTimer += Time.deltaTime;
            if (crateSpawnTimer > matchSetupData.timeBetweenCrateSpawns)
            {
                int randomSpawnPoint = Random.Range(0, crateSpawnPoints.Length);
                SpawnCrate(crateSpawnPoints[randomSpawnPoint]);
            }
        }
    }

    private void SpawnCrate(CrateSpawnPoint csp)
    {
        if (PhotonNetwork.IsMasterClient)
        {
            int randomPowerUp = Random.Range(0, powerUpPrefabs.Length);
            photonView.RPC("SpawnCrateRPC", RpcTarget.AllBufferedViaServer, csp.transform.position, randomPowerUp);
        }

        crateSpawnTimer = 0;
    }
    [PunRPC]
    private void SpawnCrateRPC(Vector3 position, int randomPowerUp)
    {
        print("Spawning crate.");
        while (Physics2D.OverlapCircle(position, 2, layersToAvoidWhenSpawningCrates.value) != null)
        {
            position += new Vector3(Random.Range(-2, 2), Random.Range(-2, 2), 0);
        }
        GameObject crateGO = Instantiate(cratePrefab, position, Quaternion.identity);
        Crate c = crateGO.GetComponent<Crate>();
        c.SetPowerUpPrefab(powerUpPrefabs[randomPowerUp]);
        c.OnCrateDestroyed.AddListener(RemoveCrate);
        crates.Add(c);
    }



    void RemoveCrate(Crate c)
    {
        c.OnCrateDestroyed.RemoveListener(RemoveCrate);
        crates.Remove(c);
    }
}
