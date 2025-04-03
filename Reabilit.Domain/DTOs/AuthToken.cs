using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.Domain.DTOs;

public class AuthToken
{
    public string Token { get; set; }

    public DateTime Expires { get; set; }

    public AuthToken(string token, DateTime expires)
    {
        Token = token;
        Expires = expires;
    }
}