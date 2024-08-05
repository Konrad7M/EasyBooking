using EasyBooking.Api.Commands;
using EasyBooking.Api.Dto;
using EasyBooking.Commands;
using EasyBooking.Dto;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EasyBooking.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ReservationController : ControllerBase
{
    private readonly IMediator _mediator;
    public ReservationController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet("GetDesksByLocation")]
    [ProducesResponseType(typeof(List<DeskDto>), 200)]
    public async Task<IActionResult> GetDesksByLocationId([FromBody] GetDesksQueryDto getDesksQueryDto, CancellationToken cancellationToken)
    {
        // todo: potrzebujesz wyslac komnde na postawie tych dat no dobrac sie do reserwacji bedzie musiał dla kazdego na start sprawdx ta flage czy id obiektu jest wyfiltruj  z z basy dokop sie do rezerwacji jest flaga do pokazania tylko available
        return Ok(await _mediator.Send(new GetDesksCommand(getDesksQueryDto.From, getDesksQueryDto.To, getDesksQueryDto.LocationId, getDesksQueryDto.IsAdmin, getDesksQueryDto.ShowAvailableOnly), cancellationToken));
    }

    [HttpPut("ReverveDeskForAPeriod")]
    [ProducesResponseType(typeof(ReservationDto), 200)]
    public async Task<IActionResult> ReverveDeskForAPeriod([FromBody] ReservationDto reservationDto, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new ReserveDeskCommand(reservationDto.ReservingEmployeeId, reservationDto.ReservedDeskId,reservationDto.FromDate, reservationDto.ToDate), cancellationToken));
    }
}
