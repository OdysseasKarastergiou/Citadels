using UnityEngine;
using UnityEngine.UI;
using Unity.Netcode;

public abstract class Card : MonoBehaviour
{
    public ulong OwnerId { get; protected set; }
    public bool IsVisible { get; protected set; }

    [SerializeField] protected Image cardFrontImage;
    [SerializeField] protected Image cardBackImage;
    [SerializeField] protected RectTransform rectTransform;

    protected virtual void Awake()
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

    public virtual void SetCardSprites(Sprite frontSprite, Sprite backSprite)
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

    public abstract void UpdateVisibility();
} 