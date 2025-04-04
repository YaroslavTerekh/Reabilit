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

namespace Reabilit.BL.Behaviours.UserPatient.ModifyPatientInfo;

public class ModifyPatientInfoQueryHandler : IRequestHandler<ModifyPatientInfoQuery, Patient>
{
    private readonly DataContext _context;

    public ModifyPatientInfoQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<Patient> Handle(ModifyPatientInfoQuery request, CancellationToken cancellationToken)
    {
        var patient = await _context.Patients
            .Include(p => p.Doctor)
            .FirstOrDefaultAsync(p => p.Id == request.PatientId, cancellationToken);
    
        if(patient is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Patient));
        }

        var user = await _context.Users.FirstOrDefaultAsync(u => u.Id == patient.AppUserId, cancellationToken);
        
        if (user is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Default));
        }

        user.FirstName = request.FirstName;
        user.LastName = request.LastName;
        user.Age = request.Age;
        user.PhoneNumber = request.PhoneNumber;
        patient.CityId = request.CityId;

        await _context.SaveChangesAsync(cancellationToken);

        return patient;
    }
}
