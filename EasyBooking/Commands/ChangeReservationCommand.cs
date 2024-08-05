using EasyBooking.Api.Dto;
using MediatR;

namespace EasyBooking.Api.Commands;

public class ChangeReservationCommand : IRequest<ReservationDto>
{
    public int ReservationId { get; set; }
    public int ReservedDeskId { get; set; }
    public int ReservingEmployeeId { get; set; }
    public DateOnly FromDate { get; set; }
    public DateOnly ToDate { get; set; }
}
