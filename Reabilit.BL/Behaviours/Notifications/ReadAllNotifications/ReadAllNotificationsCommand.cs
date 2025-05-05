using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Notifications.ReadAllNotifications;

public record ReadAllNotificationsCommand(Guid CurrentUserId) : IRequest;
