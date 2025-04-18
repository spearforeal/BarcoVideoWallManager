using System.Text.Json.Serialization;

namespace BarcoVideoWallManager;

public class WallTemperatureResponse
{
    [JsonPropertyName("kind")] public string? Kind { get; set; }
    [JsonPropertyName("processors")] public List<ProcessorTemperature>? Processors { get; set; }
    [JsonPropertyName("displays")] public List<DisplaysTemperatures>? Displays { get; set; }
}
public class ProcessorTemperature
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("refNumber")] public string? RefNumber { get; set; }
    [JsonPropertyName("temperature")] public ProcessorTemperatureValues? Temperatures { get; set; }

}
public class DisplaysTemperatures
{
    [JsonPropertyName("id")] public string? Id { get; set; }
    [JsonPropertyName("position")] public Position? Position { get; set; }
    [JsonPropertyName("temperatures")] public DisplayTemperatureValues? Temperatures { get; set; }
}
public class Position
{
    [JsonPropertyName("column")] public int Column { get; set; }
    [JsonPropertyName("row")] public int Row { get; set; }
    
}

public class ProcessorTemperatureValues
{
    [JsonPropertyName("board")] public double? Board { get; set; }
    [JsonPropertyName("fpga")] public double? Fpga { get; set; }
    
}

public class DisplayTemperatureValues
{
    [JsonPropertyName("interface")] public double? Interface { get; set;  }
    [JsonPropertyName("left")] public double? Left { get; set;  }
    [JsonPropertyName("main")] public double? Main { get; set;  }
    [JsonPropertyName("right")] public double? Right { get; set;  }
    [JsonPropertyName("lcm")] public double? Lcm { get; set;  }
    [JsonPropertyName("inputBoard")] public double? InputBoard { get; set;  }
}