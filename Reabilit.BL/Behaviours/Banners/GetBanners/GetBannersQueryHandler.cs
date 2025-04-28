using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Hosting;
using Reabilit.BL.Services.Abstractions;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Banners.GetBanners;

public class GetBannersQueryHandler : IRequestHandler<GetBannersQuery, List<BannerDTO>>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;

    public GetBannersQueryHandler(DataContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<List<BannerDTO>> Handle(GetBannersQuery request, CancellationToken cancellationToken)
    {
        return await _context.Banners
            .Select(b => new BannerDTO
            {
                Description = b.Description,
                Id = b.Id,
                ImagePath = _fileService.GetFullPathFromRoot(b.ImagePath)
            })
            .ToListAsync(cancellationToken);
    }
}
