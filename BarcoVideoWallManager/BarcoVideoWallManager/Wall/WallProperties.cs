using System.Text.Json.Serialization;

namespace BarcoVideoWallManager;

public class WallBrightnessResponse
{
    [JsonPropertyName("kind")]
    public string Kind { get; set; }
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
    public string Kind { get; set; }
    [JsonPropertyName("brightness")]
    public int BrightnessNits { get; set; }
    [JsonPropertyName("minimum")]
    public int MinimumNits { get; set; }
    [JsonPropertyName("maximum")]
    public int MaximumNits { get; set;  }

}

public class WallPowerStateResponse
{
    [JsonPropertyName("power")] public string Power { get; }
    
}

public enum WallPowerState
{
    On,
    Idle,
    Standby
    
}

public class SetWallBrightnessResponse
{
    [JsonPropertyName("kind")] public string Kind { get; set; }
    [JsonPropertyName("issuedBy")] public string IssuedBy { get; set; }
    [JsonPropertyName("result")] public string Result { get; set; }


}

public class WallPresetResponse
{
    [JsonPropertyName("kind")] public string Kind { get; }
    [JsonPropertyName("presets")] public List<WallPreset> Presets { get; set; }
    
}

public class WallPreset
{
    [JsonPropertyName("kind")] public string Kind { get; }
    [JsonPropertyName("id")] public string Id { get; }
    [JsonPropertyName("name")] public string Name { get; }
    [JsonPropertyName("active")] public bool Active { get; }
    
}

public class WallTemperatureResponse
{
    [JsonPropertyName("kind")] public string Kind { get; set; }
    [JsonPropertyName("processors")] public List<ProcessorTemperature> Processors { get; set; }
    [JsonPropertyName("displays")] public List<DisplaysTemperatures> Displays { get; set; }



}

public class ProcessorTemperature
{
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("refNumber")] public string RefNumber { get; set; }
    [JsonPropertyName("temperature")] public ProcessorTemperatureValues Temperatures { get; set; }

}

public class DisplaysTemperatures
{
    [JsonPropertyName("id")] public string Id { get; set; }
    [JsonPropertyName("position")] public Position Position { get; set; }
    [JsonPropertyName("temperatures")] public DisplayTemperatureValues Temperatures { get; set; }
    

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

