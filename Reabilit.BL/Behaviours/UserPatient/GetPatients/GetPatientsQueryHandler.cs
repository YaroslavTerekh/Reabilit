using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserPatient.GetPatients;

public class GetPatientsQueryHandler : IRequestHandler<GetPatientsQuery, List<Patient>>
{
    private readonly DataContext _context;

    public GetPatientsQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Patient>> Handle(GetPatientsQuery request, CancellationToken cancellationToken)
    {
        var query = _context.Patients
            .Include(p => p.AppUser)
            .Include(p => p.City)
            .Include(p => p.Doctor)
            .AsQueryable();

        query = request.SearchText is null ? query :
            query.Where(p => p.AppUser!.FirstName.Contains(request.SearchText) ||
                        p.AppUser.LastName.Contains(request.SearchText) ||
                        p.City!.CityName.Contains(request.SearchText));

        return await query.ToListAsync(cancellationToken);
    }
}
