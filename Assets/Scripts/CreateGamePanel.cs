using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class CreateGamePanel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject mainMenuPanel;
    
    [Header("Game Settings Fields")]
    [SerializeField] private TMP_InputField gameNameInput;
    [SerializeField] private TMP_Dropdown playersDropdown;
    [SerializeField] private TMP_Dropdown roundTimerDropdown;
    
    [Header("Buttons")]
    [SerializeField] private Button createServerButton;
    [SerializeField] private Button cancelButton;

    private static readonly string[] playerOptions = new string[7];
    private static readonly string[] timerOptions = new[] { "30 sec", "1 min", "infinite" };

    private void Start()
    {
        if (playersDropdown != null)
        {
            for (int i = 0; i < 7; i++)
            {
                playerOptions[i] = (i + 2).ToString();
            }
            playersDropdown.ClearOptions();
            playersDropdown.AddOptions(new List<string>(playerOptions));
        }

        if (roundTimerDropdown != null)
        {
            roundTimerDropdown.ClearOptions();
            roundTimerDropdown.AddOptions(new List<string>(timerOptions));
        }

        if (createServerButton != null)
        {
            createServerButton.onClick.AddListener(OnCreateServerClicked);
        }
        if (cancelButton != null)
        {
            cancelButton.onClick.AddListener(OnCancelClicked);
        }
    }

    private void OnCreateServerClicked()
    {
        if (gameNameInput == null || playersDropdown == null || GameNetworkManager.Instance == null) return;

        string gameName = gameNameInput.text;
        if (string.IsNullOrEmpty(gameName))
        {
            gameName = "Game " + Random.Range(1000, 9999);
        }

        int maxPlayers = playersDropdown.value + 2;
        GameNetworkManager.Instance.HostGame(gameName, maxPlayers);
    }

    private void OnCancelClicked()
    {
        if (mainMenuPanel != null)
        {
            gameObject.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
    }
} 