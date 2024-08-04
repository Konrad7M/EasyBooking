namespace EasyBooking.Api.Dto;

public class EmployeeDto
{
    public EmployeeDto(int id, string name)
    {
        Id = id;
        Name = name;
        IsAdmin = false;
    }

    public int Id { get; set; }
    public string Name { get; set; }
    public bool IsAdmin { get; set; }
}
