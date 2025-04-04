using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserPatient.ToggleAccount;

public class ToggleAccountCommandHandler : IRequestHandler<ToggleAccountCommand>
{
    private readonly DataContext _context;

    public ToggleAccountCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task Handle(ToggleAccountCommand request, CancellationToken cancellationToken)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == request.PatientId, cancellationToken);

        if(patient is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Patient));
        }

        patient.IsActive = !patient.IsActive;

        await _context.SaveChangesAsync(cancellationToken);
    }
}
