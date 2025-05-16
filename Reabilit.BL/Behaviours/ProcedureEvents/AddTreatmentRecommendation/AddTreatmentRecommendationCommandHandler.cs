using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.BL.Services.Abstractions;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ProcedureEvents.AddTreatmentRecommendation;

public class AddTreatmentRecommendationCommandHandler : IRequestHandler<AddTreatmentRecommendationCommand>
{
    private readonly ITreatmentNotificationService _treatmentNotificationService;
    private readonly DataContext _context;

    public AddTreatmentRecommendationCommandHandler(ITreatmentNotificationService treatmentNotificationService, DataContext context)
    {
        _context = context;
        _treatmentNotificationService = treatmentNotificationService;
    }

    public async Task Handle(AddTreatmentRecommendationCommand request, CancellationToken cancellationToken)
    {
        var procedureEvent = await _context.ProcedureEvents
            .Include(pe => pe.Patient)
            .FirstOrDefaultAsync(pe => pe.Id == request.ProcedureEventId, cancellationToken);

        if (procedureEvent is null)
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.ProcedureEvent));
        }

        var user = await _context.Users
            .FirstOrDefaultAsync(u => u.Id == request.ReceiverId, cancellationToken);

        if (user is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Default));
        }

        await _treatmentNotificationService.CreateAndSendTreatmentNotificationAsync
            (config =>
            {
                config.AppUserId = request.ReceiverId;
                config.ProcedureEventId = request.ProcedureEventId;
                config.Message = $"Вам надіслані рекомендації | {procedureEvent.Title}";
                config.Recommendations = request.Recommendation;
            }, cancellationToken);
    }
}
