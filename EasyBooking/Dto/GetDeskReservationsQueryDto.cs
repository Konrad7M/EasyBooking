namespace EasyBooking.Api.Dto;

public class GetDeskReservationsQueryDto
{
    public int DeskId { get; set; }

    public bool IsAdmin { get; set; } = false;
}
