using EasyBooking.Dto;
using MediatR;

namespace EasyBooking.Commands;

public class AddOfficeLocationCommand : IRequest<OfficeLocationDto>
{
    public OfficeLocationDto OfficeLocationDto { get; }

    public AddOfficeLocationCommand(OfficeLocationDto officeLocationDto)
    {
        OfficeLocationDto = officeLocationDto;
    }
}