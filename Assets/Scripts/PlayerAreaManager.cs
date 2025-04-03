using UnityEngine;
using Unity.Netcode;

public class PlayerAreaManager : MonoBehaviour
{
    [Header("Player Area")]
    [SerializeField] private GameObject playerArea;

    [Header("Enemy Areas")]
    [SerializeField] private GameObject[] enemyAreas; // Array of enemy area GameObjects

    private void Start()
    {
        if (GameNetworkManager.Instance != null)
        {
            GameNetworkManager.Instance.OnGameInfoUpdated += UpdatePlayerAreas;
            UpdatePlayerAreas();
        }
    }

    private void OnDestroy()
    {
        if (GameNetworkManager.Instance != null)
        {
            GameNetworkManager.Instance.OnGameInfoUpdated -= UpdatePlayerAreas;
        }
    }

    private void UpdatePlayerAreas()
    {
        var gameInfo = GameNetworkManager.Instance.GetGameInfo(NetworkManager.Singleton.LocalClientId);
        if (gameInfo == null) return;

        // Show/hide enemy areas based on number of players
        int totalPlayers = gameInfo.CurrentPlayers;
        for (int i = 0; i < enemyAreas.Length; i++)
        {
            bool shouldShow = i < totalPlayers - 1; // -1 because we don't count the local player
            enemyAreas[i].SetActive(shouldShow);
        }
    }
} 