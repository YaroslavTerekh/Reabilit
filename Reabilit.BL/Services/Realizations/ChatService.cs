using Reabilit.BL.Services.Abstractions;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Services.Realizations;

public class ChatService : IChatService
{
    private readonly DataContext _context;

    public ChatService(DataContext context)
    {
        _context = context;
    }

    public async Task<Guid> CreateAndSaveMessageAsync(Guid senderId, Guid receiverId, string text, CancellationToken cancellationToken )
    {
        var message = new ChatMessage
        {
            SenderId = senderId,
            ReceiverId = receiverId,
            MessageText = text
        };

        await _context.ChatMessages.AddAsync(message, cancellationToken);
        await _context.SaveChangesAsync(cancellationToken);

        return message.Id;
    }
}
