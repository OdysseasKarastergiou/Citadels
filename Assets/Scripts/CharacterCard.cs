using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public class CharacterCard : Card
{
    public CharacterRole Role { get; private set; }
    public ulong OwnerId { get; private set; }
    public bool IsVisible { get; private set; }
    public bool IsAssassinated { get; private set; }
    public bool IsRobbed { get; private set; }

    [SerializeField] private Image cardFrontImage;
    [SerializeField] private Image cardBackImage;
    [SerializeField] private RectTransform rectTransform;

    private void Awake()
    {
        if (rectTransform == null)
        {
            rectTransform = GetComponent<RectTransform>();
        }

        // If images weren't assigned in the Inspector, try to find them
        if (cardFrontImage == null)
        {
            cardFrontImage = transform.Find("CardFront")?.GetComponent<Image>();
            if (cardFrontImage == null)
            {
                Debug.LogError($"Card Front Image is missing on {gameObject.name}");
            }
        }

        if (cardBackImage == null)
        {
            cardBackImage = transform.Find("CardBack")?.GetComponent<Image>();
            if (cardBackImage == null)
            {
                Debug.LogError($"Card Back Image is missing on {gameObject.name}");
            }
        }
    }

    public void Setup(CharacterRole role, ulong ownerId)
    {
        Role = role;
        OwnerId = ownerId;
        IsVisible = true;
        IsAssassinated = false;
        IsRobbed = false;
        UpdateVisibility();
        
        // Rotate character cards 270 degrees
        rectTransform.rotation = Quaternion.Euler(0, 0, 270);
    }

    public void SetCardSprites(Sprite frontSprite, Sprite backSprite)
    {
        if (cardFrontImage != null)
        {
            cardFrontImage.sprite = frontSprite;
        }
        if (cardBackImage != null)
        {
            cardBackImage.sprite = backSprite;
        }
    }

    public void SetAssassinated(bool assassinated)
    {
        IsAssassinated = assassinated;
        UpdateVisibility();
    }

    public void SetRobbed(bool robbed)
    {
        IsRobbed = robbed;
        UpdateVisibility();
    }

    public override void UpdateVisibility()
    {
        if (cardFrontImage != null)
        {
            cardFrontImage.gameObject.SetActive(IsVisible && !IsAssassinated && !IsRobbed);
        }
        if (cardBackImage != null)
        {
            cardBackImage.gameObject.SetActive(!IsVisible || IsAssassinated || IsRobbed);
        }
    }

    // Character-specific abilities
    public void Assassinate(CharacterRole target)
    {
        if (Role == CharacterRole.Assassin)
        {
            // Implementation will be added later
        }
    }

    public void Steal(CharacterRole target)
    {
        if (Role == CharacterRole.Thief)
        {
            // Implementation will be added later
        }
    }

    public void SwapCards(ulong targetPlayerId)
    {
        if (Role == CharacterRole.Magician)
        {
            // Implementation will be added later
        }
    }

    public void DiscardAndDraw()
    {
        if (Role == CharacterRole.Magician)
        {
            // Implementation will be added later
        }
    }

    public void DestroyDistrict(ulong targetPlayerId, int districtIndex)
    {
        if (Role == CharacterRole.Warlord)
        {
            // Implementation will be added later
        }
    }
} 