using System.Net;
using System.Net.Http.Headers;
using System.Security.AccessControl;
using System.Text;
using System.Text.Json;

namespace BarcoVideoWallManager;

public class Barco
{

    public string IpAddress { get; set; }
    private string Psk { get; set; }
    public bool Debug { get; set; }
    private static HttpClient? _httpClient;
    private readonly CommandDictionary _commands = new();
    private string Sid { get; set; }







    public Barco(string ipAddress, string psk, bool debug)
    {
        IpAddress = ipAddress;
        Psk = psk;
        var handler = new HttpClientHandler { CookieContainer = new CookieContainer() };
        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri($"https://{ipAddress}/api/v1"),
            Timeout = TimeSpan.FromSeconds(30)

        };
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    //TODO: Ideally, this method should not be this busy. Reduce. Use generics when possible
    public async Task<bool> AuthenticateAsync()
    {

        //TODO: Have this called from enum/dict eventually
        var authPayload = new
        {
            type = "REST",
            key = Psk
        };
        var cmd = _commands.GeneralCommands[CommandDictionary.General.Authenticate];
            
        var jsonPayload = JsonSerializer.Serialize(authPayload);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var response = await _httpClient?.PostAsync(cmd, content)!;
        if (response.IsSuccessStatusCode)
        {
            if (response.Headers.TryGetValues("Set-Cookie", out var cookieValues))
            {
                foreach (var cookieValue in cookieValues)
                {
                    Sid = cookieValue;
                    Console.WriteLine("Received Set-Cookie header: " + Sid);
                }
            }

            if (!Debug) return true;
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Authentication successful: " + responseBody);

            return true; 
        }

        var errorBody = await response.Content.ReadAsStringAsync();
        await Console.Error.WriteLineAsync($"Authentication failed with status {response.StatusCode}: {errorBody}");
        return false;


    }





}
public class CommandDictionary
{
    public enum General
    {
        Authenticate,
        GetVwMVersion,
        GetApiVersion,
    }

    public enum Wall
    {
        GetWallBrightness,
        SetWallBrightness,
        GetAbsoluteWallBrightness, 
        SetAbsoluteWallBrightness,
        GetPowerWallState,
        SetPowerWallState,
        GetWallPresets,
        SetWallPreset,
        GetWallTemperature,
        GetWallAlert,
        GetWallSize,
        GetWallDevice,
        GetWallHealth,
        GetWallOsd,
        SetWallOsd,
        GetWallName,
    }

    public enum Device
    {
        GetDeviceTemperature,
        GetDevicesTemperature,
        GetDeviceFanSpeed,
        GetDevicesFanSpeed,
        GetDeviceRuntime,
        GetDevicesRuntime,
        GetDeviceHealth,
        GetDevicesHealth,
        GetDeviceAlert,
        GetDevicesAlert,
        GetDevicesPowerState
    }

    public Dictionary<General, string> GeneralCommands { get;  } = new()
    {
        { General.Authenticate, "auth/key" },
        { General.GetVwMVersion, "something" },
        { General.GetApiVersion, "Something" }

    };

    public Dictionary<Wall, string> WallCommands { get; } = new()
    {
        { Wall.GetWallBrightness, "Something" },
        { Wall.SetWallBrightness, "Something" },
        { Wall.GetAbsoluteWallBrightness, "Something" },
        { Wall.SetAbsoluteWallBrightness, "Something" },
        { Wall.GetPowerWallState, "Something" },
        { Wall.SetPowerWallState, "Something" },
        { Wall.GetWallPresets, "Something" },
        { Wall.SetWallPreset, "Something" },
        { Wall.GetWallTemperature, "Something" },
        { Wall.GetWallAlert, "Something" },
        { Wall.GetWallSize, "Something" },
        { Wall.GetWallDevice, "Something" },
        { Wall.GetWallHealth, "Something" },
        { Wall.GetWallOsd, "Something" },
        { Wall.SetWallOsd, "Something" },
        { Wall.GetWallName, "Something" },

    };

    public Dictionary<Device, string> DeviceCommands { get; } = new()
    {
        { Device.GetDeviceTemperature, "Something" },
        { Device.GetDevicesTemperature, "Something" },
        { Device.GetDeviceFanSpeed, "Something" },
        { Device.GetDevicesFanSpeed, "Something" },
        { Device.GetDeviceRuntime, "Something" },
        { Device.GetDevicesRuntime, "Something" },
        { Device.GetDeviceHealth, "Something" },
        { Device.GetDevicesHealth, "Something" },
        { Device.GetDeviceAlert, "Something" },
        { Device.GetDevicesAlert, "Something" },
        { Device.GetDevicesPowerState, "Something" },

    };

}