using System.Collections;
using System.Collections.Generic;
using System.ComponentModel;
using UnityEngine;

public class PowerUpManager : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject cratePrefab;
    public GameObject[] powerUpPrefabs;
    [SerializeField]
    List<Crate> crates;
    CrateSpawnPoint[] crateSpawnPoints;
    MatchSetupData matchSetupData;
    float crateSpawnTimer = 0;

    void Awake()
    {
        crateSpawnPoints = FindObjectsOfType<CrateSpawnPoint>();
        crates = new List<Crate>();
    }
    void Start()
    {
        matchSetupData = GameManager.Instance.matchSetupData;
    }

    // Update is called once per frame
    void Update()
    {
        crateSpawnTimer += Time.deltaTime;

        if (crateSpawnTimer > matchSetupData.timeBetweenCrateSpawns
            && crates.Count < matchSetupData.maxPowerUps)
        {
            int randomSpawnPoint = Random.Range(0, crateSpawnPoints.Length);
            SpawnCrate(crateSpawnPoints[randomSpawnPoint]);
        }
    }

    void SpawnCrate(CrateSpawnPoint csp)
    {
        int randomPowerUp = Random.Range(0, powerUpPrefabs.Length);

        GameObject crateGO = Instantiate(cratePrefab, csp.transform.position, Quaternion.identity);
        Crate c = crateGO.GetComponent<Crate>();
        c.powerUpPrefab = powerUpPrefabs[randomPowerUp];
        c.onCrateKill += RemoveCrate;
        crateSpawnTimer = 0;
        crates.Add(c);
    }

    void RemoveCrate(Crate c)
    {
        c.onCrateKill -= RemoveCrate;
        crates.Remove(c);
    }
}
