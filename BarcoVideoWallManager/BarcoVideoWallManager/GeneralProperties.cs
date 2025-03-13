using System.Text.Json.Serialization;

namespace BarcoVideoWallManager;

public class GeneralVersionResponse
{
    [JsonPropertyName("kind")] public string Kind { get; set; }
    [JsonPropertyName("version")] public string Version { get; set; }

}