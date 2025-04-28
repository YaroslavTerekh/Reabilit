using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DTOs;

public class BannerDTO 
{
    public Guid Id { get; set; }

    public required string ImagePath { get; set; }

    public required string Description { get; set; }
}
