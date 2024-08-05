namespace EasyBooking.Api.Dto;

public class GetDesksQueryDto
{
    public DateOnly From { get; set; }
    public DateOnly To { get; set; }
    public int LocationId { get; set; }
    public bool IsAdmin { get; set; }
    public bool ShowAvailableOnly { get; set; }

    public GetDesksQueryDto(DateOnly from, DateOnly to, int locationId, bool isAdmin, bool showAvailableOnly)
    {
        From = from;
        To = to;
        LocationId = locationId;
        IsAdmin = isAdmin;
        ShowAvailableOnly = showAvailableOnly;
    }
}
