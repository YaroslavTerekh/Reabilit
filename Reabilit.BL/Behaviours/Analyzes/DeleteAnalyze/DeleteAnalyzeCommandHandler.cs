using MediatR;
using Microsoft.EntityFrameworkCore;
using Reabilit.BL.Services.Abstractions;
using Reabilit.Domain.Constants;
using Reabilit.Domain.DbConnection;
using Reabilit.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Analyzes.DeleteAnalyze;

public class DeleteAnalyzeCommandHandler : IRequestHandler<DeleteAnalyzeCommand, List<AnalyzeDTO>>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;

    public DeleteAnalyzeCommandHandler(DataContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<List<AnalyzeDTO>> Handle(DeleteAnalyzeCommand request, CancellationToken cancellationToken)
    {
        var analyze = await _context.Analyzes.FirstOrDefaultAsync(a => a.Id == request.AnalyzeId, cancellationToken);

        if (analyze is null)
        {
            throw new NotFoundException(ErrorMessages.Status404EntityNotFound(EntityType.Analyze));
        }

        _context.Analyzes.Remove(analyze);
        await _context.SaveChangesAsync(cancellationToken);

        _fileService.DeleteFileFromRoot(analyze.IconPath);

        return await _context.Analyzes
            .Where(a => a.PatientId == analyze.PatientId)
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
