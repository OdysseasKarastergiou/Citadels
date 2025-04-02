using Unity.Netcode;

public class GameInfo
{
    public string Name { get; set; }
    public int CurrentPlayers { get; set; }
    public int MaxPlayers { get; set; }
    public bool HasStarted { get; set; }
    public ulong HostId { get; set; }
} 