using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.Entities;

namespace Reabilit.BL.Behaviours.DoctorSchedules.AddDoctorSchedule;

public class AddDoctorScheduleCommandHandler : IRequestHandler<AddDoctorScheduleCommand>
{
    private readonly DataContext _context;

    public AddDoctorScheduleCommandHandler(DataContext context)
    {
        _context = context;
    }

    public async Task Handle(AddDoctorScheduleCommand request, CancellationToken cancellationToken)
    {
        if (request.StartTime >= request.EndTime)
        {
            throw new InvalidRequestException("неправильний час");
        }

        bool alreadyExists = await _context.DoctorSchedules
            .AnyAsync(s => s.DoctorId == request.DoctorId && s.Day == request.Day);

        if (alreadyExists)
        {
            throw new InvalidRequestException("вже є");
        }

        var schedule = new DoctorSchedule
        {
            DoctorId = request.DoctorId,
            Day = request.Day,
            StartTime = request.StartTime,
            EndTime = request.EndTime
        };

        await _context.DoctorSchedules.AddAsync(schedule, cancellationToken);
        await _context.SaveChangesAsync();
    }
}
