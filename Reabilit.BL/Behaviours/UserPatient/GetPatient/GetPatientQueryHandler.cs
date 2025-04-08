using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserPatient.GetPatient;

public class GetPatientQueryHandler : IRequestHandler<GetPatientQuery, Patient>
{
    private readonly DataContext _context;

    public GetPatientQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Patient> Handle(GetPatientQuery request, CancellationToken cancellationToken)
    {
        var patient = await _context.Patients
            .Include(p => p.City)
            .Include(p => p.AppUser)
            .Include(p => p.Doctor)
            .FirstOrDefaultAsync(p => p.Id == request.PatientId, cancellationToken);

        if(patient is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Patient));
        }

        return patient;
    }
}
