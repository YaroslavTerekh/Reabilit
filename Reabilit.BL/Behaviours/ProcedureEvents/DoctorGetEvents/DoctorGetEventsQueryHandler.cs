using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ProcedureEvents.DoctorGetEvents;

public class DoctorGetEventsQueryHandler : IRequestHandler<DoctorGetEventsQuery, List<ProcedureEvent>>
{
    private readonly DataContext _context;

    public DoctorGetEventsQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<ProcedureEvent>> Handle(DoctorGetEventsQuery request, CancellationToken cancellationToken)
    {
        return await _context.ProcedureEvents
            .Where(pe => pe.DoctorId == request.DoctorId)
            .ToListAsync(cancellationToken);
    }
}
