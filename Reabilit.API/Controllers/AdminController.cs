using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reabilit.BL.Behaviours.Authentication.RegisterDoctor;
using Reabilit.BL.Behaviours.Authentication.RegisterPatient;
using Reabilit.BL.Behaviours.UserPatient.AttachToDoctor;
using Reabilit.Domain.Constants;

namespace Reabilit.API.Controllers;

[Authorize(Policy = ApplicationPolicies.Admins)]
[Route("api/[controller]")]
[ApiController]
public class AdminController : ControllerBase
{
    private readonly ISender _sender;

    public AdminController(ISender sender)
    {
        _sender = sender;
    }

    [AllowAnonymous]
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

    [HttpPost("patient/attach-doctor")]
    public async Task<IActionResult> AttachToDoctorAsync([FromBody] AttachToDoctorCommand command, CancellationToken cancellationToken = default)
    {
        await _sender.Send(command, cancellationToken);

        return Ok();
    }
}
