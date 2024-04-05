using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;
using SellingDreamsWebApi.Controllers;

namespace SellingDreamsWebApi;

public static class ConfigureLogging
{
    public static async Task<string> GetRequestBody(HttpRequest httpRequest)
    {
        string requestBody = string.Empty;

        if (
            httpRequest.Path.ToString().Contains(nameof(AuthController.Login))
            || httpRequest.Path.ToString().Contains(nameof(AuthController.Authenticate))
        )
        {
            requestBody = "[Redacted] Contains Sensitive Information. ";
        }
        else
        {
            using var reader = new HttpRequestStreamReader(httpRequest.Body, Encoding.UTF8);
            var payload = await reader.ReadToEndAsync();
            if (!string.IsNullOrEmpty(payload))
            {
                var json = JsonSerializer.Deserialize<object>(payload);
                requestBody = $"{JsonSerializer.Serialize(json)} ";
            }
        }

        return requestBody;
    }
}
