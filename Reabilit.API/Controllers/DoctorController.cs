using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Reabilit.BL.Behaviours.Analyzes.AddAnalyze;
using Reabilit.BL.Behaviours.Analyzes.DeleteAnalyze;
using Reabilit.BL.Behaviours.DoctorSchedules.AddDoctorSchedule;
using Reabilit.BL.Behaviours.ProcedureEvents.AddProcedureEventResult;
using Reabilit.BL.Behaviours.ProcedureEvents.AddTreatmentRecommendation;
using Reabilit.BL.Behaviours.ProcedureEvents.CreateEvent;
using Reabilit.BL.Behaviours.ProcedureEvents.DoctorGetEvents;
using Reabilit.BL.Behaviours.ProcedureEvents.EditEvent;
using Reabilit.BL.Behaviours.UserDoctor.GetDoctor;
using Reabilit.BL.Behaviours.UserDoctor.GetDoctors;
using Reabilit.BL.Behaviours.UserDoctor.GetMyEvents;
using Reabilit.BL.Behaviours.UserDoctor.GetMyInfo;
using Reabilit.BL.Behaviours.UserDoctor.GetMyPatients;
using Reabilit.BL.Behaviours.UserDoctor.GetMyTodaysEvents;
using Reabilit.BL.Behaviours.UserDoctor.ModifyDoctorInfo;
using Reabilit.Domain.Constants;

namespace Reabilit.API.Controllers;

[Authorize(Policy = ApplicationPolicies.Doctors)]
[Route("api/[controller]")]
[ApiController]
public class DoctorController : BaseController
{
    private readonly ISender _sender;

    public DoctorController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPut("info/modify")]
    public async Task<IActionResult> ModifyDoctorInfoAsync([FromBody] ModifyDoctorInfoQuery query, CancellationToken cancellationToken = default)
    {
        query.CurrentUserId = CurrentUserId;
        
        return Ok(await _sender.Send(query, cancellationToken));
    }

    [HttpPost("analyzes/add")]
    public async Task<IActionResult> AddAnalyzeAsync([FromForm] AddAnalyzeCommand command, CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(command, cancellationToken));

    [HttpPost("analyzes/{id:guid}/delete")]
    public async Task<IActionResult> DeleteAnalyzeAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(new DeleteAnalyzeCommand(id), cancellationToken));

    [HttpPost("event/add")]
    public async Task<IActionResult> AddEventAsync([FromBody] CreateEventCommand command, CancellationToken cancellationToken = default)
    {
        await _sender.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPost("schedule/add")]
    public async Task<IActionResult> AddScheduleAsync([FromBody] AddDoctorScheduleCommand command, CancellationToken cancellationToken = default)
    {
        await _sender.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPost("event/edit")]
    public async Task<IActionResult> EditEventAsync([FromBody] EditEventCommand command, CancellationToken cancellationToken = default)
    {
        await _sender.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPost("event/get")]
    public async Task<IActionResult> GetEventsAsync([FromBody] DoctorGetEventsQuery query, CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(query, cancellationToken));

    [HttpPost("doctor/get")]
    public async Task<IActionResult> GetDoctorAsync([FromBody] GetDoctorQuery query, CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(query, cancellationToken));

    [HttpPost("doctors/get")]
    public async Task<IActionResult> GetDoctorsAsync([FromBody] GetDoctorsQuery query, CancellationToken cancellationToken = default)
    => Ok(await _sender.Send(query, cancellationToken));

    [HttpGet("my-patients/get")]
    public async Task<IActionResult> GetMyPatientsAsync(CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(new GetMyPatientsQuery(CurrentUserId), cancellationToken));

    [HttpGet("my-events/get")]
    public async Task<IActionResult> GetMyEventsAsync(CancellationToken cancellationToken = default)
    => Ok(await _sender.Send(new GetMyEventsQuery(CurrentUserId), cancellationToken));

    [HttpGet("info/get")]
    public async Task<IActionResult> GetMyInfoAsync(CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(new GetMyInfoQuery(CurrentUserId), cancellationToken));

    [HttpGet("events/today/get")]
    public async Task<IActionResult> GetMyTodayEventsAsync(CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(new GetMyTodaysEventsQuery(CurrentUserId), cancellationToken));

    [HttpPatch("events/result/change")]
    public async Task<IActionResult> ChangeEventResultAsync(AddProcedureEventResultCommand command, CancellationToken cancellationToken = default)
    {
        await _sender.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPost("treatment/add")]
    public async Task<IActionResult> AddTreatmentAsync([FromBody] AddTreatmentRecommendationCommand command, CancellationToken cancellationToken = default)
    {
        await _sender.Send(command, cancellationToken);

        return Ok();
    }
}
