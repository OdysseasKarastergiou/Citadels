using UnityEngine;
using Unity.Netcode;

public class DistrictManager : MonoBehaviour
{
    public static DistrictManager Instance { get; private set; }

    [Header("District Card Sprites")]
    [SerializeField] private Sprite districtCardBackSprite;
    [SerializeField] private Sprite[] nobleDistricts;
    [SerializeField] private Sprite[] religiousDistricts;
    [SerializeField] private Sprite[] tradeDistricts;
    [SerializeField] private Sprite[] militaryDistricts;
    [SerializeField] private Sprite[] specialDistricts;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void SetupDistrictCard(DistrictCard card, string districtName)
    {
        if (card == null) return;

        var districtData = DistrictCardDatabase.Instance.GetDistrictData(districtName);
        if (districtData == null)
        {
            Debug.LogError($"District data not found for: {districtName}");
            return;
        }

        Sprite districtSprite = GetDistrictSprite(districtName);
        if (districtSprite != null)
        {
            card.SetCardSprites(districtSprite, districtCardBackSprite);
        }
        else
        {
            Debug.LogError($"District sprite not found for: {districtName}");
        }
    }

    private Sprite GetDistrictSprite(string districtName)
    {
        foreach (var sprite in nobleDistricts)
        {
            if (sprite.name == districtName) return sprite;
        }
        foreach (var sprite in religiousDistricts)
        {
            if (sprite.name == districtName) return sprite;
        }
        foreach (var sprite in tradeDistricts)
        {
            if (sprite.name == districtName) return sprite;
        }
        foreach (var sprite in militaryDistricts)
        {
            if (sprite.name == districtName) return sprite;
        }
        foreach (var sprite in specialDistricts)
        {
            if (sprite.name == districtName) return sprite;
        }
        return null;
    }
} 