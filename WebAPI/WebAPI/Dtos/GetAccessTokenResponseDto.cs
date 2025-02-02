using System.Text.Json.Serialization;

namespace WebAPI.Dtos;

public sealed class GetAccessTokenResponseDto
{
    [JsonPropertyName("access_token")]
    public string AccessToken { get; set; } = null!;
}