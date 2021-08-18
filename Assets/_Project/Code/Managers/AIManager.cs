using System.Collections.Generic;
using UnityEngine;
using Photon.Pun;
public class AIManager : MonoBehaviourPunCallbacks
{
    [Header("Set in Inspector")]
    public GameObject aiPrefab;
    [SerializeField]
    private List<Character> ais;
    private MatchSetupData matchSetupData;
    private CharacterSpawnPoint[] characterSpawnPoints;

    private float aiSpawnTimer = 0;

    private void Awake() => ais = new List<Character>();

    private void Start()
    {
        matchSetupData = GameManager.Instance.matchSetupData;
        characterSpawnPoints = GameManager.Instance.CharacterSpawnPoints;
    }

    private void Update()
    {
            aiSpawnTimer += Time.deltaTime;
            if (aiSpawnTimer > matchSetupData.timeBetweenAISpawns
                && ais.Count < matchSetupData.maxShips)
            {
                int randomSpawnPoint = UnityEngine.Random.Range(0, characterSpawnPoints.Length);
                SpawnAI(characterSpawnPoints[randomSpawnPoint]);
            }

    }

    private void SpawnAI(CharacterSpawnPoint csp)
    {
        if (csp.playerSpawnPoint)
            return;

        if (PhotonNetwork.IsMasterClient && PhotonNetwork.InRoom)
        {
            GameObject ai = PhotonNetwork.InstantiateRoomObject(aiPrefab.name, csp.transform.position, Quaternion.identity);
            Character s = ai.GetComponent<Character>();
            s.OnCharacterDestroyed.AddListener(RemoveCharacter);
            ais.Add(s);
        }

        aiSpawnTimer = 0;
    }

    private void RemoveCharacter(Character character)
    {
        character.OnCharacterDestroyed.AddListener(RemoveCharacter);
        ais.Remove(character);
    }
}
