using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using Amazon.Lambda.Core;
using System.Text.Json;

public class CanvasCreateAccountInput
{
    public string? CanvasApiBaseUrl { get; set; }
    public required string CanvasApiToken { get; set; }
    public required string AccountId { get; set; }
    public required string Name { get; set; }
    public required string ShortName { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
}

public class Function
{
    private static readonly HttpClient httpClient = new HttpClient();

    public async Task<object> FunctionHandler(CanvasCreateAccountInput input, ILambdaContext context)
    {
        var url = $"{input.CanvasApiBaseUrl}/api/v1/accounts/{input.AccountId}/users";
        httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", input.CanvasApiToken);

        var payload = new
        {
            user = new
            {
                name = input.Name,
                short_name = input.ShortName,
                terms_of_use = true
            },
            pseudonym = new
            {
                unique_id = input.Email,
                password = input.Password,
                send_confirmation = true
            },
            communication_channel = new
            {
                type = "email",
                address = input.Email
            }
        };

        var content = new StringContent(JsonSerializer.Serialize(payload), Encoding.UTF8, "application/json");

        var response = await httpClient.PostAsync(url, content);

        var responseContent = await response.Content.ReadAsStringAsync();

        return new
        {
            StatusCode = (int)response.StatusCode,
            ResponseBody = responseContent
        };
    }
}
