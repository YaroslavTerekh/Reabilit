using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DTOs;

public class AnalyzeDTO
{
    public Guid Id { get; set; }

    public required string Title { get; set; }

    public required string Unit { get; set; }

    public required string Value { get; set; }

    public bool IsNormal { get; set; }

    public required string IconPath { get; set; }
}
