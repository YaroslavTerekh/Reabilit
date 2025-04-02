using Reabilit.Domain.DTOs;
using Reabilit.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Reabilit.BL.Services.Abstractions;

public interface IJWTService
{
    public AuthToken GenerateJWT(AppUser user, string[] roles);
}
