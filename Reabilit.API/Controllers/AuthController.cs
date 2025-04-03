using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reabilit.BL.Behaviours.Authentication.Login;

namespace Reabilit.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : ControllerBase
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync ([FromBody] LoginCommand command, CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(command, cancellationToken));
}
