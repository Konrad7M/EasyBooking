using EasyBooking.Api.Commands;
using EasyBooking.Api.Dto;
using EasyBooking.Commands;
using EasyBooking.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EasyBooking.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmployeeController : ControllerBase
{
    private readonly IMediator _mediator;
    public EmployeeController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpPost("Add")]
    [ProducesResponseType(typeof(EmployeeDto), 200)]
    public async Task<IActionResult> AddEmployee([FromBody] EmployeeDto employeeDto, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new AddEmployeeCommand(employeeDto), cancellationToken));
    }

    [HttpDelete("Delete")]
    [ProducesResponseType(typeof(EmployeeDto), 200)]
    public async Task<IActionResult> RemoveEmployee([FromBody] int employeeId, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new RemoveEmployeeCommand(employeeId), cancellationToken));
    }
}
