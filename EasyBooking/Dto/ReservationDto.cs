namespace EasyBooking.Api.Dto;

public class ReservationDto
{
    public int Id { get; set; }
    public int ReservedDeskId { get; set; }
    public int ReservingEmployeeId { get; set; }
    public DateOnly FromDate { get; set; }
    public DateOnly ToDate { get; set; }
}
