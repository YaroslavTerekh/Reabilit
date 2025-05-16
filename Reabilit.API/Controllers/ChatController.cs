using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reabilit.BL.Behaviours.ChatMessages.GetChat;
using Reabilit.BL.Behaviours.ChatMessages.GetChatList;
using Reabilit.BL.Behaviours.ChatMessages.GetMySupportChat;
using Reabilit.BL.Behaviours.ChatMessages.SendMessage;
using Reabilit.BL.Behaviours.ChatMessages.SendMessageToSupport;
using Reabilit.Domain.Constants;

namespace Reabilit.API.Controllers;

[Authorize]
[Route("api/[controller]")]
[ApiController]
public class ChatController : BaseController
{
    private readonly ISender _sender;

    public ChatController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("get/{receiverId:guid}")]
    public async Task<IActionResult> GetChatAsync([FromRoute] Guid receiverId, CancellationToken cancellationToken)
        => Ok(await _sender.Send(new GetChatQuery(CurrentUserId, receiverId), cancellationToken));

    [HttpGet("support/get")]
    public async Task<IActionResult> GetSupportChatAsync(CancellationToken cancellationToken)
        => Ok(await _sender.Send(new GetMySupportChatQuery(CurrentUserId), cancellationToken));

    [HttpPost("send")]
    public async Task<IActionResult> SendMessageAsync([FromBody] SendMessageCommand command, CancellationToken cancellationToken)
    {
        command.CurrentUserId = CurrentUserId;

        await _sender.Send(command, cancellationToken);

        return Ok();
    }

    [Authorize(Policy = ApplicationPolicies.Patients)]
    [HttpPost("support/send")]
    public async Task<IActionResult> SendSupportMessageAsync([FromBody] SendMessageToSupportCommand command, CancellationToken cancellationToken)
    {
        command.CurrentUserId = CurrentUserId;

        await _sender.Send(command, cancellationToken);

        return Ok();
    }

    [HttpGet("list")]
    public async Task<IActionResult> GetChatListAsync(CancellationToken cancellationToken)
        => Ok(await _sender.Send(new GetChatListQuery(CurrentUserId), cancellationToken));
}
