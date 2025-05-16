using MediatR;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Notifications.GetMyTreatmentNotifications;

public record GetMyTreatmentNotificationsQuery(Guid CurrentUserId) : IRequest<List<TreatmentNotification>>;