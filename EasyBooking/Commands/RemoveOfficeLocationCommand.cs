using EasyBooking.Dto;
using MediatR;

namespace EasyBooking.Api.Commands;

public class RemoveOfficeLocationCommand : IRequest<Unit>
{
    public int OfficeLocationId { get; }

    public RemoveOfficeLocationCommand(int officeLocationId)
    {
        OfficeLocationId = officeLocationId;
    }
}
