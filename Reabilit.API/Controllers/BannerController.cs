using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reabilit.BL.Behaviours.Banners.AddBanner;
using Reabilit.BL.Behaviours.Banners.DeleteBanner;
using Reabilit.BL.Behaviours.Banners.GetBanners;
using Reabilit.BL.Behaviours.UserPatient.ModifyPatientInfo;
using Reabilit.Domain.Constants;

namespace Reabilit.API.Controllers;

[Authorize(Policy = ApplicationPolicies.AdminOnly)]
[Route("api/[controller]")]
[ApiController]
public class BannerController : ControllerBase
{
    private readonly ISender _sender;

    public BannerController(ISender sender)
    {
        _sender = sender;
    }


    [HttpPost("banner/add")]
    public async Task<IActionResult> AddBannerAsync([FromForm] AddBannerCommand command, CancellationToken cancellationToken = default)
    {
        await _sender.Send(command, cancellationToken);

        return Ok();
    }

    [HttpDelete("banner/delete")]
    public async Task<IActionResult> DeleteBannerAsync([FromBody] DeleteBannerCommand command, CancellationToken cancellationToken = default)
    {
        await _sender.Send(command, cancellationToken);

        return Ok();
    }

    [HttpGet("banners/get")]
    public async Task<IActionResult> GetBannersAsync(CancellationToken cancellationToken = default)
    => Ok(await _sender.Send(new GetBannersQuery(), cancellationToken));
}
