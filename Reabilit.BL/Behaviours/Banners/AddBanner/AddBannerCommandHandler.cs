using MediatR;
using Microsoft.Extensions.Configuration;
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
    private readonly IConfiguration _config;

    public AddBannerCommandHandler(DataContext context, IFileService fileService, IConfiguration config)
    {
        _context = context;
        _fileService = fileService;
        _config = config;
    }

    public async Task Handle(AddBannerCommand request, CancellationToken cancellationToken)
    {
        var fileName = Guid.NewGuid().ToString();

        var banner = new Banner
        {
            Description = request.Description,
            ImageName = fileName,
            ImagePath = await _fileService.SaveFileAsync(request.Image, _config.GetSection("AppSettings:Banner_ContentFolderName").Value!, fileName, cancellationToken)
        };

        try
        {
            await _context.Banners.AddAsync(banner, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            _fileService.DeleteFileFromRoot(banner.ImagePath);
            throw new RequestException("Не вдалося додати банер");
        }
    }
}
