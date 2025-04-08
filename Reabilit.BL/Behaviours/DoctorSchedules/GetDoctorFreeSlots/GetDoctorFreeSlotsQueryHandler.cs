using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.DoctorSchedules.GetDoctorFreeSlots;

public class GetDoctorFreeSlotsQueryHandler : IRequestHandler<GetDoctorFreeSlotsQuery, List<FreeSlot>>
{
    private readonly DataContext _context;

    public GetDoctorFreeSlotsQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<FreeSlot>> Handle(GetDoctorFreeSlotsQuery request, CancellationToken cancellationToken)
    {
        var doctor = await _context.Doctors
            .Include(d => d.DoctorSchedules)
            .Include(d => d.ProcedureEvents)
            .FirstOrDefaultAsync(d => d.Id == request.DoctorId, cancellationToken);

        if(doctor is null)
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
                if(!doctor.ProcedureEvents.Any(pe => pe.StartsOn.DayOfWeek == schedule.Day && pe.StartsOn.TimeOfDay == slot))
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
