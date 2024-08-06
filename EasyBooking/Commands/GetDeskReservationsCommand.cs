using EasyBooking.Api.Dto;
using MediatR;

namespace EasyBooking.Api.Commands;

public class GetDeskReservationsCommand : IRequest<List<ReservationDto>>
{
    public int DeskId { get; set; }

    public bool IsAdmin { get; set; }
}
