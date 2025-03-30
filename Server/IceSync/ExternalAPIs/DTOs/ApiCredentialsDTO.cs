namespace IceSync.ExternalAPIs.DTOs;

using System.Text.Json.Serialization;

public class ApiCredentialsDTO
{
    [JsonPropertyName("apiCompanyId")]
    public string CompanyId { get; set; }

    [JsonPropertyName("apiUserId")]
    public string UserId { get; set; }

    [JsonPropertyName("apiUserSecret")]
    public string UserSecret { get; set; }
}
