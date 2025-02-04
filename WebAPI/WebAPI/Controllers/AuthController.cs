using System.Net;
using System.Text;
using System.Text.Json;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using WebAPI.Dtos;
using WebAPI.Options;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class AuthController(KeycloakService service, IOptions<KeycloakConfiguration> options) : ControllerBase
{
    [HttpGet("token")]
    public async Task<IActionResult> GetAccessToken()
    {
        var token = await service.GetAccessToken();
        return Ok(token);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDto request, CancellationToken cancellationToken = default)
    {
        string endpoint = $"{options.Value.HostName}/admin/realms/{options.Value.Realm}/users";
        object data = new
        {
            username = request.UserName,
            firstName = request.FirstName,
            lastName = request.LastName,
            email = request.Email,
            enabled = true,
            emailVerified = true,
            credentials = new List<object>
            {
                new
                {
                    type = "password",
                    temporary = false,
                    value = request.Password
                }
            }
        };

        var stringData = JsonSerializer.Serialize(data);
        var content = new StringContent(stringData, Encoding.UTF8, "application/json");
        
        HttpClient client = new();
        
        string token = await service.GetAccessToken();
        client.DefaultRequestHeaders.Add("Authorization", $"Bearer {token}");
        
        var message = await client.PostAsync(endpoint, content, cancellationToken);

        if (!message.IsSuccessStatusCode)
        {
            return message.StatusCode switch
            {
                HttpStatusCode.Conflict => Conflict(new { Message = "User already exists." }),
                HttpStatusCode.BadRequest => BadRequest(new { Message = "Invalid request data." }),
                HttpStatusCode.Unauthorized => Unauthorized(new { Message = "Invalid credentials or access token." }),
                HttpStatusCode.Forbidden => Forbid(),
                _ => StatusCode((int)message.StatusCode, new { Message = "An error occurred."})
            };
        }
        
        return Ok(new {Message = "User created successfully"});
    }
}