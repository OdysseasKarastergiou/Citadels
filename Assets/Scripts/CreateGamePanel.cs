using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.SceneManagement;

public class CreateGamePanel : MonoBehaviour
{
    [Header("UI Elements")]
    [SerializeField] private TMP_InputField gameNameInput;
    [SerializeField] private TMP_Dropdown playersDropdown;
    [SerializeField] private TMP_Dropdown roundTimerDropdown;
    [SerializeField] private Button createButton;
    [SerializeField] private Button cancelButton;
    [SerializeField] private GameObject mainMenuPanel;

    private void Start()
    {
        // Set up players dropdown (2-8 players)
        if (playersDropdown != null)
        {
            playersDropdown.ClearOptions();
            for (int i = 2; i <= 8; i++)
            {
                playersDropdown.options.Add(new TMP_Dropdown.OptionData($"{i} Players"));
            }
            playersDropdown.value = 0; // Default to 2 players
            playersDropdown.RefreshShownValue();
        }

        // Set up round timer dropdown
        if (roundTimerDropdown != null)
        {
            roundTimerDropdown.ClearOptions();
            roundTimerDropdown.options.Add(new TMP_Dropdown.OptionData("30 Seconds"));
            roundTimerDropdown.options.Add(new TMP_Dropdown.OptionData("1 Minute"));
            roundTimerDropdown.options.Add(new TMP_Dropdown.OptionData("Unlimited"));
            roundTimerDropdown.value = 0; // Default to 30 seconds
            roundTimerDropdown.RefreshShownValue();
        }

        // Set up button listeners
        if (createButton != null)
        {
            createButton.onClick.AddListener(OnCreateClicked);
        }

        if (cancelButton != null)
        {
            cancelButton.onClick.AddListener(OnCancelClicked);
        }
    }

    private void OnCreateClicked()
    {
        if (gameNameInput != null && !string.IsNullOrEmpty(gameNameInput.text))
        {
            // Get selected number of players (2-8)
            int numPlayers = playersDropdown.value + 2;
            
            // Get selected round timer
            float roundTimer = GetRoundTimerFromDropdown();

            // Store game settings in PlayerPrefs or a static class for the game scene to access
            PlayerPrefs.SetString("GameName", gameNameInput.text);
            PlayerPrefs.SetInt("NumPlayers", numPlayers);
            PlayerPrefs.SetFloat("RoundTimer", roundTimer);
            PlayerPrefs.Save();

            // Load the game scene
            SceneManager.LoadScene("GameScene");
        }
    }

    private float GetRoundTimerFromDropdown()
    {
        if (roundTimerDropdown == null) return 30f; // Default to 30 seconds

        switch (roundTimerDropdown.value)
        {
            case 0: return 30f;    // 30 seconds
            case 1: return 60f;    // 1 minute
            case 2: return -1f;    // Unlimited (negative value indicates unlimited)
            default: return 30f;   // Default to 30 seconds
        }
    }

    private void OnCancelClicked()
    {
        gameObject.SetActive(false);
        if (mainMenuPanel != null)
        {
            mainMenuPanel.SetActive(true);
        }
    }
} 