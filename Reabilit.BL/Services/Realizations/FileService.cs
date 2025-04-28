using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Reabilit.BL.Services.Abstractions;
using Microsoft.AspNetCore.Hosting;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Reabilit.Domain.Entities;

namespace Reabilit.BL.Services.Realizations;

public class FileService : IFileService
{
    private readonly IWebHostEnvironment _webHostEnvironment;
    private readonly IHttpContextAccessor _httpContextAccessor;
    private readonly IConfiguration _config;

    public FileService(IWebHostEnvironment webHostEnvironment, IHttpContextAccessor httpContextAccessor, IConfiguration config)
    {
        _webHostEnvironment = webHostEnvironment;
        _httpContextAccessor = httpContextAccessor;
        _config = config;
    }

    public string GetFullPathFromRoot(string path)
        => $"{_httpContextAccessor.HttpContext.Request.Scheme}://{_httpContextAccessor.HttpContext.Request.Host.Value}/{_config.GetSection("AppSettings:StaticFiles_RequestPath").Value!}/{path}";

    public void DeleteFileFromRoot(string path)
    {
        File.Delete(Path.Combine(_webHostEnvironment.WebRootPath, path));
    }

    public async Task<string> SaveFileAsync(IFormFile file, string folderName, string fileName, CancellationToken cancellationToken)
    {
        var folderPath = Path.Combine(_webHostEnvironment.WebRootPath, folderName);

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

        return $"{folderName}/{fileName}{fileExtension}";
    }
}