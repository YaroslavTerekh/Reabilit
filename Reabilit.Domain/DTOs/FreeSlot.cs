using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DTOs;

public class FreeSlot
{
    public DayOfWeek Day { get; set; }

    public List<SlotHour> Slots { get; set; } = new();
}

public class SlotHour
{
    public TimeSpan Time { get; set; }
    public bool IsAvailable { get; set; }
}
