using EasyBooking.Api.Dto;
using MediatR;

namespace EasyBooking.Api.Commands;

public class ReserveDeskCommand : IRequest<ReservationDto>
{
    public int ReservedDeskId { get; set; }
    public int ReservingEmployeeId { get; set; }
    public DateOnly FromDate { get; set; }
    public DateOnly ToDate { get; set; }

    public ReserveDeskCommand(int reservedDeskId, int reservingEmployeeId, DateOnly fromDate, DateOnly toDate)
    {
        ReservedDeskId = reservedDeskId;
        ReservingEmployeeId = reservingEmployeeId;
        FromDate = fromDate;
        ToDate = toDate;
    }
}
