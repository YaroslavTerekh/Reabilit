using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ChatMessages.GetChat;

public class GetChatQueryHandler : IRequestHandler<GetChatQuery, List<MessageDTO>>
{
    private readonly DataContext _context;

    public GetChatQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<MessageDTO>> Handle(GetChatQuery request, CancellationToken cancellationToken)
    {
        if (!await _context.Users.AnyAsync(u => u.Id == request.CurrentUserId, cancellationToken))
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Default));
        }

        if (!await _context.Users.AnyAsync(u => u.Id == request.ReceiverId, cancellationToken))
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Default));
        }

        return await _context.ChatMessages
            .Include(m => m.Receiver)
            .Where(m =>
                (m.SenderId == request.CurrentUserId && m.ReceiverId == request.ReceiverId) ||
                (m.SenderId == request.ReceiverId && m.ReceiverId == request.CurrentUserId))
            .OrderBy(m => m.CreatedDate)
            .Select(m => new MessageDTO
            {
                Id = m.Id,
                MessageText = m.MessageText,
                SentAt = m.CreatedDate,
                IsRead = m.IsRead,
                IsSender = m.SenderId == request.CurrentUserId,
                Receiver = m.Receiver,
                ReceiverId = m.ReceiverId,
                SenderId = m.SenderId
            })
            .ToListAsync(cancellationToken);
    }
}
