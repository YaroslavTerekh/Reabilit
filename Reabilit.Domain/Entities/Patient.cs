using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.Entities;

public class Patient : BaseEntity
{ 
    public List<Analyze> Analyzes { get; set; } = new();

    public Guid CityId { get; set; }
    public City? City { get; set; }

    public Guid AppUserId { get; set; }
    public AppUser? AppUser { get; set; }

    public Guid? DoctorId { get; set; }
    public Doctor? Doctor { get; set; }
}
