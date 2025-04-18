namespace BarcoVideoWallManager;

public partial class Barco
{
    /// <summary>
    /// Retrieves the current health status of the video wall.
    /// </summary>
    /// <returns>A <see cref="WallHealthResponse"/> containing the health status (ok, warning, or error), or null if the request fails.</returns>
    public async Task<WallHealthResponse?> GetWallHealth()
    {
        var response = await SendGetRequestAsync<WallHealthResponse, CommandDictionary.Wall>(_c.WallCommands,
            CommandDictionary.Wall.GetWallHealth,
            healthResponse => $"Kind: {healthResponse.Kind}, Health: {healthResponse.Health}");
        return response;
    }
}