public enum CharacterRole
{
    Assassin,    // 1 - Can assassinate another character
    Thief,       // 2 - Can steal gold from another character
    Magician,    // 3 - Can swap cards with another player or discard and draw new cards
    King,        // 4 - Gets crown and extra gold for noble districts
    Bishop,      // 5 - Protected from Warlord, gets extra gold for religious districts
    Merchant,    // 6 - Gets extra gold for trade districts and +1 gold at start of turn
    Architect,   // 7 - Can build up to 3 districts per turn
    Warlord      // 8 - Can destroy a district and pay its cost - 1 gold
} 