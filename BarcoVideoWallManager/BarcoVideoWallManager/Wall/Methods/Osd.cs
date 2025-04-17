namespace BarcoVideoWallManager
{
    public partial class Barco
    {
        public async Task<GetWallOsdResponse?> GetWallOsdAsync()
        {
            var response = await SendGetRequestAsync<GetWallOsdResponse, CommandDictionary.Wall>(_c.WallCommands,
                CommandDictionary.Wall.GetWallOsd,
                osdResponse => $"Kind: {osdResponse.Kind}, Osd Names: {osdResponse.Osd}");
            return response;
        }
        public async Task<bool> SetWallOsdAsync(string value)
        {
            var response = await SendPostRequestAsync(_c.WallCommands, _c.WallPayload,
                CommandDictionary.Wall.SetWallOsd, value);
            var success = await ProcessResponseAsync(response, CommandDictionary.Wall.SetWallOsd.ToString());
            if (!success) return false;
            ProcessCookies(response);
            return true;
        }
        public async Task<GetWallNameResponse?> GetWallNameAsync()
        {
            var response = await SendGetRequestAsync<GetWallNameResponse, CommandDictionary.Wall>(_c.WallCommands,
                CommandDictionary.Wall.GetWallName,
                nameResponse => $"Kind: {nameResponse.Kind}\n Name: {nameResponse.Name}");
            return response;
        }
    }
}