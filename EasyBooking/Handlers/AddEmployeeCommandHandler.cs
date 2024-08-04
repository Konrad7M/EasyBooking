using AutoMapper;
using EasyBooking.Api.Commands;
using EasyBooking.Api.Dto;
using EasyBooking.Commands;
using EasyBooking.Domain.Model;
using EasyBooking.Dto;
using EasyBooking.Infrastructure.Database;
using MediatR;

namespace EasyBooking.Api.Handlers;

public class AddEmployeeCommandHandler : IRequestHandler<AddEmployeeCommand, EmployeeDto>
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public AddEmployeeCommandHandler(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EmployeeDto> Handle(AddEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = new Employee(request.EmployeeDto.Name);
        _context.Employees.Add(employee);
        await _context.SaveChangesAsync();
        return _mapper.Map<EmployeeDto>(employee);
    }
}
