using MediatR;
using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Banners.AddBanner;

public class AddBannerCommand : IRequest
{
    public required IFormFile Image { get; set; }
    public required string Description { get; set; }
}
