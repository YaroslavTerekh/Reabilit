using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.DoctorSchedules.DeleteDoctorSchedule;

public class DeleteDoctorScheduleCommandHandler : IRequestHandler<DeleteDoctorScheduleCommand>
{
    private readonly DataContext _context;

    public DeleteDoctorScheduleCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task Handle(DeleteDoctorScheduleCommand request, CancellationToken cancellationToken)
    {
        var doctorSchedule = await _context.DoctorSchedules
            .FirstOrDefaultAsync(dc => dc.Day == request.dayOfWeek, cancellationToken);

        if(doctorSchedule is null)
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.DoctorSchedule));
        }

        _context.DoctorSchedules.Remove(doctorSchedule);
        await _context.SaveChangesAsync(cancellationToken);
    }
}
