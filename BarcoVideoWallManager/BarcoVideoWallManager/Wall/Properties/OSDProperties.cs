using System.Text.Json.Serialization;

public class GetWallOSDResopnse
{
    [JsonPropertyName("kind")] public string? Kind { get; set; }
    [JsonPropertyName("osd")] public List<string>? Osd { get; set; }
}