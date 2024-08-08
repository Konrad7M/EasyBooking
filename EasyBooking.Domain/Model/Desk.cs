namespace EasyBooking.Domain.Model;

public class Desk
{
    public Desk(bool isAvailable = true)
    {
        IsAvailable = isAvailable;
    }

    public int Id { get; set; }
    public bool IsAvailable { get; set; }
}
