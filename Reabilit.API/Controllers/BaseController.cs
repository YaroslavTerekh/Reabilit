using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Reabilit.API.Controllers;

public class BaseController : ControllerBase
{
    protected Guid CurrentUserId => Guid.Parse(User.FindFirstValue(ClaimTypes.NameIdentifier)!);
}
