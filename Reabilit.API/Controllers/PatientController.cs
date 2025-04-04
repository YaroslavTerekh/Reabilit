using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reabilit.BL.Behaviours.UserPatient.ModifyPatientInfo;
using Reabilit.BL.Behaviours.UserPatient.ToggleAccount;

namespace Reabilit.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class PatientController : ControllerBase
{
    private readonly ISender _sender;

    public PatientController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPut("info/modify")]
    public async Task<IActionResult> ModifyPatientInfoAsync([FromBody] ModifyPatientInfoQuery query, CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(query, cancellationToken));

    [HttpPut("info/account/toggle")]
    public async Task<IActionResult> ToggleAccountAsync([FromBody] ToggleAccountCommand command, CancellationToken cancellationToken = default)
    {
        await _sender.Send(command, cancellationToken);

        return Ok();
    }
}
