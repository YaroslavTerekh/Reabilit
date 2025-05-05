using Reabilit.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Services.Abstractions;

public interface IEventNotificationService : INotificationService
{
    public Task CreateAndSendEventNotificationAsync(EventNotificationConfiguration configuration, CancellationToken cancellationToken = default);
}
