using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reabilit.BL.Behaviours.Authentication.GetUserRole;
using Reabilit.BL.Behaviours.Authentication.Login;

namespace Reabilit.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AuthController : BaseController
{
    private readonly ISender _sender;

    public AuthController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("login")]
    public async Task<IActionResult> LoginAsync ([FromBody] LoginCommand command, CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(command, cancellationToken));

    [Authorize]
    [HttpGet("role/get")]
    public async Task<IActionResult> GetMyRoleAsync(CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(new GetUserRoleQuery(CurrentUserId), cancellationToken));
}
