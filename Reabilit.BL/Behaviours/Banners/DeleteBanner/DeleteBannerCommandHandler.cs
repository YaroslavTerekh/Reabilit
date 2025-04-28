using MediatR;
using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;
using Reabilit.BL.Services.Abstractions;
using Reabilit.BL.Services.Realizations;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Banners.DeleteBanner;

public class DeleteBannerCommandHandler : IRequestHandler<DeleteBannerCommand>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;

    public DeleteBannerCommandHandler(DataContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task Handle(DeleteBannerCommand request, CancellationToken cancellationToken)
    {
        var banner = await _context.Banners.FirstOrDefaultAsync(b => b.Id == request.BannerId, cancellationToken);

        if(banner is null)
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.Banner));
        }

        _context.Banners.Remove(banner);
        await _context.SaveChangesAsync(cancellationToken);


        _fileService.DeleteFileFromRoot(banner.ImagePath);
    }
}
