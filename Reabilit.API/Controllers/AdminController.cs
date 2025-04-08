using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reabilit.BL.Behaviours.Authentication.RegisterDoctor;
using Reabilit.BL.Behaviours.Authentication.RegisterPatient;

namespace Reabilit.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly ISender _sender;

    public AdminController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("regiter/patient")]
    public async Task<IActionResult> RegisterPatientAsync([FromBody] RegisterPatientCommand command, CancellationToken cancellationToken = default)
    {
        await _sender.Send(command, cancellationToken);
        return Ok();
    }

    [HttpPost("regiter/doctor")]
    public async Task<IActionResult> RegisterDoctorAsync([FromBody] RegisterDoctorCommand command, CancellationToken cancellationToken = default)
    {
        await _sender.Send(command, cancellationToken);
        return Ok();
    }
}
