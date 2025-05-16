using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ChatMessages.GetChatList;

public class GetChatListQueryHandler : IRequestHandler<GetChatListQuery, List<ChatDTO>>
{
    private readonly DataContext _context;

    public GetChatListQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<ChatDTO>> Handle(GetChatListQuery request, CancellationToken cancellationToken)
    {
        var currentUserId = request.CurrentUserId;

        var messages = await _context.ChatMessages
                .Where(m => m.SenderId == currentUserId || m.ReceiverId == currentUserId)
                .GroupBy(m => m.SenderId == currentUserId ? m.ReceiverId : m.SenderId)
                .Select(g => g.Key)
                .Join(_context.Users,
                      receiverId => receiverId,
                      user => user.Id,
                      (receiverId, user) => new ChatDTO
                      {
                          ReceiverId = receiverId,
                          Receiver = user
                      })
                .ToListAsync(cancellationToken);


        return messages;

    }
}
