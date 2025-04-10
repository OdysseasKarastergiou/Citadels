using UnityEngine;

public class PlayerAreaManager : MonoBehaviour
{
    [Header("Player Area")]
    [SerializeField] private GameObject playerArea;

    [Header("Enemy Areas")]
    [SerializeField] private GameObject[] enemyAreas;

    private void Start()
    {
        // Get the number of players from PlayerPrefs
        int numPlayers = PlayerPrefs.GetInt("NumPlayers", 2);
        
        // Show the player area
        if (playerArea != null)
        {
            playerArea.SetActive(true);
        }

        // Show enemy areas based on number of players
        // We show (numPlayers - 1) enemy areas because one is the local player
        for (int i = 0; i < enemyAreas.Length; i++)
        {
            if (enemyAreas[i] != null)
            {
                enemyAreas[i].SetActive(i < numPlayers - 1);
            }
        }
    }
} 