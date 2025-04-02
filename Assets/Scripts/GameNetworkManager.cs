using Unity.Netcode;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class GameNetworkManager : MonoBehaviour
{
    public static GameNetworkManager Instance { get; private set; }

    private readonly Dictionary<ulong, GameInfo> hostedGames = new Dictionary<ulong, GameInfo>();

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void HostGame(string gameName, int maxPlayers)
    {
        if (NetworkManager.Singleton == null) return;

        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        
        NetworkManager.Singleton.StartHost();
        
        var gameInfo = new GameInfo
        {
            Name = gameName,
            CurrentPlayers = 1,
            MaxPlayers = maxPlayers,
            HasStarted = false,
            HostId = NetworkManager.Singleton.LocalClientId
        };

        hostedGames[NetworkManager.Singleton.LocalClientId] = gameInfo;
        
        NetworkManager.Singleton.SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    public void JoinGame(ulong hostId)
    {
        if (NetworkManager.Singleton == null) return;

        NetworkManager.Singleton.OnClientConnectedCallback += OnClientConnected;
        NetworkManager.Singleton.OnClientDisconnectCallback += OnClientDisconnected;
        
        NetworkManager.Singleton.StartClient();
        
        NetworkManager.Singleton.SceneManager.LoadScene("GameScene", LoadSceneMode.Single);
    }

    private void OnClientConnected(ulong clientId)
    {
        if (hostedGames.TryGetValue(clientId, out GameInfo gameInfo))
        {
            gameInfo.CurrentPlayers++;
            UpdateGameInfoClientRpc(clientId, gameInfo);
        }
    }

    private void OnClientDisconnected(ulong clientId)
    {
        if (hostedGames.TryGetValue(clientId, out GameInfo gameInfo))
        {
            gameInfo.CurrentPlayers--;
            UpdateGameInfoClientRpc(clientId, gameInfo);
        }
    }

    [ClientRpc]
    private void UpdateGameInfoClientRpc(ulong hostId, GameInfo gameInfo)
    {
        hostedGames[hostId] = gameInfo;
        if (JoinGamePanel.Instance != null)
        {
            JoinGamePanel.Instance.UpdateGameList();
        }
    }

    public List<GameInfo> GetAvailableGames()
    {
        return new List<GameInfo>(hostedGames.Values);
    }

    public GameInfo GetGameInfo(ulong hostId)
    {
        return hostedGames.TryGetValue(hostId, out GameInfo gameInfo) ? gameInfo : null;
    }
} 