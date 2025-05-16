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

namespace Reabilit.BL.Behaviours.ProcedureEvents.AddProcedureEventResult;

public class AddProcedureEventResultCommandHandler : IRequestHandler<AddProcedureEventResultCommand>
{
    private readonly DataContext _context;
    private readonly ITreatmentNotificationService _treatmentNotificationService;

    public AddProcedureEventResultCommandHandler(DataContext context, ITreatmentNotificationService treatmentNotificationService)
    {
        _context = context;
        _treatmentNotificationService = treatmentNotificationService;
    }

    public async Task Handle(AddProcedureEventResultCommand request, CancellationToken cancellationToken)
    {
        var procedureEvent = await _context.ProcedureEvents
            .Include(pe => pe.Patient)
            .FirstOrDefaultAsync(pe => pe.Id == request.ProcedureEventId, cancellationToken);

        if(procedureEvent is null)
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.ProcedureEvent));
        }

        try
        {
            procedureEvent.Result = request.Result;
            await _context.SaveChangesAsync(cancellationToken);

            await _treatmentNotificationService.CreateAndSendTreatmentNotificationAsync
                (config =>
                {
                    config.AppUserId = procedureEvent.Patient!.AppUserId;
                    config.Recommendations = request.Result;
                    config.Message = $"Готовий результат обстеження від {procedureEvent.StartsOn.ToShortDateString()} | {procedureEvent.Title}";
                    config.ProcedureEventId = procedureEvent.Id;
                }, cancellationToken);
        }
        catch { } // ToDo: Catch logic
    }
}
