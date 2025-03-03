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
        Console.WriteLine(response);
        if (response.IsSuccessStatusCode)
        {
            if (!Debug) return true;
            var responseBody = await response.Content.ReadAsStringAsync();
            Console.WriteLine("Authentication successful: " + responseBody);

            return true; 
        }
        else
        {
            var errorBody = await response.Content.ReadAsStringAsync();
            await Console.Error.WriteLineAsync($"Authentication failed with status {response.StatusCode}: {errorBody}");
            return false;
        }


    }





}