namespace BarcoVideoWallManager;

public partial class Barco
{
    /// <summary>
    /// Indicates the health status of the wall.
    /// </summary>
    /// <returns>Either: ok|warning|error</returns>
    public async Task<WallHealthResponse?> GetWallHealth()
    {
        var response = await SendGetRequestAsync<WallHealthResponse, CommandDictionary.Wall>(_c.WallCommands,
            CommandDictionary.Wall.GetWallHealth,
            healthResponse => $"Kind: {healthResponse.Kind}, Health: {healthResponse.Health}");
        return response;
    }
}