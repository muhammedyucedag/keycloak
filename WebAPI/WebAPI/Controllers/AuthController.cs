using Microsoft.AspNetCore.Mvc;
using WebAPI.Services;

namespace WebAPI.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AuthController(KeycloakService service) : ControllerBase
{
    [HttpGet("token")]
    public async Task<IActionResult> GetAccessToken(CancellationToken cancellationToken)
    {
        var token = await service.GetAccessToken(cancellationToken);
        return Ok(token);
    }
}