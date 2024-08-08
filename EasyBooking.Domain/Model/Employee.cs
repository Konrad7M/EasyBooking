namespace EasyBooking.Domain.Model;

public class Employee
{
    public Employee(string name)
    {
        Name = name;
        IsAdmin = false;
    }

    public int Id { get; set; }
    public string Name { get; set; }

    public bool IsAdmin { get; set; }
}
