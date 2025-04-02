using UnityEngine;
using Unity.Netcode;

public class CardManager : MonoBehaviour
{
    public static CardManager Instance { get; private set; }

    [Header("Character Card Sprites")]
    [SerializeField] private Sprite assassinSprite;
    [SerializeField] private Sprite thiefSprite;
    [SerializeField] private Sprite magicianSprite;
    [SerializeField] private Sprite kingSprite;
    [SerializeField] private Sprite bishopSprite;
    [SerializeField] private Sprite merchantSprite;
    [SerializeField] private Sprite architectSprite;
    [SerializeField] private Sprite warlordSprite;
    [SerializeField] private Sprite characterCardBackSprite;

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

    public void SetupCharacterCard(CharacterCard card, CharacterRole role)
    {
        if (card == null) return;

        Sprite frontSprite = GetCharacterSprite(role);
        if (frontSprite != null)
        {
            card.SetCardSprites(frontSprite, characterCardBackSprite);
        }
        else
        {
            Debug.LogError($"Character sprite not found for role: {role}");
        }
    }

    private Sprite GetCharacterSprite(CharacterRole role)
    {
        return role switch
        {
            CharacterRole.Assassin => assassinSprite,
            CharacterRole.Thief => thiefSprite,
            CharacterRole.Magician => magicianSprite,
            CharacterRole.King => kingSprite,
            CharacterRole.Bishop => bishopSprite,
            CharacterRole.Merchant => merchantSprite,
            CharacterRole.Architect => architectSprite,
            CharacterRole.Warlord => warlordSprite,
            _ => null
        };
    }
} 