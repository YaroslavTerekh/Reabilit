using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Notifications.DeleteTreatmentNotification;

public record DeleteTreatmentNotificationCommand(Guid Id) : IRequest;