using EasyBooking.Api.Dto;
using MediatR;

namespace EasyBooking.Api.Commands;

public class GetDesksCommand : IRequest<List<DeskDto>>
{
    public DateOnly From { get; set; }
    public DateOnly To { get; set; }
    public int LocationId { get; set; }
    public bool IsAdmin { get; set; }
    public bool ShowAvailableOnly { get; set; }

    public GetDesksCommand(DateOnly from, DateOnly to, int locationId, bool isAdmin, bool showAvailableOnly)
    {
        From = from;
        To = to;
        LocationId = locationId;
        IsAdmin = isAdmin;
        ShowAvailableOnly = showAvailableOnly;
    }
}
