using EasyBooking.Api.Dto;
using MediatR;

namespace EasyBooking.Api.Commands;

public class RemoveEmployeeCommand : IRequest<EmployeeDto>
{
    public int EmployeeId { get; set;}

    public RemoveEmployeeCommand(int employeeId)
    {
        EmployeeId = employeeId;
    }
}
