using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.UserPatient.GetMyDoctorFreeSlots;

public class GetMyDoctorFreeSlotsQueryHandler : IRequestHandler<GetMyDoctorFreeSlotsQuery, List<FreeSlot>>
{
    private readonly DataContext _context;

    public GetMyDoctorFreeSlotsQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<FreeSlot>> Handle(GetMyDoctorFreeSlotsQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _context.Patients
            .Include(p => p.Doctor)
                .ThenInclude(pd => pd.DoctorSchedules)
            .Include(p => p.Doctor)
                .ThenInclude(pd => pd.ProcedureEvents)
            .Where(p => p.AppUserId == request.CurrentUserId)
            .Select(p => p.Doctor)
            .FirstOrDefaultAsync(cancellationToken);

        if (doctor is null)
        {
            throw new NotFoundException(ErrorMessages.Status404UserNotFound(UserRole.Doctor));
        }

        var result = new List<FreeSlot>();

        foreach (DayOfWeek day in Enum.GetValues(typeof(DayOfWeek)))
        {
            var schedule = doctor.DoctorSchedules.FirstOrDefault(s => s.Day == day);

            var absentSlot = new FreeSlot() { Day = day };

            if (schedule is null)
            {
                result.Add(absentSlot);
                continue;
            }
        }

        foreach (var schedule in doctor.DoctorSchedules)
        {
            var freeSlot = new FreeSlot()
            {
                Day = schedule.Day
            };

            var allSlots = new List<TimeSpan>();

            var time = schedule.StartTime;
            while (time + TimeSpan.FromMinutes(30) <= schedule.EndTime)
            {
                allSlots.Add(time);
                time = time.Add(TimeSpan.FromMinutes(30));
            }

            foreach (var slot in allSlots)
            {
                if (!doctor.ProcedureEvents.Any(pe => pe.StartsOn.DayOfWeek == schedule.Day && pe.StartsOn.TimeOfDay == slot && pe.Status == ProcedureEventStatus.Planned))
                {
                    freeSlot.Slots.Add(new SlotHour { Time = slot, IsAvailable = true });

                    continue;
                }

                freeSlot.Slots.Add(new SlotHour { Time = slot, IsAvailable = false });
            }

            result.Add(freeSlot);
        }

        return result.OrderBy(fs => fs.Day).ToList();
    }
}
