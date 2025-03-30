using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    [Header("Menu Panels")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private GameObject createGamePanel;
    [SerializeField] private GameObject settingsPanel;
    [SerializeField] private GameObject rulesPanel;
    [SerializeField] private GameObject joinGamePanel;

    [Header("Menu Buttons")]
    [SerializeField] private Button createGameButton;
    [SerializeField] private Button joinGameButton;
    [SerializeField] private Button settingsButton;
    [SerializeField] private Button rulesButton;

    private void Awake()
    {
        // Ensure panels are hidden at start
        if (createGamePanel != null)
        {
            createGamePanel.SetActive(false);
        }
        if (settingsPanel != null)
        {
            settingsPanel.SetActive(false);
        }
        if (rulesPanel != null)
        {
            rulesPanel.SetActive(false);
        }
        if (joinGamePanel != null)
        {
            joinGamePanel.SetActive(false);
        }
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }
    }

    private void Start()
    {
        // Add button listeners
        if (createGameButton != null)
        {
            createGameButton.onClick.AddListener(OnCreateGameClicked);
        }
        if (joinGameButton != null)
        {
            joinGameButton.onClick.AddListener(OnJoinGameClicked);
        }
        if (settingsButton != null)
        {
            settingsButton.onClick.AddListener(OnSettingsClicked);
        }
        if (rulesButton != null)
        {
            rulesButton.onClick.AddListener(OnRulesClicked);
        }
    }

    private void OnCreateGameClicked()
    {
        if (mainMenuPanel != null && createGamePanel != null)
        {
            mainMenuPanel.SetActive(false);
            createGamePanel.SetActive(true);
        }
    }

    private void OnJoinGameClicked()
    {
        if (mainMenuPanel != null && joinGamePanel != null)
        {
            mainMenuPanel.SetActive(false);
            joinGamePanel.SetActive(true);
        }
    }

    private void OnSettingsClicked()
    {
        if (mainMenuPanel != null && settingsPanel != null)
        {
            mainMenuPanel.SetActive(false);
            settingsPanel.SetActive(true);
        }
    }

    private void OnRulesClicked()
    {
        if (mainMenuPanel != null && rulesPanel != null)
        {
            mainMenuPanel.SetActive(false);
            rulesPanel.SetActive(true);
        }
    }
} 