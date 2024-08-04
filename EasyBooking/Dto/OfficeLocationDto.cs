using EasyBooking.Domain.Model; 

namespace EasyBooking.Dto;

public class OfficeLocationDto
{
    public int Id { get; set; }
    public string LocationName { get; set; }
    public List<Desk> Desks { get; set; }
}
