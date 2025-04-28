using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Content.DoctorClasses;

public class GetDoctorClassesQueryHandler : IRequestHandler<GetDoctorClassesQuery, List<DoctorClassDTO>>
{
    private readonly DataContext _context;

    public GetDoctorClassesQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<DoctorClassDTO>> Handle(GetDoctorClassesQuery request, CancellationToken cancellationToken)
    {
        return await _context.DoctorClasses
            .Select(dc => new DoctorClassDTO 
            { 
                Id = dc.Id,
                ClassName = dc.ClassName
            }).ToListAsync(cancellationToken);
    }
}
