using MediatR;
using Microsoft.AspNetCore.Http;
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

namespace Reabilit.BL.Behaviours.UserPatient.GetMyAnalyzes;

public class GetMyAnalyzesQueryHandler : IRequestHandler<GetMyAnalyzesQuery, List<AnalyzeDTO>>
{
    private readonly DataContext _context;
    private readonly IFileService _fileService;

    public GetMyAnalyzesQueryHandler(DataContext context, IFileService fileService)
    {
        _context = context;
        _fileService = fileService;
    }

    public async Task<List<AnalyzeDTO>> Handle(GetMyAnalyzesQuery request, CancellationToken cancellationToken)
    {
        var patientId = await _context.Patients
            .Where(p => p.AppUserId == request.CurrentUserId)
            .Select(p => p.Id)
            .FirstOrDefaultAsync(cancellationToken);

        if(patientId == Guid.Empty)
        {
            throw new AuthException(StatusCodes.Status401Unauthorized, ErrorMessages.Unauthorized401);
        }

        return await _context.Analyzes
            .Where(a => a.PatientId == patientId)
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