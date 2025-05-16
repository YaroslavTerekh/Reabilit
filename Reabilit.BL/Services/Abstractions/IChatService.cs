using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Services.Abstractions;

public interface IChatService
{
    public Task<Guid> CreateAndSaveMessageAsync(Guid senderId, Guid receiverId, string text, CancellationToken cancellationToken);
}
