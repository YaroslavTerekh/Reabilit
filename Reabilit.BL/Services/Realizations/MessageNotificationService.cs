using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Reabilit.BL.Services.Abstractions;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using Reabilit.Domain.Entities;
using Reabilit.Domain.SignalrHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Services.Realizations;

public class MessageNotificationService : IMessageNotificationsService
{
    private readonly DataContext _context;
    private readonly IHubContext<NotificationsHub> _hubContext;

    public MessageNotificationService(DataContext context, IHubContext<NotificationsHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }

    public async Task CreateAndSendMessageNotificationAsync(Action<MessageNotificationConfiguration> configure, CancellationToken cancellationToken = default)
    {
        var configuration = new MessageNotificationConfiguration { Message = "Default implementation" };
        configure(configuration);

        if (!await _context.Users.AnyAsync(u => u.Id == configuration.SenderId, cancellationToken))
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.User));
        }

        if (!await _context.ChatMessages.AnyAsync(pe => pe.Id == configuration.ChatMessageId, cancellationToken))
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.ProcedureEvent));
        }

        var messageNotification = new MessageNotification
        {
            IsRead = false,
            Message = configuration.Message,
            AppUserId = configuration.ReceiverId,
            ChatMessageId = configuration.ChatMessageId,
        };

        try
        {
            await _context.MessageNotification.AddAsync(messageNotification, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);


            var chatMessage = await _context.ChatMessages
                .Include(cm => cm.Receiver)
                .Select(cm => new MessageDTO
                {
                    Id = cm.Id,
                    SenderId = cm.SenderId,
                    ReceiverId = cm.ReceiverId,
                    Receiver = cm.Receiver,
                    MessageText = cm.MessageText,
                    IsRead = cm.IsRead,
                    IsSender = false,
                    SentAt = cm.CreatedDate
                })
                .FirstOrDefaultAsync(cm => cm.Id == configuration.ChatMessageId, cancellationToken);

            await _hubContext.Clients.User(configuration.ReceiverId.ToString()).SendAsync(nameof(CreateAndSendMessageNotificationAsync), chatMessage);
        }
        catch { } // ToDo: Create block for catching
    }

    public Task DeleteNotificationAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
