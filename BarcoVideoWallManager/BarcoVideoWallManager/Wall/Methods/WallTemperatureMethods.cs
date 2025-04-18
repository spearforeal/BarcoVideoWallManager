namespace BarcoVideoWallManager;

public partial class Barco
{
    public async Task<WallTemperatureResponse?> GetWallTemperature()
    {
        var response =
            await SendGetRequestAsync<WallTemperatureResponse, CommandDictionary.Wall>(_c.WallCommands,
                CommandDictionary.Wall.GetWallTemperature, temperatureResponse =>
                {
                    var sb = new System.Text.StringBuilder();
                    sb.AppendLine($"Kind: {temperatureResponse.Kind}");
                    if (temperatureResponse.Processors?.Any() == true)
                    {
                        foreach (var proc in temperatureResponse.Processors)
                        {
                            sb.AppendLine(
                                $"Processor {proc.RefNumber} (ID: {proc.Id}): Board = {proc.Temperatures?.Board}, FPGA = {proc.Temperatures?.Fpga}");
                        }
                    }
                    else
                    {
                        sb.AppendLine("No processor temperature returned");
                    }

                    if (temperatureResponse.Displays is { Count: > 0 })
                    {
                        foreach (var disp in temperatureResponse.Displays)
                        {
                            sb.Append($"Display {disp.Id} at ({disp.Position?.Column}, {disp.Position?.Row})");
                            if (disp.Temperatures is { Interface: not null })
                            {
                                sb.AppendLine(
                                    $"Interface = {disp.Temperatures.Interface}, Left = {disp.Temperatures.Left}, Main = {disp.Temperatures.Main}, Right = {disp.Temperatures.Right}");
                            }
                            else if (disp.Temperatures is { Lcm: not null })
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
        return response;
    }
}