using System.Text.Json.Serialization;

namespace BarcoVideoWallManager;

public class WallBrightnessResponse
{
    [JsonPropertyName("kind")]
    public string Kind { get; set; }
    [JsonPropertyName("brightness")]
    public int Brightness { get; set; }
    [JsonPropertyName("minimum")]
    public int Minimum { get; set; }
    [JsonPropertyName("maximum")]
    public int Maximum { get; set;  }

}

public class SetWallBrightnessResponse
{
    [JsonPropertyName("kind")] public string Kind { get; set; }
    [JsonPropertyName("issuedBy")] public string IssuedBy { get; set; }
    [JsonPropertyName("result")] public string Result { get; set; }


}