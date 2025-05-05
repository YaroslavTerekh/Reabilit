using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Notifications.DeleteEventNotifications;

public record DeleteEventNotificationsCommand(Guid Id) : IRequest;
