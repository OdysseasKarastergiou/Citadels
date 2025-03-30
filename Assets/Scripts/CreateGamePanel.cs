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

    private void Start()
    {
        // Setup players dropdown
        if (playersDropdown != null)
        {
            List<string> playerOptions = new List<string>();
            for (int i = 2; i <= 8; i++)
            {
                playerOptions.Add(i.ToString());
            }
            playersDropdown.ClearOptions();
            playersDropdown.AddOptions(playerOptions);
        }

        // Setup round timer dropdown
        if (roundTimerDropdown != null)
        {
            List<string> timerOptions = new List<string> { "30 sec", "1 min", "infinite" };
            roundTimerDropdown.ClearOptions();
            roundTimerDropdown.AddOptions(timerOptions);
        }

        // Add button listeners
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
        if (gameNameInput != null && playersDropdown != null && roundTimerDropdown != null)
        {
            string gameName = gameNameInput.text;
            int playerCount = int.Parse(playersDropdown.options[playersDropdown.value].text);
            string roundTimer = roundTimerDropdown.options[roundTimerDropdown.value].text;
            
            Debug.Log($"Creating server with settings: Name={gameName}, Players={playerCount}, Timer={roundTimer}");
        }
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