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
                CommandDictionary.General.GetVwMVersion, response => $"Kind: {response.Kind}/n" + $"Version: {response.Version}" );

        return softwareVersionResponse != null;

    }

    /// <summary>
    /// Gets current version of the api.
    /// </summary>
    /// <returns></returns>
    public async Task<bool> GetApiVersionAsync()
    {
        var apiVersionResponse =
            await SendGetRequestAsync<BarcoApiVersionResponse, CommandDictionary.General>(_c.GeneralCommands,
                CommandDictionary.General.GetApiVersion, response => $"Kind: {response.Kind}\n" + $"Version: {response.Version}");
        return apiVersionResponse != null;
    }
}