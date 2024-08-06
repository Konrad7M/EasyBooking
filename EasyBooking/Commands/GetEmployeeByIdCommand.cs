using EasyBooking.Api.Dto;
using MediatR;

namespace EasyBooking.Api.Commands;

public class GetEmployeeByIdCommand : IRequest<EmployeeDto>
{
    public int EmployeeId;

    public bool IsAdmin;
}
