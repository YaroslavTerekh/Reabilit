using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reabilit.BL.Behaviours.DoctorSchedules.GetDoctorFreeSlots;
using Reabilit.BL.Behaviours.ProcedureEvents.DoctorGetEvents;
using Reabilit.BL.Behaviours.ProcedureEvents.PatientGetEvents;
using Reabilit.BL.Behaviours.UserDoctor.GetDoctor;
using Reabilit.BL.Behaviours.UserPatient.AddNewProcedureEvent;
using Reabilit.BL.Behaviours.UserPatient.CancelEvent;
using Reabilit.BL.Behaviours.UserPatient.GetMyAnalyzes;
using Reabilit.BL.Behaviours.UserPatient.GetMyDoctor;
using Reabilit.BL.Behaviours.UserPatient.GetMyDoctorFreeSlots;
using Reabilit.BL.Behaviours.UserPatient.GetMyEvents;
using Reabilit.BL.Behaviours.UserPatient.GetPatient;
using Reabilit.BL.Behaviours.UserPatient.GetPatients;
using Reabilit.BL.Behaviours.UserPatient.ModifyPatientInfo;
using Reabilit.BL.Behaviours.UserPatient.ToggleAccount;
using Reabilit.Domain.Constants;

namespace Reabilit.API.Controllers;

[Authorize(Policy = ApplicationPolicies.Patients)]
[Route("api/[controller]")]
[ApiController]
public class PatientController : BaseController
{
    private readonly ISender _sender;

    public PatientController(ISender sender)
    {
        _sender = sender;
    }

    [HttpPost("info/modify")]
    public async Task<IActionResult> ModifyPatientInfoAsync([FromBody] ModifyPatientInfoQuery query, CancellationToken cancellationToken = default)
    {
        query.CurrentUserId = CurrentUserId;

        return Ok(await _sender.Send(query, cancellationToken));
    }

    [HttpPut("info/account/toggle")]
    public async Task<IActionResult> ToggleAccountAsync([FromBody] ToggleAccountCommand command, CancellationToken cancellationToken = default)
    {
        await _sender.Send(command, cancellationToken);

        return Ok();
    }

    [HttpPost("event/get")]
    public async Task<IActionResult> GetEventsAsync([FromBody] PatientGetEventsQuery query, CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(query, cancellationToken));

    [HttpPost("patients/get")]
    public async Task<IActionResult> GetPatientsAsync([FromBody] GetPatientsQuery query, CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(query, cancellationToken));

    [HttpPost("get")]
    public async Task<IActionResult> GetPatientAsync([FromBody] GetPatientQuery query, CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(query, cancellationToken));

    [HttpGet("doctor/get")]
    public async Task<IActionResult> GetDoctorAsync(CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(new GetMyDoctorQuery(CurrentUserId), cancellationToken));

    [HttpGet("doctor/slots/get")]
    public async Task<IActionResult> GetDoctorFreeSlotsAsync(CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(new GetMyDoctorFreeSlotsQuery(CurrentUserId), cancellationToken));

    [HttpPost("doctor/slots/reserve")]
    public async Task<IActionResult> ReserveSlotAsync([FromBody] AddNewProcedureEventCommand query, CancellationToken cancellationToken = default)
    {
        query.CurrentUserId = CurrentUserId; 
        await _sender.Send(query, cancellationToken);

        return Ok();
    }

    [HttpGet("events/get")]
    public async Task<IActionResult> GetMyEventsAsync(CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(new GetMyEventsQuery(CurrentUserId), cancellationToken));

    [HttpPost("events/cancel")]
    public async Task<IActionResult> CancelEventAsync([FromBody] CancelEventCommand command, CancellationToken cancellationToken = default)
    {
        command.CurrentUserId = CurrentUserId;
        await _sender.Send(command, cancellationToken);

        return Ok();
    }

    [HttpGet("analyzes/get")]
    public async Task<IActionResult> GetAnalyzesAsync(CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(new GetMyAnalyzesQuery(CurrentUserId), cancellationToken));
}
