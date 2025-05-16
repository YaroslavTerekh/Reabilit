using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using Reabilit.BL.Services.Abstractions;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using Reabilit.Domain.Entities;
using Reabilit.Domain.SignalrHub;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Services.Realizations;

public class TreatmentNotificationService : ITreatmentNotificationService
{
    private readonly DataContext _context;
    private readonly IHubContext<NotificationsHub> _hubContext;

    public TreatmentNotificationService(DataContext context, IHubContext<NotificationsHub> hubContext)
    {
        _context = context;
        _hubContext = hubContext;
    }

    public async Task CreateAndSendTreatmentNotificationAsync(Action<TreatmentNotificationConfiguration> configure, CancellationToken cancellationToken = default)
    {
        var configuration = new TreatmentNotificationConfiguration { Message = "Default implementation", Recommendations = "Default implementation" };
        configure(configuration);

        if (!await _context.Users.AnyAsync(u => u.Id == configuration.AppUserId, cancellationToken))
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.User));
        }

        var treatmentNotification = new TreatmentNotification
        {
            IsRead = false,
            Message = configuration.Message,
            AppUserId = configuration.AppUserId,
            Recommendations = configuration.Recommendations
        };

        try
        {
            await _context.TreatmentNotification.AddAsync(treatmentNotification, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);

            var procedureEventDTO = await _context.ProcedureEvents
                .Select(pe => new ProcedureEventDTO
                {
                    Id = pe.Id,
                    Title = pe.Title,
                    Description = pe.Description,     
                    Result = pe.Result,
                    StartsOn = pe.StartsOn,
                    Status = pe.Status,
                    Doctor = new DoctorDTO
                    {
                        Age = pe.Doctor!.AppUser!.Age,
                        AppUserId = pe.Doctor.AppUserId,
                        Biography = pe.Doctor.Biography,
                        Degree = pe.Doctor.Degree,
                        ExperienceInYear = pe.Doctor.ExperienceInYear,
                        FirstName = pe.Doctor.AppUser.FirstName,
                        LastName = pe.Doctor.AppUser.LastName,
                        Id = pe.Doctor.Id,
                        PhoneNumber = pe.Doctor!.AppUser!.PhoneNumber!,
                        DoctorClass = new DoctorClassDTO
                        {
                            Id = pe.Doctor.DoctorClass!.Id,
                            ClassName = pe.Doctor.DoctorClass.ClassName
                        },
                        DoctorClassId = pe.Doctor.DoctorClassId
                    },
                    Patient = null
                })
                .FirstOrDefaultAsync(pe => pe.Id == configuration.ProcedureEventId, cancellationToken);

            await _hubContext.Clients.User(configuration.AppUserId.ToString()).SendAsync(nameof(CreateAndSendTreatmentNotificationAsync), procedureEventDTO);
        }
        catch { } // ToDo: Create block for catching
    }

    public Task DeleteNotificationAsync(Guid id, CancellationToken cancellationToken = default)
    {
        throw new NotImplementedException();
    }
}
