using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reabilit.BL.Behaviours.DoctorSchedules.DeleteDoctorSchedule;
using Reabilit.BL.Behaviours.DoctorSchedules.GetDoctorFreeSlots;
using Reabilit.BL.Behaviours.UserDoctor.ModifyDoctorInfo;
using Reabilit.Domain.Constants;

namespace Reabilit.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class DoctorScheduleController : ControllerBase
{
    private readonly ISender _sender;

    public DoctorScheduleController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("slots/get")]
    public async Task<IActionResult> GetDoctorFreeSlotsAsync([FromBody] GetDoctorFreeSlotsQuery query, CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(query, cancellationToken));

    [HttpDelete("slots/delete/{dayOfWeek:int}")]
    public async Task<IActionResult> GetDoctorFreeSlotsAsync([FromRoute] DayOfWeek dayOfWeek, CancellationToken cancellationToken = default)
    {
        await _sender.Send(new DeleteDoctorScheduleCommand(dayOfWeek), cancellationToken);
        
        return Ok();
    }
}
