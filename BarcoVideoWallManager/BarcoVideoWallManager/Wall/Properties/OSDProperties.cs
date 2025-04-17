using System.Text.Json.Serialization;

public class GetWallOsdResponse
{
    [JsonPropertyName("kind")] public string? Kind { get; set; }
    [JsonPropertyName("osd")] public List<string>? Osd { get; set; }
}

public class GetWallNameResponse
{
    [JsonPropertyName("kind")] public string? Kind { get; set; }
    [JsonPropertyName("name")] public List<string>? Name { get; set; }
    
}
