﻿using EasyBooking.Api.Commands;
using EasyBooking.Api.Dto;
using MediatR;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace EasyBooking.Api.Controllers;
[Authorize(AuthenticationSchemes = "BasicAuthentication")]
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

    [HttpPost("Get")]
    [ProducesResponseType(typeof(EmployeeDto), 200)]
    public async Task<IActionResult> GetEmployeeById([FromBody] GetEmployeeByIdQueryDto getEmployeeByIdQueryDto, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetEmployeeByIdCommand()
        {
            EmployeeId = getEmployeeByIdQueryDto.EmployeeId,
            IsAdmin = getEmployeeByIdQueryDto.IsAdmin
        }, cancellationToken));
    }

    [HttpDelete("Delete")]
    [ProducesResponseType(typeof(EmployeeDto), 200)]
    public async Task<IActionResult> RemoveEmployee([FromBody] int employeeId, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new RemoveEmployeeCommand(employeeId), cancellationToken));
    }
}
