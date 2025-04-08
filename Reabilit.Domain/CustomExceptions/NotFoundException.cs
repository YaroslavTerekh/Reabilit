using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.CustomExceptions;

public class NotFoundException(string description) : Exception
{
    public string Description { get; set; } = description;
}
