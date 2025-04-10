using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class JoinGamePanel : MonoBehaviour
{
    [Header("Search")]
    [SerializeField] private TMP_InputField searchInput;
    [SerializeField] private Button searchButton;

    [Header("Game List")]
    [SerializeField] private Transform gameListContent;
    [SerializeField] private GameObject gameListItemPrefab;
    [SerializeField] private GameObject noGamesMessage;

    [Header("Buttons")]
    [SerializeField] private Button returnButton;
    [SerializeField] private Button refreshButton;
    [SerializeField] private Button joinButton;

    [Header("Navigation")]
    [SerializeField] private GameObject mainMenuPanel;

    private List<GameInfo> availableGames = new List<GameInfo>();
    private GameInfo selectedGame;

    private void Start()
    {
        // Set up button listeners
        if (searchButton != null)
        {
            searchButton.onClick.AddListener(OnSearchClicked);
        }

        if (returnButton != null)
        {
            returnButton.onClick.AddListener(OnReturnClicked);
        }

        if (refreshButton != null)
        {
            refreshButton.onClick.AddListener(OnRefreshClicked);
        }

        if (joinButton != null)
        {
            joinButton.onClick.AddListener(OnJoinClicked);
            joinButton.interactable = false;
        }

        // Initial game list update
        UpdateGameList();
    }

    public void UpdateGameList(string searchTerm = "")
    {
        if (gameListContent == null) return;

        // Clear existing items
        foreach (Transform child in gameListContent)
        {
            if (child.gameObject != noGamesMessage)
            {
                Destroy(child.gameObject);
            }
        }

        // Show/hide no games message
        if (noGamesMessage != null)
        {
            noGamesMessage.SetActive(availableGames.Count == 0);
        }

        // Filter games based on search term
        var filteredGames = string.IsNullOrEmpty(searchTerm) 
            ? availableGames 
            : availableGames.FindAll(g => g.Name.ToLower().Contains(searchTerm.ToLower()));

        // Create list items
        foreach (var game in filteredGames)
        {
            CreateGameListItem(game);
        }
    }

    private void CreateGameListItem(GameInfo game)
    {
        if (gameListItemPrefab == null || gameListContent == null) return;

        GameObject item = Instantiate(gameListItemPrefab, gameListContent);
        
        // Set game name
        var nameText = item.transform.Find("GameNameText")?.GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = game.Name;
        }
        
        // Set player count
        var playersText = item.transform.Find("PlayersText")?.GetComponent<TextMeshProUGUI>();
        if (playersText != null)
        {
            playersText.text = $"{game.CurrentPlayers}/{game.MaxPlayers}";
        }
        
        // Add click listener
        var button = item.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => OnGameSelected(game));
        }
    }

    private void OnGameSelected(GameInfo game)
    {
        selectedGame = game;
        
        // Enable join button only if game is not full
        if (joinButton != null)
        {
            joinButton.interactable = game.CurrentPlayers < game.MaxPlayers;
        }
    }

    private void OnSearchClicked()
    {
        if (searchInput != null)
        {
            UpdateGameList(searchInput.text);
        }
    }

    private void OnRefreshClicked()
    {
        if (searchInput != null)
        {
            UpdateGameList(searchInput.text);
        }
    }

    private void OnJoinClicked()
    {
        if (selectedGame != null && selectedGame.CurrentPlayers < selectedGame.MaxPlayers)
        {
            // TODO: Implement join game logic
            Debug.Log($"Joining game: {selectedGame.Name}");
        }
    }

    private void OnReturnClicked()
    {
        gameObject.SetActive(false);
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }
    }
} 