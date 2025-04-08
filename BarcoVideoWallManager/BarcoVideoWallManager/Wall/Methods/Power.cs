namespace BarcoVideoWallManager;

public partial class Barco
{
    public async Task<WallPowerStateResponse?> GetWallPowerStateAsync()
    {
        var response =
            await SendGetRequestAsync<WallPowerStateResponse, CommandDictionary.Wall>(_c.WallCommands,
                CommandDictionary.Wall.GetPowerWallState, stateResponse => $"Power State: {stateResponse.Power}");
        return response;
    }

    /// <summary>
    /// Sets power state.
    /// </summary>
    /// <param name="newState">The desired power state. Accepted values are: On, Idle, Standby.</param>
    /// <returns>True is the operation succeeded, otherwise false.</returns>
    public async Task<bool> SetWallPowerStateAsync(WallPowerState newState)
    {
        var newStateString = newState.ToString().ToLower();
        var response = await SendPostRequestAsync(_c.WallCommands, _c.WallPayload,
            CommandDictionary.Wall.SetPowerWallState, newStateString);
        var success = await ProcessResponseAsync(response, CommandDictionary.Wall.SetPowerWallState.ToString());
        if (!success) return false;
        ProcessCookies(response);
        return true;
    }
}