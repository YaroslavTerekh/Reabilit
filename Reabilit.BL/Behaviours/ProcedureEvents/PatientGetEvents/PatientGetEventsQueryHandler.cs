using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ProcedureEvents.PatientGetEvents;

public class PatientGetEventsQueryHandler : IRequestHandler<PatientGetEventsQuery, List<ProcedureEvent>>
{
    private readonly DataContext _context;

    public PatientGetEventsQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<ProcedureEvent>> Handle(PatientGetEventsQuery request, CancellationToken cancellationToken)
    {
        return await _context.ProcedureEvents
            .Where(pe => pe.PatientId == request.PatientId)
            .ToListAsync(cancellationToken);
    }
}
