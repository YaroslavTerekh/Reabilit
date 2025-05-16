using Hangfire;
using Microsoft.EntityFrameworkCore;
using Reabilit.BL.Services.Abstractions;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Services.Realizations;

public class ProcedureEventJobsService : IProcedureEventJobsService
{
    private readonly DataContext _context;

    public ProcedureEventJobsService(DataContext context)
    {
        _context = context;
    }

    public async Task AddFinishProcedureEventJob(Guid eventId, CancellationToken cancellationToken)
    {
        var procedureEvent = await _context.ProcedureEvents
            .FirstOrDefaultAsync(pe => pe.Id == eventId, cancellationToken);

        if (procedureEvent is null)
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.ProcedureEvent));
        }

        var triggerTime = procedureEvent.StartsOn.AddMinutes(30);

        BackgroundJob.Schedule(() => FinishProcedureEvent(eventId), triggerTime);
    }

    public void FinishProcedureEvent(Guid eventId)
    {
        var procedureEvent =  _context.ProcedureEvents.FirstOrDefaultAsync(pe => pe.Id == eventId).GetAwaiter().GetResult();
        if (procedureEvent is null)
            return;

        procedureEvent.Status = ProcedureEventStatus.Finished;
        _context.SaveChangesAsync().GetAwaiter().GetResult();
    }

}
