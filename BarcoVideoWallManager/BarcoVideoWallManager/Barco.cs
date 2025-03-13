using System.Diagnostics.Metrics;
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
    private readonly HttpClient? _httpClient;
    private readonly CommandDictionary _commands = new();
    private Session? _session;
    public Barco(string ipAddress, string psk, bool debug)
    {
        IpAddress = ipAddress;
        Psk = psk;
        var handler = new HttpClientHandler { CookieContainer = new CookieContainer() };
        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri($"https://{ipAddress}/api/"),
            Timeout = TimeSpan.FromSeconds(30)

        };
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
        var cookies = handler.CookieContainer.GetCookies(new Uri($"https://{ipAddress}/"));
        foreach (Cookie cookie in cookies)
        {
            Console.WriteLine($"Cookie: {cookie.Name} = {cookie.Value}");
        }
    }

    //TODO: Ideally, this method should not be this busy. Reduce. Use generics when possible
    public async Task<bool> AuthenticateAsync()
    {
        var response = await SendPostRequestAsync(_commands.GeneralCommands, _commands.GeneralPayload,
            CommandDictionary.General.Authenticate, Psk);
        if (response.IsSuccessStatusCode)
        {
            ProcessCookies(response);
            if (!Debug) return true;
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Authentication successful: " + responseBody);
            return true; 
        }
        var errorBody = await response.Content.ReadAsStringAsync();
        await Console.Error.WriteLineAsync($"Authentication failed with status {response.StatusCode}: {errorBody}");
        return false;
    }
    /// <summary>
    /// Gets software version of video wall manager.
    /// </summary>
    public async Task<bool> GetVwMVersionAsync()
    {
        var softwareVersionResponse =
            await SendGetRequestAsync<BarcoSoftwareVersionResponse, CommandDictionary.General>(_commands.GeneralCommands,
                CommandDictionary.General.GetVwMVersion);
        if (softwareVersionResponse== null) return false;
        if (Debug)
        {
            Console.WriteLine($"Kind: {softwareVersionResponse.Kind}");
            Console.WriteLine($"Version: {softwareVersionResponse.Version}");
        }

        return true;
    }
    /// <summary>
    /// Gets current version of the api.
    /// </summary>
    /// <returns></returns>

    public async Task<bool> GetApiVersionAsync()
    {
        var apiVersionResponse =
            await SendGetRequestAsync<BarcoApiVersionResponse, CommandDictionary.General>(_commands.GeneralCommands,
                CommandDictionary.General.GetApiVersion);
        if (apiVersionResponse == null) return false;
        if (Debug)
        {
            Console.WriteLine($"Kind: {apiVersionResponse.Kind}");
            Console.WriteLine($"Version: {apiVersionResponse.Version}");
        }
        return true;
    }

    public async Task<bool> GetWallBrightnessAsync()
    {
        var brightnessResponse =
            await SendGetRequestAsync<WallBrightnessResponse, CommandDictionary.Wall>(_commands.WallCommands,CommandDictionary.Wall.GetWallBrightness);
        if (brightnessResponse == null) return false;
        if (Debug)
        {
            Console.WriteLine($"Kind: {brightnessResponse.Kind}");
            Console.WriteLine($"Current brightness: {brightnessResponse.Brightness}");
            Console.WriteLine($"Minimum: {brightnessResponse.Minimum}, Maximum: {brightnessResponse.Maximum}");
        }

        return true;
    }

    private  async Task<HttpResponseMessage> SendPostRequestAsync<TEnum, TParam>(
        Dictionary<TEnum, string> commandDictionary, Dictionary<TEnum, Func<TParam, object>> payloadDictionary,
        TEnum command, TParam parameter) where TEnum : notnull
    {
        SessionCookieHeader();
        var endpoint = commandDictionary[command];
        var payload = BuildPayload(payloadDictionary, command, parameter);
        var content = new StringContent(payload, Encoding.UTF8, "application/json");
        return await _httpClient!.PostAsync(endpoint, content);
    }

    private  async Task<TResponse?> SendGetRequestAsync<TResponse, TEnum>(Dictionary<TEnum, string> commandDictionary,
        TEnum command, params object[] parameters) where TEnum : notnull
    {
        SessionCookieHeader();
        var endpointTemplate = commandDictionary[command];
        var endpoint = parameters.Length > 0 ? string.Format(endpointTemplate, parameters) : endpointTemplate;
        var response = await _httpClient?.GetAsync(endpoint)!;
        response.EnsureSuccessStatusCode();
        var responseBody = await response.Content.ReadAsStringAsync();
        return JsonSerializer.Deserialize<TResponse>(responseBody);

    }
    /// <summary>
    /// Ensures that the current session SID is added to the 'Cookie' header on every POST/GET request,
    /// except for the Authenticate request.
    /// If no valid session exists, no cookie is added.
    /// </summary>
    private void SessionCookieHeader()
    {
        if (_session == null || string.IsNullOrEmpty(_session.Sid)) return;
        _httpClient?.DefaultRequestHeaders.Remove("Cookie");
        _httpClient?.DefaultRequestHeaders.TryAddWithoutValidation("Cookie", _session.Sid);
    }

    private void ProcessCookies(HttpResponseMessage response)
    {
        if (!response.Headers.TryGetValues("Set-Cookie", out var cookieValues)) return;
        foreach (var cookieValue in cookieValues)
        {
            _session = new Session(cookieValue, DateTime.UtcNow.AddMinutes(30));
            Console.WriteLine("Received Set-Cookie header: " + _session.Sid);
        }
    }


    private static string BuildPayload<TEnum, TParam>(Dictionary<TEnum, Func<TParam, object>> payloadDictionary, TEnum command,
        TParam parameter) where TEnum : notnull
    {
        if (payloadDictionary.TryGetValue(command, out var payloadBuilder))
        {
            var payloadObj = payloadBuilder(parameter);
            return JsonSerializer.Serialize(payloadObj);
        }
        throw new ArgumentException($"No payload builder found for command: {command}", nameof(command));
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
        { General.Authenticate, "v1/auth/key" },
        { General.GetVwMVersion, "v1/version" },
        { General.GetApiVersion, "version" }

    };

    public Dictionary<General, Func<string, object>> GeneralPayload { get; } = new()
    {
        { General.Authenticate, (psk) => new { type = "REST", key = psk } }
    };

    public Dictionary<Wall, string> WallCommands { get; } = new()
    {
        { Wall.GetWallBrightness, "v1/wall/brightness" },
        { Wall.SetWallBrightness, "v1/wall/brightness" },
        { Wall.GetAbsoluteWallBrightness, "v1/wall/absoluteBrightness" },
        { Wall.SetAbsoluteWallBrightness, "v1/wall/absoluteBrightness" },
        { Wall.GetPowerWallState, "v1/wall/power" },
        { Wall.SetPowerWallState, "v1/wall/power" },
        { Wall.GetWallPresets, "v1/wall/presets" },
        { Wall.SetWallPreset, "v1/wall/preset" },
        { Wall.GetWallTemperature, "v1/wall/temperature" },
        { Wall.GetWallAlert, "v1/wall/alert" },
        { Wall.GetWallSize, "v1/wall/size" },
        { Wall.GetWallDevice, "v1/wall/device" },
        { Wall.GetWallHealth, "v1/wall/health" },
        { Wall.GetWallOsd, "v1/wall/osd" },
        { Wall.SetWallOsd, "v1/wall/osd" },
        { Wall.GetWallName, "v1/wall/name" },

    };

    public Dictionary<Wall, Func<string, object>> WallPayload { get; } = new()
    {
        { Wall.SetWallBrightness, (brightness) => new { brightness = brightness } },
        { Wall.SetAbsoluteWallBrightness, (brightness) => new { brightness = brightness } },
        { Wall.SetPowerWallState, (powerState) => new {powerState = powerState} },
        { Wall.SetWallPreset, (presetName) => new{presetName = presetName}},
        { Wall.SetWallOsd, (osdName) => new{osdName = osdName}},
    };

    public Dictionary<Device, string> DeviceCommands { get; } = new()
    {
        { Device.GetDeviceTemperature, "v1/device/{0}/temperature" },
        { Device.GetDevicesTemperature, "v1/device/temperature" },
        { Device.GetDeviceFanSpeed, "v1/device/{0}/fanSpeed" },
        { Device.GetDevicesFanSpeed, "v1/device/fanSpeed" },
        { Device.GetDeviceRuntime, "v1/device/{0}/runtime" },
        { Device.GetDevicesRuntime, "v1/device/runtime" },
        { Device.GetDeviceHealth, "v1/device/{0}/health" },
        { Device.GetDevicesHealth, "v1/device/health" },
        { Device.GetDeviceAlert, "v1/device/{0}/alert" },
        { Device.GetDevicesAlert, "v1/device/alert" },
        { Device.GetDevicesPowerState, "v1/device/power" },

    };


}
public class Session(string sid, DateTime expiresAt)
{
    public string Sid { get;  } = sid;
    private DateTime ExpiresAt { get;  } = expiresAt;
 
    public bool IsExpired() => DateTime.UtcNow >= ExpiresAt;
}