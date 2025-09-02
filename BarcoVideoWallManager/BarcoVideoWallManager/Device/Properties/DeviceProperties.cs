using System.Text.Json.Serialization;

namespace BarcoVideoWallManager;

public class GetDeviceTemperatureResponse
{
    [JsonPropertyName("kind")] public string? Kind { get; set; }
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("refNumber")] public int? RefNumber { get; set; }
    [JsonPropertyName("position")] public PositionInfo? Position { get; set; }
    [JsonPropertyName("temperatures")] public Dictionary<string, double>? Temperatures { get; set; }

}

public class PositionInfo
{
    [JsonPropertyName("column")] public int? Column { get; set; }
    [JsonPropertyName("row")] public int? Row { get; set; }
    
}

public class GetDevicesTemperatureResponse
{
    [JsonPropertyName("kind")] public string? Kind { get; set; }

    [JsonPropertyName("processors")]
    public List<DeviceTemperatureItem>? Processors { get; set; }

    [JsonPropertyName("displays")]
    public List<DeviceTemperatureItem>? Displays { get; set; }
}

public class DeviceTemperatureItem
{
    [JsonPropertyName("id")]
    public string? Id { get; set; }

    [JsonPropertyName("refNumber")]
    public int? RefNumber { get; set; }

    [JsonPropertyName("position")]
    public PositionInfo? Position { get; set; }

    [JsonPropertyName("temperatures")]
    public Dictionary<string, double>? Temperatures { get; set; }
}

public class GetDeviceFanSpeedResponse
{
    [JsonPropertyName("kind")] public string? Kind { get; }

    [JsonPropertyName("id")]
    public string? Id { get;  }

    [JsonPropertyName("refNumber")] 
    public int? RefNumber { get;  }

    [JsonPropertyName("position")] 
    public PositionInfo? Position { get; }

    [JsonPropertyName("speed")] 
    public int? Speed { get; }
}

public class GetDevicesFanSpeedResponse
{
    [JsonPropertyName("kind")] public string? Kind { get; }
    [JsonPropertyName("processors")] public List<DeviceFanSpeedItem>? Processors { get; }
}

public class DeviceFanSpeedItem
{
    [JsonPropertyName("id")] public string? Id { get; }
    [JsonPropertyName("refNumber")] public int? RefNumber { get; }
    [JsonPropertyName("speed")] public int? Speed { get; }
}