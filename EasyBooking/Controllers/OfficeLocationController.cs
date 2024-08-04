using EasyBooking.Api.Commands;
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

    [HttpPost("Add")]
    [ProducesResponseType(typeof(OfficeLocationDto), 200)]
    public async Task<IActionResult> AddOfficeLocation([FromBody] OfficeLocationDto officeLocationDto, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new AddOfficeLocationCommand(officeLocationDto), cancellationToken));
    }

    [HttpDelete("Delete")]
    [ProducesResponseType(typeof(Unit), 200)]
    public async Task<IActionResult> RemoveOfficeLocation([FromBody] int officeLocationId, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new RemoveOfficeLocationCommand(officeLocationId), cancellationToken));
    }
}
