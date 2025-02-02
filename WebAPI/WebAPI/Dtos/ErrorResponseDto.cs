using System.Text.Json.Serialization;

namespace WebAPI.Dtos;

public sealed class ErrorResponseDto
{
    [JsonPropertyName("error")]
    public string Error { get; set; } = null!;
    
    [JsonPropertyName("error_description")]
    public string ErrorDescription { get; set; } = null!;
}

public sealed class BadRequestErrorResponseDto
{
    [JsonPropertyName("error")]
    public string Field { get; set; } = null!;
    
    [JsonPropertyName("error_message")]
    public string ErrorMessage { get; set; } = null!;
}