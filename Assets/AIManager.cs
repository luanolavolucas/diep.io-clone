using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AIManager : MonoBehaviour
{
    [Header("Set in Inspector")]
    public GameObject aiPrefab;
    [SerializeField]
    List<Ship> ais;
    MatchSetupData matchSetupData;
    ShipSpawnPoint[] shipSpawnPoints;

    float aiSpawnTimer = 0;

    void Awake()
    {
        ais = new List<Ship>();
    }

    void Start()
    {
        matchSetupData = GameManager.Instance.matchSetupData;
        shipSpawnPoints = GameManager.Instance.ShipSpawnPoints;
    }

    void Update()
    {
        aiSpawnTimer += Time.deltaTime;
        if (aiSpawnTimer > matchSetupData.timeBetweenAISpawns
            && ais.Count < matchSetupData.maxShips)
        {
            int randomSpawnPoint = UnityEngine.Random.Range(0, shipSpawnPoints.Length);
            SpawnAI(shipSpawnPoints[randomSpawnPoint]);
        }
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
