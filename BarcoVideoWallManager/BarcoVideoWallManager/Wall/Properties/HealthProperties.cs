using System.Text.Json.Serialization;

namespace BarcoVideoWallManager;

public class WallHealthResponse
{
    [JsonPropertyName("kind")] public string? Kind { get; private set;}
    [JsonPropertyName("health")] public string? Health { get; private set;}
    
}
public enum WallHealthState
{
    Ok,
    Warning,
    Error
}

