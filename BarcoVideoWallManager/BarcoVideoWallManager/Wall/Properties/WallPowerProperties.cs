using System.Text.Json.Serialization;

namespace BarcoVideoWallManager;
/// <summary>
/// Power States: On, Idle, Standby
/// </summary>
public enum WallPowerState
{
    On,
    Idle,
    Standby
}
/// <summary>
/// Represents the response from GetPowerStateAsync. Contains the power state as a <see cref="WallPowerState"/> enum value.
/// </summary>
public class WallPowerStateResponse
{
    [JsonPropertyName("power")] public string? Power { get; }
}