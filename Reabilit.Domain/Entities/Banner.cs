using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.Entities;

public class Banner : BaseEntity
{
    public required string ImageName { get; set; }
    public required string ImagePath { get; set; }
    public required string Description { get; set; }
}
