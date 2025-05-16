using Reabilit.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Services.Abstractions;

public interface IMessageNotificationsService : INotificationService
{
    public Task CreateAndSendMessageNotificationAsync(Action<MessageNotificationConfiguration> configure, CancellationToken cancellationToken = default);
}
