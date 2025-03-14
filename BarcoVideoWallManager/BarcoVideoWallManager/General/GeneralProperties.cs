using System.Text.Json.Serialization;

namespace BarcoVideoWallManager;

public class BarcoSoftwareVersionResponse
{
    [JsonPropertyName("kind")] public string Kind { get; set; }
    [JsonPropertyName("version")] public string Version { get; set; }

}

public class BarcoApiVersionResponse
{
    [JsonPropertyName("kind")] public string Kind { get; set; }
    [JsonPropertyName("version")] public string Version { get; set; }
}