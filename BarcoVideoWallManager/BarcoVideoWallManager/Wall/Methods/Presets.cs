namespace BarcoVideoWallManager;

public partial class Barco
{
    /// <summary>
    /// <para><b>Important:</b> This request executes immediately.</para>
    /// Gets list of wall presets.
    /// Preset details include: id, name, and its current active state.
    /// </summary>
    /// <returns></returns>
    public async Task<WallPresetResponse?> GetWallPresetsAsync()
    {
        var response =
            await SendGetRequestAsync<WallPresetResponse, CommandDictionary.Wall>(_c.WallCommands,
                CommandDictionary.Wall.GetWallPresets, presetResponse =>
                {
                    var sb = new System.Text.StringBuilder();
                    sb.AppendLine($"Response Kind: {presetResponse.Kind}");
                    if (presetResponse.Presets?.Any() == true)
                    {
                        foreach (var preset in presetResponse.Presets)
                        {
                            sb.AppendLine($"Preset ID: {preset.Id}, Name: {preset.Name}, Active: {preset.Active}");
                        }
                    }
                    else
                    {
                        sb.AppendLine($"No presets were returned.");
                    }

                    return sb.ToString();
                });

        return response;
    }

    /// <summary>
    /// Sets wall to specified preset.
    /// </summary>
    /// <param name="presetName">Specify the name of an exisiting preset. Should be a valid name that exists in the system. Will raise an error if not valid. Name is case sensitive.</param>
    /// <returns></returns>
    public async Task<bool> SetWallPresetAsync(string presetName)
    {
        var response = await SendPostRequestAsync(_c.WallCommands, _c.WallPayload, CommandDictionary.Wall.SetWallPreset,
            presetName);
        var success = await ProcessResponseAsync(response, CommandDictionary.Wall.SetWallPreset.ToString());
        if (!success) return false;
        ProcessCookies(response);
        return true;
    }
}