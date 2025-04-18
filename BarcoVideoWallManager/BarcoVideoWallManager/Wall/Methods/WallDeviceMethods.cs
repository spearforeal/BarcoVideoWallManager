namespace BarcoVideoWallManager;

public partial class Barco
{
    /// <summary>
    /// Retrieves the current wall device configuration, including processor and display details.
    /// </summary>
    /// <returns>A <see cref="WallDeviceResponse"/> containing device processor and display collections, or null if the request fails.</returns>
    public async Task<WallDeviceResponse?> GetWallDeviceAsync()
    {
        var response = await SendGetRequestAsync<WallDeviceResponse, CommandDictionary.Wall>(_c.WallCommands,
            CommandDictionary.Wall.GetWallDevice,
            deviceResponse =>
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine($"Kind: {deviceResponse.Kind}");
                if (deviceResponse.DeviceProcessors?.Any() == true)
                {
                    foreach (var devProc in deviceResponse.DeviceProcessors)
                    {
                        sb.AppendLine($"Id: {devProc.Id}, RefNumber: {devProc.RefNumber}");
                    }
                }

                if (deviceResponse.DeviceDisplays?.Any() == true)
                    if (deviceResponse.DeviceDisplays.Count != 0)
                    {
                        foreach (var devDisp in deviceResponse.DeviceDisplays)
                        {
                            sb.AppendLine($"Id: {devDisp.Id}");
                            foreach (var dispPosition in devDisp.DisplayValues)
                            {
                                sb.AppendLine($"Column: {dispPosition.Column}, Row: {dispPosition.Row}");
                            }
                        }
                    }

                return sb.ToString();
            });
        return response;
    }
}