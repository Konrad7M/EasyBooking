using EasyBooking.Dto;
using MediatR;

namespace EasyBooking.Commands;

public class AddDeskCommand : IRequest<OfficeLocationDto>
{
    public int OfficeLocationId { get; }

    public AddDeskCommand(int officeLocationId)
    {
        OfficeLocationId = officeLocationId;
    }
}