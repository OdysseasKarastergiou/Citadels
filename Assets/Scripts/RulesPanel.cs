using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System.Collections.Generic;

public class RulesPanel : MonoBehaviour
{
    [Header("UI References")]
    [SerializeField] private GameObject mainMenuPanel;
    [SerializeField] private TextMeshProUGUI rulesText;
    [SerializeField] private Button previousButton;
    [SerializeField] private Button nextButton;
    
    private int currentPage = 0;
    private static readonly string[] rulesPages = new[]
    {
        // Page 1: Introduction
        "Welcome to Citadels!\n\n" +
        "Citadels is a strategic card game where players take on different character roles to build their districts and gain points. " +
        "Each round, players secretly choose a character, and then take turns performing actions based on their character's special abilities.\n\n" +
        "The game ends when a player builds their eighth district. The player with the most points wins!",

        // Page 2: Setup
        "Game Setup:\n\n" +
        "1. Shuffle the district cards and deal 4 to each player\n" +
        "2. Place the remaining district cards face down as the draw pile\n" +
        "3. Place the character cards face up in the center\n" +
        "4. Give each player 2 gold\n" +
        "5. Randomly choose the first player",

        // Page 3: Character Roles
        "Character Roles:\n\n" +
        "Each round, players secretly choose a character. Characters have unique abilities:\n\n" +
        "• Assassin: Choose a character to eliminate\n" +
        "• Thief: Steal gold from another character\n" +
        "• Magician: Exchange cards with another player\n" +
        "• King: Take first turn and receive gold\n" +
        "• Bishop: Protected from Warlord, receive gold\n" +
        "• Merchant: Receive extra gold\n" +
        "• Architect: Draw extra cards\n" +
        "• Warlord: Destroy a district",

        // Page 4: Game Flow
        "Game Flow:\n\n" +
        "1. Character Selection:\n" +
        "   • Players secretly choose characters\n" +
        "   • Reveal choices simultaneously\n" +
        "   • Eliminated characters are skipped\n\n" +
        "2. Character Actions:\n" +
        "   • Players take turns in character order\n" +
        "   • Each turn: Take 2 actions\n" +
        "   • Actions: Draw cards or take gold\n" +
        "   • Use character ability when it's your turn",

        // Page 5: Building Districts
        "Building Districts:\n\n" +
        "• Cost: Pay gold equal to district's cost\n" +
        "• Colors:\n" +
        "  - Noble (Yellow): 3 points\n" +
        "  - Religious (Blue): 3 points\n" +
        "  - Trade (Green): 3 points\n" +
        "  - Military (Red): 3 points\n" +
        "  - Special (Purple): 4 points\n\n" +
        "• First player to build 8 districts triggers end game",

        // Page 6: Scoring
        "Scoring:\n\n" +
        "• Points from districts\n" +
        "• Bonus points:\n" +
        "  - First to 8 districts: 4 points\n" +
        "  - All 5 colors: 3 points\n" +
        "  - Most gold: 2 points\n\n" +
        "The player with the most points wins!"
    };

    private void Start()
    {
        if (previousButton != null)
        {
            previousButton.onClick.AddListener(OnPreviousClicked);
        }
        if (nextButton != null)
        {
            nextButton.onClick.AddListener(OnNextClicked);
        }

        UpdateUI();
    }

    private void UpdateUI()
    {
        if (rulesText != null && currentPage >= 0 && currentPage < rulesPages.Length)
        {
            rulesText.text = rulesPages[currentPage];
        }
    }

    private void OnPreviousClicked()
    {
        if (currentPage > 0)
        {
            currentPage--;
            UpdateUI();
        }
    }

    private void OnNextClicked()
    {
        if (currentPage < rulesPages.Length - 1)
        {
            currentPage++;
            UpdateUI();
        }
    }

    public void OnReturnClicked()
    {
        if (mainMenuPanel != null)
        {
            gameObject.SetActive(false);
            mainMenuPanel.SetActive(true);
        }
    }
}