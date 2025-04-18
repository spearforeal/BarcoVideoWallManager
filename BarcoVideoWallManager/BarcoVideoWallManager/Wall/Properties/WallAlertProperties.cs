using System.Text.Json.Serialization;

namespace BarcoVideoWallManager;

public class WallAlertResponse
{
    [JsonPropertyName("kind")] public string? Kind { get; set; }
    [JsonPropertyName("processors")] public List< ProcessorWallAlert>? Processors { get; set; }
    [JsonPropertyName("displays")] public List<DisplayWallAlert>? Displays { get; set; }
}
public class ProcessorWallAlert
{
    [JsonPropertyName("id")] public string? Id { get; }
    [JsonPropertyName("refNumber")] public int? RefNumber { get; }
    [JsonPropertyName("alerts")] public List<ProcessorWallAlertValues>? AlertValues { get; set; }
}
public class ProcessorWallAlertValues
{
    [JsonPropertyName("code")] public string? Code { get; set; }
    [JsonPropertyName("type")] public string? Type { get; set; }
}

public class DisplayWallAlert
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("position")] public List<DisplayAlertPosition>? AlertPosition { get; set; }
    [JsonPropertyName("alerts")] public List<DisplayAlertValues>? AlertValues { get; set; }
}
public class DisplayAlertPosition
{
    [JsonPropertyName("column")]public string? Column { get; set; }
    [JsonPropertyName("row")]public string? Row { get; set; }
}

public class DisplayAlertValues
{
    [JsonPropertyName("code")] public string? Code { get; set; }
    [JsonPropertyName("type")] public string? Type { get; set; }
}