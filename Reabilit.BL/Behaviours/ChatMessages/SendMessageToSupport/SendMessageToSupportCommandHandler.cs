using MediatR;
using Microsoft.AspNetCore.Identity;
using Reabilit.BL.Services.Abstractions;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ChatMessages.SendMessageToSupport;

public class SendMessageToSupportCommandHandler : IRequestHandler<SendMessageToSupportCommand>
{
    private readonly UserManager<AppUser> _userManager;
    private readonly IChatService _chatService;
    private readonly IMessageNotificationsService _messageNotificationService;

    public SendMessageToSupportCommandHandler(UserManager<AppUser> userManager, IChatService chatService, IMessageNotificationsService messageNotificationService)
    {
        _userManager = userManager;
        _chatService = chatService;
        _messageNotificationService = messageNotificationService;
    }

    public async Task Handle(SendMessageToSupportCommand request, CancellationToken cancellationToken)
    {
        var supports = await _userManager.GetUsersInRoleAsync(ApplicationRoles.RoleSupport);

        var messageId = await _chatService.CreateAndSaveMessageAsync(request.CurrentUserId, supports.First().Id, request.Message, cancellationToken);

        await _messageNotificationService.CreateAndSendMessageNotificationAsync(config =>
        {
            config.ReceiverId = supports.First().Id;
            config.SenderId = request.CurrentUserId;
            config.ChatMessageId = messageId;
            config.Message = request.Theme;
        }, cancellationToken);
    }
}
