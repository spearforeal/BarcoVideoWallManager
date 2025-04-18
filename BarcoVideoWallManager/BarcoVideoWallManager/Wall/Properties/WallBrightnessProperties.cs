using System.Text.Json.Serialization;

namespace BarcoVideoWallManager;

public class WallBrightnessResponse: ApiResponse
{
    [JsonPropertyName("brightness")]
    public int BrightnessPercentage { get; set; }
    [JsonPropertyName("minimum")]
    public int MinimumPercentage{ get; set; }
    [JsonPropertyName("maximum")]
    public int MaximumPercentage { get; set;  }

}
public class AbsoluteWallBrightnessResponse
{
    [JsonPropertyName("kind")]
    public string? Kind { get; set; }
    [JsonPropertyName("brightness")]
    public int BrightnessNits { get; set; }
    [JsonPropertyName("minimum")]
    public int MinimumNits { get; set; }
    [JsonPropertyName("maximum")]
    public int MaximumNits { get; set;  }

}
public class SetWallBrightnessResponse
{
    [JsonPropertyName("kind")] public string? Kind { get; set; }
    [JsonPropertyName("issuedBy")] public string? IssuedBy { get; set; }
    [JsonPropertyName("result")] public string? Result { get; set; }
}