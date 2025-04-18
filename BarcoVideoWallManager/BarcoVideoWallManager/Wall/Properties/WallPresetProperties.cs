using System.Text.Json.Serialization;

namespace BarcoVideoWallManager;

public class WallPreset
{
    [JsonPropertyName("kind")] public string? Kind { get; }
    [JsonPropertyName("id")] public string? Id { get; }
    [JsonPropertyName("name")] public string? Name { get; }
    [JsonPropertyName("active")] public bool? Active { get; }
}

public class WallPresetResponse
{
    [JsonPropertyName("kind")] public string? Kind { get; }
    [JsonPropertyName("presets")] public List<WallPreset>? Presets { get; set; }
}