using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.BL.Services.Abstractions;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.ProcedureEvents.CreateEvent;

public class CreateEventCommandHandler : IRequestHandler<CreateEventCommand>
{
    private readonly DataContext _context;
    private readonly IEventNotificationService _eventNotificationService;

    public CreateEventCommandHandler(DataContext context, IEventNotificationService eventNotificationService)
    {
        _context = context;
        _eventNotificationService = eventNotificationService;
    }

    public async Task Handle(CreateEventCommand request, CancellationToken cancellationToken)
    {
        var patient = await _context.Patients.FirstOrDefaultAsync(p => p.Id == request.PatientId, cancellationToken);

        if (patient is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Patient));
        }

        var doctor = await _context.Doctors
            .Include(d => d.DoctorSchedules)
            .FirstOrDefaultAsync(d => d.Id == request.DoctorId, cancellationToken);

        if (doctor is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Doctor));
        }

        var selectedDoctorSchedule = doctor.DoctorSchedules.FirstOrDefault(dc => dc.Day == request.StartsOn.DayOfWeek);

        if (selectedDoctorSchedule is null)
        {
            throw new RequestException("лікар не працює");
        }

        var allSlots = new List<TimeSpan>();

        for (var time = selectedDoctorSchedule.StartTime; time.Add(TimeSpan.FromMinutes(30)) <= selectedDoctorSchedule.EndTime; time.Add(TimeSpan.FromMinutes(30)))
        {
            allSlots.Add(time);
            time = time.Add(TimeSpan.FromMinutes(30));
        }

        if (!allSlots.Contains(request.StartsOn.TimeOfDay))
        {
            throw new RequestException("такого слоту німа");
        }

        if (await _context.ProcedureEvents.AnyAsync(pe => pe.StartsOn == request.StartsOn, cancellationToken))
        {
            throw new RequestException("дата зайнята");
        }

        var procedureEvent = new ProcedureEvent
        {
            Title = request.Title,
            Description = request.Description,
            DoctorId = request.DoctorId,
            PatientId = request.PatientId,
            StartsOn = request.StartsOn
        };

        try
        {
            await _context.ProcedureEvents.AddAsync(procedureEvent, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await _eventNotificationService.CreateAndSendEventNotificationAsync
                (
                new EventNotificationConfiguration
                {
                    AppUserId = doctor.AppUserId,
                    Message = procedureEvent.Title, //ToDo: Add Messages
                    ProcedureEventId = procedureEvent.Id
                }, cancellationToken);
        }
        catch { } //ToDo: Add error catch logic
    }
}

