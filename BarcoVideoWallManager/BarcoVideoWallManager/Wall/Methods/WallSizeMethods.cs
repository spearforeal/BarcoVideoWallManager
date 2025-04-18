namespace BarcoVideoWallManager;

public partial class Barco
{
    /// <summary>
    /// Retrieves the overall dimensions of the video wall.
    /// Returns true if call succeeds and valid response is received.
    /// </summary>
    /// <returns></returns>
    public async Task<WallSizeResponse?> GetWallSizeAsync()
    {
        var response = await SendGetRequestAsync<WallSizeResponse, CommandDictionary.Wall>(_c.WallCommands,
            CommandDictionary.Wall.GetWallSize,
            sizeResponse => $"Kind: {sizeResponse.Kind}, Width: {sizeResponse.Width}, Height: {sizeResponse.Height}");
        return response;
    }
}