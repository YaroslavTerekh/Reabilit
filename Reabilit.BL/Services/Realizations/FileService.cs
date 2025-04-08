using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Reabilit.BL.Services.Abstractions;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Services.Realizations;

public class FileService : IFileService
{
    private readonly IConfiguration _config;
    private readonly IWebHostEnvironment _webHostEnvironment;

    public FileService(IConfiguration config, IWebHostEnvironment webHostEnvironment)
    {
        _config = config;
        _webHostEnvironment = webHostEnvironment;
    }

    public async Task<string> SaveFileAsync(IFormFile file, string fileName, CancellationToken cancellationToken)
    {
        var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, _config.GetSection("AppSettings:Banner_ContentFolderName").Value!);

        var fileExtension = Path.GetExtension(file.FileName);
        var fullPath = Path.Combine(folderPath, fileName + fileExtension);

        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }

        using (var fs = File.Open(fullPath, FileMode.OpenOrCreate | FileMode.Append))
        {
            await file.CopyToAsync(fs);
        }

        return fullPath;
    }
}