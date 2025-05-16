using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reabilit.BL.Behaviours.Notifications.DeleteEventNotifications;
using Reabilit.BL.Behaviours.Notifications.DeleteMessageNotifications;
using Reabilit.BL.Behaviours.Notifications.DeleteTreatmentNotification;
using Reabilit.BL.Behaviours.Notifications.GetMyMessagesNotifications;
using Reabilit.BL.Behaviours.Notifications.GetMyNotifications;
using Reabilit.BL.Behaviours.Notifications.GetMyTreatmentNotifications;
using Reabilit.BL.Behaviours.Notifications.GetNewMessageNotificationsCount;
using Reabilit.BL.Behaviours.Notifications.GetNewNotificationsCount;
using Reabilit.BL.Behaviours.Notifications.GetNewTreatmentNotifications;
using Reabilit.BL.Behaviours.Notifications.ReadAllNotifications;
using Reabilit.BL.Behaviours.Notifications.ReadEventNotification;
using Reabilit.BL.Behaviours.Notifications.ReadMessageNotification;
using Reabilit.BL.Behaviours.Notifications.ReadTreatmentNotification;

namespace Reabilit.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class NotificationController : BaseController
{
    private readonly ISender _sender;

    public NotificationController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("events")]
    public async Task<IActionResult> GetMyProcedureEventNotificationsAsync(CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(new GetMyEventNotificationsQuery(CurrentUserId), cancellationToken));

    [HttpGet("messages")]
    public async Task<IActionResult> GetMyMessagesNotificationsAsync(CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(new GetMyMessagesNotificationsQuery(CurrentUserId), cancellationToken));

    [HttpGet("treatment")]
    public async Task<IActionResult> GetMyTreatmentNotificationsAsync(CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(new GetMyTreatmentNotificationsQuery(CurrentUserId), cancellationToken));

    [HttpGet("events/new")]
    public async Task<IActionResult> GetMyProcedureEventNewCountNotificationsAsync(CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(new GetNewNotificationsCountQuery(CurrentUserId), cancellationToken));

    [HttpGet("messages/new")]
    public async Task<IActionResult> GetMyMessageNewCountNotificationsAsync(CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(new GetNewMessageNotificationsCountQuery(CurrentUserId), cancellationToken));

    [HttpGet("treatment/new")]
    public async Task<IActionResult> GetMyTreatmentNewCountNotificationsAsync(CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(new GetNewTreatmentNotificationsQuery(CurrentUserId), cancellationToken));

    [HttpPatch("read/{id:guid}")]
    public async Task<IActionResult> ReadNotificationAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        await _sender.Send(new ReadEventNotificationCommand(id), cancellationToken);

        return Ok();
    }

    [HttpPatch("message/read/{id:guid}")]
    public async Task<IActionResult> ReadMessageNotificationAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        await _sender.Send(new ReadMessageNotificationCommand(id), cancellationToken);

        return Ok();
    }

    [HttpPatch("treatment/read/{id:guid}")]
    public async Task<IActionResult> ReadTreatmentNotificationAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        await _sender.Send(new ReadTreatmentNotificationCommand(id), cancellationToken);

        return Ok();
    }

    [HttpPatch("read/all")]
    public async Task<IActionResult> ReadAllNotificationsAsync(CancellationToken cancellationToken = default)
    {
        await _sender.Send(new ReadAllNotificationsCommand(CurrentUserId), cancellationToken);

        return Ok();
    }

    [HttpDelete("events/delete/{id:guid}")]
    public async Task<IActionResult> DeleteProcedureEventNotificationsAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        await _sender.Send(new DeleteEventNotificationsCommand(id), cancellationToken);

        return Ok();
    }

    [HttpDelete("messages/delete/{id:guid}")]
    public async Task<IActionResult> DeleteMessageNotificationsAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        await _sender.Send(new DeleteMessageNotificationsCommand(id), cancellationToken);

        return Ok();
    }

    [HttpDelete("treatments/delete/{id:guid}")]
    public async Task<IActionResult> DeleteTreatmentNotificationsAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        await _sender.Send(new DeleteTreatmentNotificationCommand(id), cancellationToken);

        return Ok();
    }
}
