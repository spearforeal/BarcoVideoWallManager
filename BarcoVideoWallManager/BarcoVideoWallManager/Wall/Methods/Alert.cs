namespace BarcoVideoWallManager;

public partial class Barco
{
    public async Task<WallAlertResponse?> GetWallAlertAsync()
    {
        var response = await SendGetRequestAsync<WallAlertResponse, CommandDictionary.Wall>(_c.WallCommands,
            CommandDictionary.Wall.GetWallAlert,
            alertResponse =>
            {
                var sb = new System.Text.StringBuilder();
                sb.AppendLine($"Kind: {alertResponse.Kind}");
                if (alertResponse.Processors != null && alertResponse.Processors.Any())
                {
                    foreach (var proc in alertResponse.Processors)
                    {
                        sb.AppendLine($"Id: {proc.Id}, RefName: {proc.RefNumber}");
                        if (proc.AlertValues == null || !proc.AlertValues.Any()) continue;
                        foreach (var procAlert in proc.AlertValues)
                        {
                            sb.AppendLine($"Code: {procAlert.Code}, Type: {procAlert.Type}");
                        }
                    }
                }

                if (alertResponse.Displays != null && alertResponse.Displays.Any())
                    foreach (var disp in alertResponse.Displays)
                    {
                        sb.AppendLine($"Id: {disp.Id}");
                        if (disp.AlertPosition != null)
                            foreach (var alertPosition in disp.AlertPosition)
                            {
                                sb.AppendLine($"Column: {alertPosition.Column}, Row: {alertPosition.Row}");
                            }

                        if (disp.AlertValues == null) continue;
                        foreach (var alertValue in disp.AlertValues)
                        {
                            sb.AppendLine($"Code: {alertValue.Code}, Type: {alertValue.Type}");
                        }
                    }

                return sb.ToString();
            });
        return response;
    }
}