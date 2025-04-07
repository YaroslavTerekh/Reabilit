using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.Entities;

public class DoctorSchedule : BaseEntity
{
    public DayOfWeek Day { get; set; }

    public TimeSpan StartTime { get; set; }
    public TimeSpan EndTime { get; set; }

    public Guid DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
}
