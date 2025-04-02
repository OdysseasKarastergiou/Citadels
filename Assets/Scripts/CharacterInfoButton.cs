using UnityEngine;
using UnityEngine.UI;

public class CharacterInfoButton : MonoBehaviour
{
    [SerializeField] private CharacterInfoPanel infoPanel;

    private void Start()
    {
        Button button = GetComponent<Button>();
        if (button != null)
        {
            button.onClick.AddListener(OnButtonClick);
        }
    }

    private void OnButtonClick()
    {
        if (infoPanel != null)
        {
            infoPanel.OpenPanel();
        }
    }
} 