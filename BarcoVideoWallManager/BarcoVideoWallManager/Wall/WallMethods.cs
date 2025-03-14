namespace BarcoVideoWallManager;

public partial class Barco
{
    public async Task<bool> GetWallBrightnessAsync()
    {
        var brightnessResponse =
            await SendGetRequestAsync<WallBrightnessResponse, CommandDictionary.Wall>(_c.WallCommands,
                CommandDictionary.Wall.GetWallBrightness);
        if (brightnessResponse == null) return false;
        if (Debug)
        {
            Console.WriteLine($"Kind: {brightnessResponse.Kind}");
            Console.WriteLine($"Current brightness: {brightnessResponse.Brightness}");
            Console.WriteLine($"Minimum: {brightnessResponse.Minimum}, Maximum: {brightnessResponse.Maximum}");
        }

        return true;
    }

    public async Task<bool> SetWallBrightness(string value)
    {
        
        var response = await SendPostRequestAsync(_c.WallCommands, _c.WallPayload, CommandDictionary.Wall.SetWallBrightness, value);
        if (response.IsSuccessStatusCode)
        {
            ProcessCookies(response);
        }
        var errorBody = await response.Content.ReadAsStringAsync();
        await Console.Error.WriteLineAsync($"Set Brightness failed with status {response.StatusCode}: {errorBody}");
        return false;
    }
}