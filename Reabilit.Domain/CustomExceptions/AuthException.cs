using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.CustomExceptions;

public class AuthException(int code, string description) : Exception
{
    public int Code { get; set; } = code;
    public string Description { get; set; } = description;
}
