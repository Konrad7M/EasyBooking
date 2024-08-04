using EasyBooking.Commands;
using EasyBooking.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EasyBooking.Controllers;

[ApiController]
[Route("api/[controller]")]
public class OfficeLocationController : ControllerBase
{
    private readonly IMediator _mediator;
    public OfficeLocationController(IMediator mediator) 
    {
        _mediator = mediator;
    }

    [HttpPut("Post")]
    [ProducesResponseType(typeof(OfficeLocationDto), 200)]
    public async Task<IActionResult> AddOfficeLocation([FromBody] OfficeLocationDto officeLocationDto, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new AddOfficeLocationCommand(officeLocationDto), cancellationToken));
    }
}
