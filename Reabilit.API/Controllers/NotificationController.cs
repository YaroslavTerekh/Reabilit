using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reabilit.BL.Behaviours.Notifications.DeleteEventNotifications;
using Reabilit.BL.Behaviours.Notifications.GetMyNotifications;
using Reabilit.BL.Behaviours.Notifications.GetNewNotificationsCount;
using Reabilit.BL.Behaviours.Notifications.ReadAllNotifications;
using Reabilit.BL.Behaviours.Notifications.ReadEventNotification;

namespace Reabilit.API.Controllers;

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

    [HttpGet("events/new")]
    public async Task<IActionResult> GetMyProcedureEventNewCountNotificationsAsync(CancellationToken cancellationToken = default)
    => Ok(await _sender.Send(new GetNewNotificationsCountQuery(CurrentUserId), cancellationToken));

    [HttpPatch("read/{id:guid}")]
    public async Task<IActionResult> ReadNotificationAsync([FromRoute] Guid id, CancellationToken cancellationToken = default)
    {
        await _sender.Send(new ReadEventNotificationCommand(id), cancellationToken);

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
}
