namespace BarcoVideoWallManager;

public partial class Barco
{
    /// <summary>
    /// Gets the brightness, minimum, and the maximum values of the wall.
    /// All values responses in percentage.
    /// </summary>
    public async Task<WallBrightnessResponse?> GetWallBrightnessAsync()
    {
        var brightnessResponse =
            await SendGetRequestAsync<WallBrightnessResponse, CommandDictionary.Wall>(_c.WallCommands,
                CommandDictionary.Wall.GetWallBrightness,
                response => $"Kind: {response.Kind}\n" + $"Current brightness: {response.BrightnessPercentage}\n" +
                            $"Minimum: {response.MinimumPercentage}, Maximum: {response.MaximumPercentage}");

        return brightnessResponse;
    }
    /// <summary>
    /// Sets the brightness of the wall. Accepts values from 0-100.
    /// </summary>
    /// <param name="value"></param>
    /// <returns>Returns true upon successful response.</returns>

    public async Task<bool> SetWallBrightnessAsync(string value)
    {
        var response = await SendPostRequestAsync(_c.WallCommands, _c.WallPayload,
            CommandDictionary.Wall.SetWallBrightness, value);
        var success = await ProcessResponseAsync(response, CommandDictionary.Wall.SetWallBrightness.ToString());
        if (!success) return false;
        ProcessCookies(response);
        return true;
    }

    /// <summary>
    /// Gets the absolute, minimum, and maximum values of the wall.
    /// Brightness values in nits (cd/m^2)
    /// </summary>
    /// <returns></returns>
    public async Task<AbsoluteWallBrightnessResponse?> GetAbsoluteWallBrightnessAsync()
    {
        var brightnessResponse =
            await SendGetRequestAsync<AbsoluteWallBrightnessResponse, CommandDictionary.Wall>(_c.WallCommands,
                CommandDictionary.Wall.GetAbsoluteWallBrightness,
                response => $"Kind: {response.Kind}\n" + $"Current brightness: {response.BrightnessNits}\n" +
                            $"Minimum: {response.MinimumNits}, Maximum: {response.MaximumNits}");
        return brightnessResponse;
    }
    /// <summary>
    /// Set the brightness of the wall in nits
    /// </summary>
    /// <param name="value"></param>
    /// <returns>return true upon successful response.</returns>

    public async Task<bool> SetAbsoluteWallBrightnessAsync(string value)
    {
        var response = await SendPostRequestAsync(_c.WallCommands, _c.WallPayload,
            CommandDictionary.Wall.GetAbsoluteWallBrightness, value);
        var success = await ProcessResponseAsync(response, CommandDictionary.Wall.SetAbsoluteWallBrightness.ToString());
        if (!success) return false;
        ProcessCookies(response);
        return true;
    }
}