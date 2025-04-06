using System.ComponentModel;
using System.Net.Mime;

namespace BarcoVideoWallManager;

public partial class Barco
{
    /// <summary>
    /// Gets the brightness, minimum, and the maximum values of the wall.
    /// All values responses in percentage.
    /// </summary>
    public async Task<bool> GetWallBrightnessAsync()
    {
        var brightnessResponse =
            await SendGetRequestAsync<WallBrightnessResponse, CommandDictionary.Wall>(_c.WallCommands,
                CommandDictionary.Wall.GetWallBrightness, response => $"Kind: {response.Kind}\n" + $"Current brightness: {response.BrightnessPercentage}\n" + $"Minimum: {response.MinimumPercentage}, Maximum: {response.MaximumPercentage}");

        return brightnessResponse != null;
    }

    public async Task<bool> SetWallBrightnessAsync(string value)
    {
        
        var response = await SendPostRequestAsync(_c.WallCommands, _c.WallPayload, CommandDictionary.Wall.SetWallBrightness, value);
        var success = await ProcessResponseAsync(response, CommandDictionary.Wall.SetWallBrightness.ToString());
        if(!success)return false;
        ProcessCookies(response);
        return true;
    }
    //TODO: Test GetAbsoluteWallBrightness()
    /// <summary>
    /// Gets the absolute, minimum, and maximum values of the wall.
    /// Brightness values in nits (cd/m^2)
    /// </summary>
    /// <returns></returns>
    public async Task<bool> GetAbsoluteWallBrightnessAsync()
    {
        var brightnessResponse =
            await SendGetRequestAsync<AbsoluteWallBrightnessResponse, CommandDictionary.Wall>(_c.WallCommands,
                CommandDictionary.Wall.GetAbsoluteWallBrightness, response => $"Kind: {response.Kind}\n" + $"Current brightness: {response.BrightnessNits}\n" + $"Minimum: {response.MinimumNits}, Maximum: {response.MaximumNits}");
        return brightnessResponse != null;

    }
    //TODO: Test SetAbsoluteWallBrightness()
    public async Task<bool> SetAbsoluteWallBrightnessAsync(string value)
    {
        var response = await SendPostRequestAsync(_c.WallCommands, _c.WallPayload,
            CommandDictionary.Wall.GetAbsoluteWallBrightness, value);
        var success = await ProcessResponseAsync(response, CommandDictionary.Wall.SetAbsoluteWallBrightness.ToString());
        if (!success) return false;
        ProcessCookies(response);
        return true;
    }

    //TODO: Test GetWallPowerState()
    public async Task<bool> GetWallPowerStateAsync()
    {
        var response =
            await SendGetRequestAsync<WallPowerStateResponse, CommandDictionary.Wall>(_c.WallCommands,
                CommandDictionary.Wall.GetPowerWallState, stateResponse => $"Power State: {stateResponse.Power}" );
        return response != null;
    }
    //TODO: Test SetWallPowerStateAsync()
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
    //TODO: Test GetWallPresetAsync() 
    /// <summary>
    /// <para><b>Important:</b> This request executes immediately.</para>
    /// Gets list of wall presets.
    /// Preset details include: id, name, and its current active state.
    /// </summary>
    /// <returns></returns>

    public async Task<bool> GetWallPresetsAsync()
    {
        var response =
            await SendGetRequestAsync<WallPresetResponse, CommandDictionary.Wall>(_c.WallCommands,
                CommandDictionary.Wall.GetWallPresets, presetResponse =>
                {
                    var sb = new System.Text.StringBuilder();
                    sb.AppendLine($"Response Kind: {presetResponse.Kind}");
                    if (presetResponse.Presets.Count > 0)
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

        return response != null;
    }
    
    //TODO: Test SetWallPresetAsync
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

    public async Task<bool> GetWallTemperature()
    {

        var response =
            await SendGetRequestAsync<WallTemperatureResponse, CommandDictionary.Wall>(_c.WallCommands,
                CommandDictionary.Wall.GetWallTemperature, temperatureResponse =>
                {
                    var sb = new System.Text.StringBuilder();
                    sb.AppendLine($"Kind: {temperatureResponse.Kind}");
                    if (temperatureResponse.Processors.Count > 0)
                    {
                        foreach (var proc in temperatureResponse.Processors)
                        {
                            sb.AppendLine(
                                $"Processor {proc.RefNumber} (ID: {proc.Id}): Board = {proc.Temperatures.Board}, FPGA = {proc.Temperatures.Fpga}");

                        }
                    }
                    else
                    {
                        sb.AppendLine("No processor temperature returned");
                    }

                    if (temperatureResponse.Displays.Count > 0)
                    {
                        foreach (var disp in temperatureResponse.Displays)
                        {
                            sb.Append($"Display {disp.Id} at ({disp.Position.Column}, {disp.Position.Row})");
                            if (disp.Temperatures.Interface.HasValue)
                            {
                                sb.AppendLine(
                                    $"Interface = {disp.Temperatures.Interface}, Left = {disp.Temperatures.Left}, Main = {disp.Temperatures.Main}, Right = {disp.Temperatures.Right}");
                            }
                            else if (disp.Temperatures.Lcm.HasValue)
                            {
                                sb.AppendLine(
                                    $"LCM = {disp.Temperatures.Lcm}, InputBoard = {disp.Temperatures.InputBoard}");
                            }
                            else
                            {
                                sb.AppendLine("No temperature data available");
                            }
                        }
                    }
                    else
                    {
                        sb.AppendLine("No display temperature returned");
                        
                    }

                    return sb.ToString();
                });
        return response != null;
    }

    public async Task<bool> GetWallAlertAsync()
    {
        var response = await SendGetRequestAsync<WallAlertResponse, CommandDictionary.Wall>(_c.WallCommands,
            CommandDictionary.Wall.GetWallAlert,
            alertResponse =>
            {
                var sb = new System.Text.StringBuilder();
                

            });

    }
}