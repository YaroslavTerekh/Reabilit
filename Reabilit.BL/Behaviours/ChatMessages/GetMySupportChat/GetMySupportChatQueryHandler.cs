using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ChatMessages.GetMySupportChat;

public class GetMySupportChatQueryHandler : IRequestHandler<GetMySupportChatQuery, List<MessageDTO>>
{
    private readonly DataContext _context;
    private readonly UserManager<AppUser> _userManager;

    public GetMySupportChatQueryHandler(DataContext context, UserManager<AppUser> userManager)
    {
        _context = context;
        _userManager = userManager;
    }

    public async Task<List<MessageDTO>> Handle(GetMySupportChatQuery request, CancellationToken cancellationToken)
    {
        var supports = await _userManager.GetUsersInRoleAsync(ApplicationRoles.RoleSupport);

        if (!await _context.Users.AnyAsync(u => u.Id == request.CurrentUserId, cancellationToken))
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Default));
        }

        return await _context.ChatMessages
            .Include(m => m.Receiver)
            .Where(m =>
                (m.SenderId == request.CurrentUserId && m.ReceiverId == supports.First().Id) ||
                (m.SenderId == supports.First().Id && m.ReceiverId == request.CurrentUserId))
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
            .ToListAsync(cancellationToken); ;
    }
}
