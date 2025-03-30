using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class JoinGamePanel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private TMP_InputField searchInput;
    [SerializeField] private Button searchButton;
    
    [Header("Game List")]
    [SerializeField] private Transform gameListContent;
    [SerializeField] private GameObject gameListItemPrefab;
    [SerializeField] private GameObject noGamesMessage;
    
    [Header("Navigation Buttons")]
    [SerializeField] private Button returnButton;
    [SerializeField] private Button refreshButton;
    [SerializeField] private Button joinButton;
    
    // Temporary list of games for testing
    private List<GameInfo> availableGames = new List<GameInfo>();
    private GameInfo selectedGame;

    private void Start()
    {
        // Add button listeners
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
            joinButton.interactable = false; // Disable join button until a game is selected
        }

        // Add some test games
        AddTestGames();
    }

    private void AddTestGames()
    {
        availableGames.Clear();
        // Comment out these lines to test the "No games" message
        availableGames.Add(new GameInfo { Name = "Game 1", CurrentPlayers = 2, MaxPlayers = 6, HasStarted = false });
        availableGames.Add(new GameInfo { Name = "Game 2", CurrentPlayers = 3, MaxPlayers = 4, HasStarted = false });
        availableGames.Add(new GameInfo { Name = "Game 3", CurrentPlayers = 1, MaxPlayers = 8, HasStarted = false });
        availableGames.Add(new GameInfo { Name = "Test Game", CurrentPlayers = 4, MaxPlayers = 6, HasStarted = false });
    }

    private void OnSearchClicked()
    {
        string searchTerm = searchInput.text.ToLower();
        UpdateGameList(searchTerm);
    }

    private void UpdateGameList(string searchTerm = "")
    {
        // Clear existing list
        foreach (Transform child in gameListContent)
        {
            Destroy(child.gameObject);
        }

        // Show/hide no games message
        if (noGamesMessage != null)
        {
            noGamesMessage.SetActive(availableGames.Count == 0);
        }

        // Filter and display games
        foreach (var game in availableGames)
        {
            if (string.IsNullOrEmpty(searchTerm) || 
                game.Name.ToLower().Contains(searchTerm))
            {
                CreateGameListItem(game);
            }
        }
    }

    private void CreateGameListItem(GameInfo game)
    {
        GameObject item = Instantiate(gameListItemPrefab, gameListContent);
        
        // Set game name
        var nameText = item.transform.Find("GameNameText").GetComponent<TextMeshProUGUI>();
        nameText.text = game.Name;
        
        // Set players count
        var playersText = item.transform.Find("PlayersText").GetComponent<TextMeshProUGUI>();
        playersText.text = $"{game.CurrentPlayers}/{game.MaxPlayers}";
        
        // Add click handler
        var button = item.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => OnGameSelected(game));
        }
    }

    private void OnGameSelected(GameInfo game)
    {
        selectedGame = game;
        
        // Update join button interactability
        if (joinButton != null)
        {
            bool canJoin = CanJoinGame(game);
            joinButton.interactable = canJoin;
        }
    }

    private bool CanJoinGame(GameInfo game)
    {
        if (game == null) return false;
        
        // Check if game exists
        if (!availableGames.Contains(game)) return false;
        
        // Check if game is full
        if (game.CurrentPlayers >= game.MaxPlayers) return false;
        
        // Check if game has started
        if (game.HasStarted) return false;
        
        return true;
    }

    private void OnJoinClicked()
    {
        if (selectedGame != null && CanJoinGame(selectedGame))
        {
            // TODO: Implement actual game joining functionality
            Debug.Log($"Joining game: {selectedGame.Name}");
        }
    }

    private void OnRefreshClicked()
    {
        // TODO: Implement actual server refresh
        AddTestGames(); // For now, just refresh the test games
        UpdateGameList(searchInput.text);
    }

    private void OnReturnClicked()
    {
        if (mainMenuPanel != null)
        {
            gameObject.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
    }
}

// Class to hold game information
public class GameInfo
{
    public string Name { get; set; }
    public int CurrentPlayers { get; set; }
    public int MaxPlayers { get; set; }
    public bool HasStarted { get; set; }
} 