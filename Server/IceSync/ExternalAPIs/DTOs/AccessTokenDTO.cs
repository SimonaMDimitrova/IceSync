﻿namespace IceSync.ExternalAPIs.DTOs;

using System.Text.Json.Serialization;

public class AccessTokenDTO
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; }

    [JsonPropertyName("token_type")]
    public string TokenType { get; set; }

    [JsonPropertyName("expires_in")]
    public int ExpiresIn { get; set; }
}
