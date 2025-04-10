using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class LobbyPanel : MonoBehaviour
{
    [Header("Title")]
    [SerializeField] private TextMeshProUGUI lobbyTitleText;

    [Header("Game Info")]
    [SerializeField] private TextMeshProUGUI lobbyNameText;
    [SerializeField] private TextMeshProUGUI playerCountText;

    [Header("Player List")]
    [SerializeField] private Transform playerListContent;
    [SerializeField] private GameObject playerListItemPrefab;

    [Header("Buttons")]
    [SerializeField] private Button startGameButton;
    [SerializeField] private Button backButton;

    [Header("Navigation")]
    [SerializeField] private GameObject mainMenuPanel;

    private List<GameObject> playerListItems = new List<GameObject>();
    private int maxPlayers = 4; // Default value, will be set from game settings

    private void OnEnable()
    {
        // Initialize when the panel is enabled
        InitializeLobby();
    }

    private void InitializeLobby()
    {
        // Set up button listeners
        if (startGameButton != null)
        {
            startGameButton.onClick.AddListener(OnStartGameClicked);
            startGameButton.interactable = false; // Initially disabled
        }

        if (backButton != null)
        {
            backButton.onClick.AddListener(OnBackClicked);
        }

        // Load game settings
        LoadGameSettings();
        
        // Initial update
        UpdateLobbyInfo();
        UpdatePlayerList();
    }

    private void LoadGameSettings()
    {
        // Get game name and max players from PlayerPrefs
        string gameName = PlayerPrefs.GetString("GameName", "My Game");
        maxPlayers = PlayerPrefs.GetInt("NumPlayers", 4);

        // Update UI
        if (lobbyTitleText != null)
        {
            lobbyTitleText.text = "Lobby";
        }

        if (lobbyNameText != null)
        {
            lobbyNameText.text = gameName;
        }
    }

    private void UpdateLobbyInfo()
    {
        // Update player count
        if (playerCountText != null)
        {
            int currentPlayers = playerListItems.Count;
            playerCountText.text = $"{currentPlayers}/{maxPlayers}";

            // Enable start button only when lobby is full
            if (startGameButton != null)
            {
                startGameButton.interactable = currentPlayers == maxPlayers;
            }
        }
    }

    private void UpdatePlayerList()
    {
        if (playerListContent == null || playerListItemPrefab == null) return;

        // Clear existing player items
        foreach (Transform child in playerListContent)
        {
            Destroy(child.gameObject);
        }
        playerListItems.Clear();

        // Add only the host player for now
        // In the future, this will be updated when other players join
        AddPlayerItem("Host", true);

        // Update lobby info after adding players
        UpdateLobbyInfo();
    }

    private void AddPlayerItem(string playerName, bool isHost)
    {
        GameObject item = Instantiate(playerListItemPrefab, playerListContent);
        playerListItems.Add(item);

        var nameText = item.transform.Find("PlayerNameText")?.GetComponent<TextMeshProUGUI>();
        var statusText = item.transform.Find("StatusText")?.GetComponent<TextMeshProUGUI>();

        if (nameText != null)
        {
            nameText.text = playerName;
            nameText.color = isHost ? Color.green : Color.white;
        }

        if (statusText != null)
        {
            statusText.text = isHost ? "Host" : "Ready";
            statusText.color = isHost ? Color.green : Color.yellow;
        }
    }

    private void OnStartGameClicked()
    {
        // TODO: In the future, this will start the game for all players
        Debug.Log("Starting game...");
    }

    private void OnBackClicked()
    {
        gameObject.SetActive(false);
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }
    }
} 