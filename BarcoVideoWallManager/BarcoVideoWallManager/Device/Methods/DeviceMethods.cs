using System.Linq;

namespace BarcoVideoWallManager;

public partial class Barco
{
    //TODO: Write test and summary for GetDeviceTemperature
    public async Task<GetDeviceTemperatureResponse?> GetDeviceTemperatureAsync(string deviceId)
    {
        var response = await SendGetRequestAsync<GetDeviceTemperatureResponse, CommandDictionary.Device>(
            _c.DeviceCommands, CommandDictionary.Device.GetDeviceTemperature,
            temperatureResponse => $"Kind: {temperatureResponse.Kind}, " + $"Id: {temperatureResponse.Id}," +
                                   $"Ref#: {temperatureResponse.RefNumber}" +
                                   $"Position: (Row:{temperatureResponse.Position?.Row}, Column:{temperatureResponse.Position?.Column}), " +
                                   $"Temperature: {string.Join(", ", temperatureResponse.Temperatures!.Select(kv => $"{kv.Key}={kv.Value:0.##}\u00b0C"))}",
            deviceId);
        return response;
    }

    public async Task<GetDevicesTemperatureResponse?> GetDevicesTemperatureAsync()
    {
        var response = await SendGetRequestAsync<GetDevicesTemperatureResponse, CommandDictionary.Device>(
            _c.DeviceCommands, CommandDictionary.Device.GetDevicesTemperature, temperatureResponse =>
            {
                var procs = temperatureResponse.Processors is null
                    ? "(none)"
                    : string.Join(", ",
                        temperatureResponse.Processors.Select(
                            p => $"{p.Id}(ref#{p.RefNumber}): " +
                                 $"{string.Join("; ", p.Temperatures!.Select(kv => $"{kv.Key} ={kv.Value:0.##}°C"))}"
                        )
                    );
                var displays = temperatureResponse.Displays is null
                    ? "(none)"
                    : string.Join(", ",
                        temperatureResponse.Displays.Select(d =>
                            $"{d.Id}(r{d.Position?.Row}, c{d.Position?.Column}):" +
                            $"{string.Join("; ", d.Temperatures!.Select(kv => $"{kv.Key}={kv.Value:0.##}°C"))}"
                        )
                    );
                return $"Kind: {temperatureResponse.Kind} | Processors: {procs} | Displays: {displays}";

            });
        return response;
    }

}