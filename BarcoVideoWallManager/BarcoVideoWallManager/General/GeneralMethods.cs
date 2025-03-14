namespace BarcoVideoWallManager;

public partial class Barco
{
    public async Task<bool> AuthenticateAsync()
    {
        var response = await SendPostRequestAsync(_c.GeneralCommands, _c.GeneralPayload,
            CommandDictionary.General.Authenticate, Psk);
        var success = await ProcessResponseAsync(response, CommandDictionary.General.Authenticate.ToString());
        if (!success) return false;
        ProcessCookies(response);
        return true;
    }

    /// <summary>
    /// Gets software version of video wall manager.
    /// </summary>
    public async Task<bool> GetVwMVersionAsync()
    {
        var softwareVersionResponse =
            await SendGetRequestAsync<BarcoSoftwareVersionResponse, CommandDictionary.General>(
                _c.GeneralCommands,
                CommandDictionary.General.GetVwMVersion);
        if (softwareVersionResponse == null) return false;
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
            await SendGetRequestAsync<BarcoApiVersionResponse, CommandDictionary.General>(_c.GeneralCommands,
                CommandDictionary.General.GetApiVersion);
        if (apiVersionResponse == null) return false;
        if (Debug)
        {
            Console.WriteLine($"Kind: {apiVersionResponse.Kind}");
            Console.WriteLine($"Version: {apiVersionResponse.Version}");
        }

        return true;
    }
}