using MediatR;
using Reabilit.BL.Services.Abstractions;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ChatMessages.SendMessage;

public class SendMessageCommandHandler : IRequestHandler<SendMessageCommand>
{
    private readonly IChatService _chatService;
    private readonly IMessageNotificationsService _messageNotificationService;

    public SendMessageCommandHandler(IChatService chatService, IMessageNotificationsService messageNotificationsService)
    {
        _chatService = chatService;
        _messageNotificationService = messageNotificationsService;
    }

    public async Task Handle(SendMessageCommand request, CancellationToken cancellationToken)
    {
        var messageId = await _chatService.CreateAndSaveMessageAsync(request.CurrentUserId, request.ReceiverId, request.Message, cancellationToken);

        await _messageNotificationService.CreateAndSendMessageNotificationAsync(config =>
            {
                config.ReceiverId = request.ReceiverId;
                config.SenderId = request.CurrentUserId;
                config.ChatMessageId = messageId;
                config.Message = "У Вас нове повідомлення"; //ToDo: Add Messages
            }, cancellationToken);
    }
}
