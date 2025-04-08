using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Banners.GetBanners;

public class GetBannersQueryHandler : IRequestHandler<GetBannersQuery, List<Banner>>
{
    private readonly DataContext _context;

    public GetBannersQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<Banner>> Handle(GetBannersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Banners
            .ToListAsync(cancellationToken);
    }
}
