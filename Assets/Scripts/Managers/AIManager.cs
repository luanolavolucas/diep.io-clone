using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class AIManager : MonoBehaviourPunCallbacks
{
    [Header("Set in Inspector")]
    public GameObject aiPrefab;
    [SerializeField]
    List<Character> ais;
    MatchSetupData matchSetupData;
    CharacterSpawnPoint[] characterSpawnPoints;

    float aiSpawnTimer = 0;

    void Awake()
    {
        ais = new List<Character>();
    }

    void Start()
    {
        matchSetupData = GameManager.Instance.matchSetupData;
        characterSpawnPoints = GameManager.Instance.CharacterSpawnPoints;
    }

    void Update()
    {
            aiSpawnTimer += Time.deltaTime;
            if (aiSpawnTimer > matchSetupData.timeBetweenAISpawns
                && ais.Count < matchSetupData.maxShips)
            {
                int randomSpawnPoint = UnityEngine.Random.Range(0, characterSpawnPoints.Length);
                SpawnAI(characterSpawnPoints[randomSpawnPoint]);
            }

    }

    void SpawnAI(CharacterSpawnPoint csp)
    {
        if (csp.playerSpawnPoint)
            return;

        if (PhotonNetwork.IsMasterClient)
        {
            GameObject ai = PhotonNetwork.InstantiateRoomObject(aiPrefab.name, csp.transform.position, Quaternion.identity);
            Character s = ai.GetComponent<Character>();
            s.OnCharacterDestroyed.AddListener(RemoveShip);
            ais.Add(s);
        }

        aiSpawnTimer = 0;
    }

    void RemoveShip(Character character)
    {
        character.OnCharacterDestroyed.AddListener(RemoveShip);
        ais.Remove(character);
    }
}
