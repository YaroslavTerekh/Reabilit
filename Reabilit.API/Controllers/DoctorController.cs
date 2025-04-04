using MediatR;
using Microsoft.AspNetCore.Mvc;
using Reabilit.BL.Behaviours.UserDoctor.ModifyDoctorInfo;

namespace Reabilit.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class DoctorController : ControllerBase
{
    private readonly ISender _sender;

    public DoctorController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPut("info/modify")]
    public async Task<IActionResult> ModifyDoctorInfoAsync([FromBody] ModifyDoctorInfoQuery query, CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(query, cancellationToken));
}
