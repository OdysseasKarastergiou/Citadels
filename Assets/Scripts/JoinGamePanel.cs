using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class JoinGamePanel : MonoBehaviour
{
    public static JoinGamePanel Instance { get; private set; }

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

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
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

        UpdateGameList();
    }

    public void UpdateGameList(string searchTerm = "")
    {
        if (gameListContent == null) return;

        foreach (Transform child in gameListContent)
        {
            if (child.gameObject != noGamesMessage)
            {
                Destroy(child.gameObject);
            }
        }

        var availableGames = GameNetworkManager.Instance.GetAvailableGames();

        if (noGamesMessage != null)
        {
            noGamesMessage.SetActive(availableGames.Count == 0);
        }

        if (string.IsNullOrEmpty(searchTerm))
        {
            foreach (var game in availableGames)
            {
                CreateGameListItem(game);
            }
        }
        else
        {
            searchTerm = searchTerm.ToLower();
            foreach (var game in availableGames)
            {
                if (game.Name.ToLower().Contains(searchTerm))
                {
                    CreateGameListItem(game);
                }
            }
        }
    }

    private void CreateGameListItem(GameInfo game)
    {
        if (gameListItemPrefab == null || gameListContent == null) return;

        GameObject item = Instantiate(gameListItemPrefab, gameListContent);
        
        var nameText = item.transform.Find("GameNameText")?.GetComponent<TextMeshProUGUI>();
        if (nameText != null)
        {
            nameText.text = game.Name;
        }
        
        var playersText = item.transform.Find("PlayersText")?.GetComponent<TextMeshProUGUI>();
        if (playersText != null)
        {
            playersText.text = $"{game.CurrentPlayers}/{game.MaxPlayers}";
        }
        
        var button = item.GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(() => OnGameSelected(game));
        }
    }

    private void OnGameSelected(GameInfo game)
    {
        if (joinButton != null)
        {
            joinButton.interactable = CanJoinGame(game);
        }
    }

    private bool CanJoinGame(GameInfo game)
    {
        return game != null && game.CurrentPlayers < game.MaxPlayers && !game.HasStarted;
    }

    private void OnJoinClicked()
    {
        var selectedItem = gameListContent.GetComponentInChildren<Button>();
        if (selectedItem != null)
        {
            var gameInfo = selectedItem.GetComponent<GameInfo>();
            if (gameInfo != null && CanJoinGame(gameInfo))
            {
                GameNetworkManager.Instance.JoinGame(gameInfo.HostId);
            }
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

    private void OnReturnClicked()
    {
        if (mainMenuPanel != null)
        {
            gameObject.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
    }
} 