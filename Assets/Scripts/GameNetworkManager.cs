using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameNetworkManager : MonoBehaviour
{
    public static GameNetworkManager Instance { get; private set; }

    public event System.Action OnGameInfoUpdated;

    private readonly Dictionary<ulong, GameInfo> hostedGames = new Dictionary<ulong, GameInfo>();
    private readonly Dictionary<ulong, bool> simulatedPlayers = new Dictionary<ulong, bool>();

    private void Awake()
    {
        Debug.Log("GameNetworkManager Awake");
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
            Debug.Log("GameNetworkManager initialized as singleton");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void HostGame(string gameName, int maxPlayers)
    {
        Debug.Log($"Hosting game: {gameName} with {maxPlayers} players");
        if (NetworkManager.Singleton == null)
        {
            Debug.LogError("NetworkManager.Singleton is null!");
            return;
        }

        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        
        NetworkManager.Singleton.StartHost();
        
        var gameInfo = new GameInfo
        {
            Name = gameName,
            CurrentPlayers = maxPlayers, // Set to max players for testing
            MaxPlayers = maxPlayers,
            HasStarted = false,
            HostId = NetworkManager.Singleton.LocalClientId
        };

        Debug.Log($"Created game info: CurrentPlayers={gameInfo.CurrentPlayers}, MaxPlayers={gameInfo.MaxPlayers}");
        hostedGames[NetworkManager.Singleton.LocalClientId] = gameInfo;

        // Simulate other players
        for (int i = 1; i < maxPlayers; i++)
        {
            ulong simulatedId = (ulong)i;
            simulatedPlayers[simulatedId] = true;
            Debug.Log($"Simulated player {i} added with ID {simulatedId}");
        }
        
        // Load scene after setting up game info
        NetworkManager.Singleton.SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void JoinGame(ulong hostId)
    {
        Debug.Log($"Joining game with host ID: {hostId}");
        if (NetworkManager.Singleton == null)
        {
            Debug.LogError("NetworkManager.Singleton is null!");
            return;
        }

        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        
        NetworkManager.Singleton.StartClient();
        
        // Load scene after setting up client
        NetworkManager.Singleton.SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    private void OnClientConnected(ulong clientId)
    {
        Debug.Log($"Client connected: {clientId}");
        if (hostedGames.TryGetValue(clientId, out GameInfo gameInfo))
        {
            gameInfo.CurrentPlayers++;
            UpdateGameInfoClientRpc(clientId, gameInfo);
            OnGameInfoUpdated?.Invoke();
        }
    }

    private void OnClientDisconnected(ulong clientId)
    {
        Debug.Log($"Client disconnected: {clientId}");
        if (hostedGames.TryGetValue(clientId, out GameInfo gameInfo))
        {
            gameInfo.CurrentPlayers--;
            UpdateGameInfoClientRpc(clientId, gameInfo);
            OnGameInfoUpdated?.Invoke();
        }
    }

    [ClientRpc]
    private void UpdateGameInfoClientRpc(ulong hostId, GameInfo gameInfo)
    {
        Debug.Log($"Updating game info for host {hostId}: CurrentPlayers={gameInfo.CurrentPlayers}, MaxPlayers={gameInfo.MaxPlayers}");
        hostedGames[hostId] = gameInfo;
        if (JoinGamePanel.Instance != null)
        {
            JoinGamePanel.Instance.UpdateGameList();
        }
        OnGameInfoUpdated?.Invoke();
    }

    public List<GameInfo> GetAvailableGames()
    {
        return new List<GameInfo>(hostedGames.Values);
    }

    public GameInfo GetGameInfo(ulong hostId)
    {
        var gameInfo = hostedGames.TryGetValue(hostId, out GameInfo info) ? info : null;
        Debug.Log($"Getting game info for host {hostId}: {(gameInfo != null ? $"CurrentPlayers={gameInfo.CurrentPlayers}, MaxPlayers={gameInfo.MaxPlayers}" : "null")}");
        return gameInfo;
    }

    public bool IsSimulatedPlayer(ulong clientId)
    {
        return simulatedPlayers.ContainsKey(clientId);
    }
} 