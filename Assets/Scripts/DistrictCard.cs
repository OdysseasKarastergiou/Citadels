using UnityEngine;
using Unity.Netcode;

public class DistrictCard : Card
{
    public DistrictCardDatabase.DistrictCardData District { get; private set; }

    public void Setup(DistrictCardDatabase.DistrictCardData district, ulong ownerId)
    {
        District = district;
        OwnerId = ownerId;
        IsVisible = NetworkManager.Singleton.LocalClientId == ownerId; // Only visible to owner
        transform.rotation = Quaternion.identity; // No rotation
        UpdateVisibility();
    }

    public override void UpdateVisibility()
    {
        if (cardFrontImage != null)
        {
            cardFrontImage.gameObject.SetActive(IsVisible);
        }
        if (cardBackImage != null)
        {
            cardBackImage.gameObject.SetActive(!IsVisible);
        }
    }
} 