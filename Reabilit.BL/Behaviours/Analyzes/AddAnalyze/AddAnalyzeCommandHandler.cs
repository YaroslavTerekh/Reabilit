using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Reabilit.BL.Services.Abstractions;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Analyzes.AddAnalyze;

public class AddAnalyzeCommandHandler : IRequestHandler<AddAnalyzeCommand, List<AnalyzeDTO>>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;
    private readonly IConfiguration _config;

    public AddAnalyzeCommandHandler(DataContext context, IFileService fileService, IConfiguration config)
    {
        _context = context;
        _fileService = fileService;
        _config = config;
    }

    public async Task<List<AnalyzeDTO>> Handle(AddAnalyzeCommand request, CancellationToken cancellationToken)
    {
        var analyze = new Analyze
        {
            Title = request.Title,
            Value = request.Value,
            Unit = request.Unit,
            IsNormal = request.IsNormal,
            PatientId = request.PatientId,
            IconPath = 
                await _fileService.SaveFileAsync(request.Icon, _config.GetSection("AppSettings:Analyze_ContentFolderName").Value!, Guid.NewGuid().ToString(), cancellationToken)
        };

        try
        {
            await _context.Analyzes.AddAsync(analyze, cancellationToken);
            await _context.SaveChangesAsync(cancellationToken);
        }
        catch
        {
            _fileService.DeleteFileFromRoot(analyze.IconPath);
            throw new RequestException("Не вдалося додати результат аналізу");
        }

        return await _context.Analyzes
            .Where(a => a.PatientId == request.PatientId)
            .Select(a => new AnalyzeDTO
            {
                Id = a.Id,
                IconPath = _fileService.GetFullPathFromRoot(a.IconPath),
                IsNormal = a.IsNormal,
                Title = a.Title,
                Unit = a.Unit,
                Value = a.Value
            })
            .ToListAsync(cancellationToken);
    }
}
