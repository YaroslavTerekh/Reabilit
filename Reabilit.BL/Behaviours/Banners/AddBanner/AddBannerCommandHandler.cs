using MediatR;
using Reabilit.BL.Services.Abstractions;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Banners.AddBanner;

public class AddBannerCommandHandler : IRequestHandler<AddBannerCommand>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;

    public AddBannerCommandHandler(DataContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task Handle(AddBannerCommand request, CancellationToken cancellationToken)
    {
        var fileName = Guid.NewGuid().ToString();

        var banner = new Banner
        {
            Description = request.Description,
            ImageName = fileName,
            ImagePath = await _fileService.SaveFileAsync(request.Image, fileName, cancellationToken)
        };

        try
        {
            await _context.Banners.AddAsync(banner, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            File.Delete(banner.ImagePath);
            throw new RequestException("Не вдалося додати банер");
        }
    }
}
