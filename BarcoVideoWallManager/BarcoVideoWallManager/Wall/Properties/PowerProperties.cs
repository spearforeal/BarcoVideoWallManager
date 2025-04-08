using System.Text.Json.Serialization;

namespace BarcoVideoWallManager;

public enum WallPowerState
{
    On,
    Idle,
    Standby
}

public class WallPowerStateResponse
{
    [JsonPropertyName("power")] public string? Power { get; }
}