namespace EasyBooking.Api.Dto;

public class GetEmployeeByIdQueryDto
{
    public int EmployeeId { get; set; }
    public bool IsAdmin{ get; set; } = false;
}
