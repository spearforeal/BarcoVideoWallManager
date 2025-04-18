using System.Text.Json.Serialization;

namespace BarcoVideoWallManager;

public class WallSizeResponse
{
    [JsonPropertyName("kind")] public string? Kind { get; private set; }
    [JsonPropertyName("width")] public string? Width { get; private set; }
    [JsonPropertyName("height")] public string? Height { get; private set; }
    

}
public class WallDeviceProcessor
{
    [JsonPropertyName("id")] public string? Id { get; private set; }
    [JsonPropertyName("refNumber")] public string? RefNumber { get; private set; }
}

public class WallDeviceDisplays
{
    [JsonPropertyName("id")] public string? Id { get; private set; }
    [JsonPropertyName("position")] public List<DeviceDisplayValues>? DisplayValues{ get; private set; }
}

public class DeviceDisplayValues
{
    [JsonPropertyName("column")] public string? Column { get; private set; }
    [JsonPropertyName("row")] public string? Row { get; private set; }
}