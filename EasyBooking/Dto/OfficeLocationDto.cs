using EasyBooking.Api.Dto;

namespace EasyBooking.Dto;

public class OfficeLocationDto
{
    public int Id { get; set; }
    public string LocationName { get; set; }
    public List<DeskDto> Desks { get; set; }
}
