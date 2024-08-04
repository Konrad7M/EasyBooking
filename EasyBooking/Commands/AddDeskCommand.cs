using EasyBooking.Api.Dto;
using MediatR;

namespace EasyBooking.Commands;

public class AddDeskCommand : IRequest<DeskDto>
{
    public int OfficeLocationId { get; }

    public AddDeskCommand(int officeLocationId)
    {
        OfficeLocationId = officeLocationId;
    }
}