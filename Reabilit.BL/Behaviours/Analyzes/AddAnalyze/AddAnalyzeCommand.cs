using MediatR;
using Microsoft.AspNetCore.Http;
using Reabilit.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Analyzes.AddAnalyze;

public class AddAnalyzeCommand : IRequest<List<AnalyzeDTO>>
{
    public required string Title { get; set; }

    public required string Unit { get; set; }

    public required string Value { get; set; }

    public bool IsNormal { get; set; }

    public required IFormFile Icon { get; set; }

    public Guid PatientId { get; set; }
}
