using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Services.Abstractions;

public interface IFileService
{
    public Task<string> SaveFileAsync(IFormFile file, string fileName, CancellationToken cancellationToken);
}
