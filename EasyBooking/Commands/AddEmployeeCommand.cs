using EasyBooking.Api.Dto;
using MediatR;

namespace EasyBooking.Api.Commands;

public class AddEmployeeCommand : IRequest<EmployeeDto>
{
    public EmployeeDto EmployeeDto { get; set; }
    public AddEmployeeCommand(EmployeeDto employeeDto)
    {
        EmployeeDto = employeeDto;
    }
}
