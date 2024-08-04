using EasyBooking.Api.Dto;
using MediatR;

namespace EasyBooking.Api.Commands;

public class RemoveDeskCommand : IRequest<DeskDto>
{
    public int DeskId { get; }
}
