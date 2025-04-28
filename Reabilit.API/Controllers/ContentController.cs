using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Reabilit.BL.Behaviours.Content.Cities.GetAllCities;
using Reabilit.Domain.DTOs;

namespace Reabilit.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ContentController : ControllerBase
{
    private readonly ISender _sender;

    public ContentController(ISender sender)
    {
        _sender = sender;
    }

    [HttpGet("cities/get")]
    public async Task<IActionResult> GetAllCitiesAsync(CancellationToken cancellationToken = default)
        => Ok(await _sender.Send(new GetAllCitiesQuery(), cancellationToken));

    [HttpGet("doctor-classes/get")]
    public async Task<IActionResult> GetAllDoctorClassesAsync(CancellationToken cancellationToken = default)
    => Ok(await _sender.Send(new GetDoctorClassesQuery(), cancellationToken));
}
