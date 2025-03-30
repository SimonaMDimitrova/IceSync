namespace IceSync.ExternalAPIs;

using Configurations;
using DTOs;

using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Text.Json;

public class UniversalLoaderClient : IUniversalLoaderClient
{
    private static string BASE_URL = ExternalAPIs.Instance.UniversalLoaderAPI;
    private static string MEDIA_TYPE = "application/json";
    private static Encoding ENCODING = Encoding.UTF8;

    private static string AUTHENTICATION_ENDPOINT = $"{BASE_URL}/v2/authenticate";
    private static string WORKFLOWS_ENDPOINT = $"{BASE_URL}​/workflows";
    private static string WORKFLOW_RUN_ENDPOINT = $"{BASE_URL}​/workflows​/{{0}}​/run?waitOutput=true&decodeOutputJsonString=true";

    private string _accessToken = string.Empty;
    private DateTime _tokenExpiry = DateTime.MinValue;

    private readonly IHttpClientFactory _httpClientFactory;

    public UniversalLoaderClient(IHttpClientFactory httpClientFactory)
    {
        this._httpClientFactory = httpClientFactory;
    }

    public async Task<List<WorkflowDTO>> GetWorkflowsAync()
    {
        var httpClient = _httpClientFactory.CreateClient();
        await this.AuthenticateAsync(httpClient);

        HttpResponseMessage response = await httpClient.GetAsync(WORKFLOWS_ENDPOINT);

        if (!response.IsSuccessStatusCode)
        {
            throw new ArgumentException($"Something when wrong!");
        }

        string rawResponse = await response.Content.ReadAsStringAsync();
        List<WorkflowDTO> serializedResponse = JsonSerializer.Deserialize<List<WorkflowDTO>>(rawResponse);

        return serializedResponse;
    }

    public async Task RunWorkflowAsync(int workflowId)
    {
        var httpClient = _httpClientFactory.CreateClient();
        await this.AuthenticateAsync(httpClient);

        string url = string.Format(WORKFLOW_RUN_ENDPOINT, workflowId);
        StringContent content = new("", ENCODING, MEDIA_TYPE);
        HttpResponseMessage response = await httpClient.PostAsync(url, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new ArgumentException($"Something when wrong!");
        }

        string rawResponse = await response.Content.ReadAsStringAsync();
    }

    private async Task AuthenticateAsync(HttpClient httpClient)
    {
        if (!string.IsNullOrEmpty(_accessToken) && _tokenExpiry > DateTime.UtcNow)
        {
            return;
        }

        UniversalLoaderCredentials universalLoaderCredentials = UniversalLoaderCredentials.Instance;
        var credentials = new ApiCredentialsDTO()
        {
            CompanyId = universalLoaderCredentials.CompanyId,
            UserId = universalLoaderCredentials.UserId,
            UserSecret = universalLoaderCredentials.UserSecret,
        };

        string rawRequest = JsonSerializer.Serialize(credentials);

        StringContent content = new(rawRequest, ENCODING, MEDIA_TYPE);
        HttpResponseMessage response = await httpClient.PostAsync(AUTHENTICATION_ENDPOINT, content);

        if (!response.IsSuccessStatusCode)
        {
            throw new ArgumentException($"Something when wrong!");
        }

        string rawResponse = await response.Content.ReadAsStringAsync();
        AccessTokenDTO serializedResponse = JsonSerializer.Deserialize<AccessTokenDTO>(rawResponse);

        this._accessToken = serializedResponse.AccessToken;

        int tokenExpiryOffset = 5;
        this._tokenExpiry = DateTime.UtcNow.AddSeconds(serializedResponse.ExpiresIn - tokenExpiryOffset);

        httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {this._accessToken}");
    }
}
