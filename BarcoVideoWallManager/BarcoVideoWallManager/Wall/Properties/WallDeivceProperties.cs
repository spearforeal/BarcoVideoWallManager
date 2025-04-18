using System.Text.Json.Serialization;

namespace BarcoVideoWallManager;

public class WallDeviceResponse
{
    [JsonPropertyName("kind")] public string? Kind { get; private set; }
    [JsonPropertyName("processors")] public List<WallDeviceProcessor>? DeviceProcessors { get; set; }
    [JsonPropertyName("displays")] public List<WallDeviceDisplays>? DeviceDisplays { get; set; }
}