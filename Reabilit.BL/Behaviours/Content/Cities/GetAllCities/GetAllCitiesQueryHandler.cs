using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Content.Cities.GetAllCities;

public class GetAllCitiesQueryHandler : IRequestHandler<GetAllCitiesQuery, List<CityDTO>>
{
    private readonly DataContext _context;

    public GetAllCitiesQueryHandler(DataContext context)
    {
        _context = context;
    }

    public async Task<List<CityDTO>> Handle(GetAllCitiesQuery request, CancellationToken cancellationToken)
    {
        return await _context.Cities
            .Select(c => new CityDTO {
                Id = c.Id,
                CityName = c.CityName
            }).ToListAsync(cancellationToken);
    }
}
