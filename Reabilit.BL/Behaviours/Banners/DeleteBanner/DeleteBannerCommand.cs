using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Banners.DeleteBanner;

public class DeleteBannerCommand : IRequest
{
    public Guid BannerId { get; set; }
}
