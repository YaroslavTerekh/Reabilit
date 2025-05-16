using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Notifications.GetMyMessagesNotifications;

public class GetMyMessagesNotificationsQueryHandler : IRequestHandler<GetMyMessagesNotificationsQuery, List<MessageNotificationDTO>>
{
    private readonly DataContext _context;

    public GetMyMessagesNotificationsQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<MessageNotificationDTO>> Handle(GetMyMessagesNotificationsQuery request, CancellationToken cancellationToken)
    {
        if (!await _context.Users.AnyAsync(u => u.Id == request.CurrentUserId, cancellationToken))
        {
            throw new AuthException(StatusCodes.Status401Unauthorized, ErrorMessages.Unauthorized401);
        }

        return await _context.MessageNotification
            .Where(cmn => cmn.AppUserId == request.CurrentUserId)
            .OrderByDescending(cmn => cmn.CreatedDate)
            .Include(cmn => cmn.ChatMessage)
            .Select(cmn => new MessageNotificationDTO
            {
                Id = cmn.Id,
                Message = cmn.Message,
                ChatMessage = cmn.ChatMessage,
                IsRead = cmn.IsRead,
                ChatMessageId = cmn.ChatMessageId
            })
            .ToListAsync(cancellationToken);
    }
}
