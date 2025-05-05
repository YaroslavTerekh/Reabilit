using Microsoft.AspNetCore.Mvc;
using Reabilit.Domain.Constants;
using Reabilit.Domain.CustomExceptions;
using System.Security.Claims;

namespace Reabilit.API.Controllers;

public class BaseController : ControllerBase
{
    protected Guid CurrentUserId
    {
        get
        {
            if (!Guid.TryParse(User.FindFirstValue(ClaimTypes.NameIdentifier)!, out Guid result))
            {
                throw new AuthException(StatusCodes.Status401Unauthorized, ErrorMessages.Unauthorized401);
            }

            return result;
        }
    }
}
