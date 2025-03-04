using System.Net;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;

namespace BarcoVideoWallManager;

public class Barco
{

    public string IpAddress { get; set; }
    public string Psk { get; set; }
    public bool Debug { get; set; }
    private static HttpClient? _httpClient;
    private string Sid { get; set; }

    private static readonly Dictionary<Endpoint, string> EndpointMap = new()
    {
        { Endpoint.AuthKey, "auth/key" },
        { Endpoint.WallBrightness, "wall/brightness" }

    };
    public enum Endpoint
    {
        AuthKey,
        WallBrightness
        
        
    }

    public Barco(string ipAddress, string psk, bool debug)
    {
        IpAddress = ipAddress;
        Psk = psk;
        var handler = new HttpClientHandler { CookieContainer = new CookieContainer() };
        _httpClient = new HttpClient(handler)
        {
            BaseAddress = new Uri($"https://{ipAddress}/api/v1"),
            Timeout = TimeSpan.FromSeconds(30)

        };
        _httpClient.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
    }

    //TODO: Ideally, this method should not be this busy. Reduce. Use generics when possible
    public async Task<bool> AuthenticateAsync()
    {

        //TODO: Have this called from enum/dict eventually
        var authPayload = new
        {
            type = "REST",
            key = Psk
        };
        var jsonPayload = JsonSerializer.Serialize(authPayload);
        var content = new StringContent(jsonPayload, Encoding.UTF8, "application/json");
        var response = await _httpClient?.PostAsync("v1/auth/key", content)!;
        if (response.IsSuccessStatusCode)
        {
            if (response.Headers.TryGetValues("Set-Cookie", out var cookieValues))
            {
                foreach (var cookieValue in cookieValues)
                {
                    Sid = cookieValue;
                    Console.WriteLine("Received Set-Cookie header: " + Sid);
                }
            }

            if (!Debug) return true;
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Authentication successful: " + responseBody);

            return true; 
        }

        var errorBody = await response.Content.ReadAsStringAsync();
        await Console.Error.WriteLineAsync($"Authentication failed with status {response.StatusCode}: {errorBody}");
        return false;


    }





}