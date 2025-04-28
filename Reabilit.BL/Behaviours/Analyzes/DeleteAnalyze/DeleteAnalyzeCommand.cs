using MediatR;
using Reabilit.Domain.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Behaviours.Analyzes.DeleteAnalyze;

public record DeleteAnalyzeCommand(Guid AnalyzeId) : IRequest<List<AnalyzeDTO>>;
