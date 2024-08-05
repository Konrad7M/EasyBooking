using EasyBooking.Api.Commands;
using EasyBooking.Api.Dto;
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

    [HttpPost("GetDesksByLocation")]
    [ProducesResponseType(typeof(List<DeskDto>), 200)]
    public async Task<IActionResult> GetDesksByLocationId([FromBody] GetDesksQueryDto getDesksQueryDto, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetDesksCommand(getDesksQueryDto.From, getDesksQueryDto.To, getDesksQueryDto.LocationId, getDesksQueryDto.IsAdmin, getDesksQueryDto.ShowAvailableOnly), cancellationToken));
    }

    [HttpPost("ReverveDeskForAPeriod")]
    [ProducesResponseType(typeof(ReservationDto), 200)]
    public async Task<IActionResult> ReverveDeskForAPeriod([FromBody] ReservationDto reservationDto, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new ReserveDeskCommand(reservationDto.ReservingEmployeeId, reservationDto.ReservedDeskId,reservationDto.FromDate, reservationDto.ToDate), cancellationToken));
    }

    [HttpPut("ChangeReservation")]
    [ProducesResponseType(typeof(ReservationDto), 200)]
    public async Task<IActionResult> ChangeReservation([FromBody] ReservationDto reservationDto, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new ReserveDeskCommand(reservationDto.ReservingEmployeeId, reservationDto.ReservedDeskId, reservationDto.FromDate, reservationDto.ToDate), cancellationToken));
    }

    [HttpPost("GetReservationsByDeskId")]
    [ProducesResponseType(typeof(List<ReservationDto>), 200)]
    public async Task<IActionResult> GetReservationsByDeskID([FromBody] GetDeskReservationsQueryDto getDeskReservationsQueryDto, CancellationToken cancellationToken)
    {
        return Ok(await _mediator.Send(new GetDeskReservationsCommand { DeskId = getDeskReservationsQueryDto.DeskId }));
    }
}
