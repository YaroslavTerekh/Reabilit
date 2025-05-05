using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Services.Abstractions;

public interface INotificationService
{
    public Task DeleteNotificationAsync(Guid id, CancellationToken cancellationToken = default);    
}
