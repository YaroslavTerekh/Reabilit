using MediatR;
using Microsoft.AspNetCore.Http;
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

namespace Reabilit.BL.Behaviours.UserPatient.AddNewProcedureEvent;

public class AddNewProcedureEventCommandHandler : IRequestHandler<AddNewProcedureEventCommand>
{
    private readonly DataContext _context;
    private readonly IEventNotificationService _eventNotificationService;

    public AddNewProcedureEventCommandHandler(DataContext context, IEventNotificationService eventNotificationService)
    {
        _context = context;
        _eventNotificationService = eventNotificationService;
    }

    public async Task Handle(AddNewProcedureEventCommand request, CancellationToken cancellationToken)
    {
        var patient = await _context.Patients
            .Where(p => p.AppUserId == request.CurrentUserId)
            .FirstOrDefaultAsync(cancellationToken);

        if (patient is null)
        {
            throw new AuthException(StatusCodes.Status401Unauthorized, ErrorMessages.Unauthorized401);
        }

        var doctor = await _context.Doctors
            .Include(d => d.DoctorSchedules)
            .Include(d => d.AppUser)
            .FirstOrDefaultAsync(d => d.Id == patient.DoctorId, cancellationToken);

        if(doctor is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Doctor));
        }

        var selectedDoctorSchedule = doctor.DoctorSchedules.FirstOrDefault(dc => dc.Day == request.StartsOn.DayOfWeek);

        if (selectedDoctorSchedule is null)
        {
            throw new RequestException(ErrorMessages.DoctorAbsent);
        }

        var allSlots = new List<TimeSpan>();

        for (var time = selectedDoctorSchedule.StartTime; time.Add(TimeSpan.FromMinutes(30)) <= selectedDoctorSchedule.EndTime; time.Add(TimeSpan.FromMinutes(30)))
        {
            allSlots.Add(time);
            time = time.Add(TimeSpan.FromMinutes(30));
        }

        if (!allSlots.Contains(request.StartsOn.TimeOfDay))
        {
            throw new RequestException(ErrorMessages.SlotAbsent);
        }

        if (await _context.ProcedureEvents.AnyAsync(pe => pe.StartsOn == request.StartsOn
                                && pe.Status == ProcedureEventStatus.Planned, cancellationToken))
        {
            throw new RequestException(ErrorMessages.DateBusy);
        }

        var procedure = new ProcedureEvent
        {
            DoctorId = doctor.Id,
            Title = $"Запис до {doctor.AppUser!.FirstName} {doctor.AppUser.LastName}",
            Description = "Ви були записані до свого лікуйочого лікаря, він огляне Вас, вислухає проблеми які Вас турбують, випише необхідні процедури",
            StartsOn = request.StartsOn,
            Status = ProcedureEventStatus.Planned,
            PatientId = patient.Id
        };


        try
        {
            await _context.ProcedureEvents.AddAsync(procedure, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            await _eventNotificationService.CreateAndSendEventNotificationAsync
                (
                new EventNotificationConfiguration
                {
                    AppUserId = doctor.AppUserId,
                    Message = procedure.Title,
                    ProcedureEventId = procedure.Id
                }, cancellationToken);
        }
        catch { } //ToDo: Add error catch logic
    }
}
