using AutoMapper;
using EasyBooking.Api.Commands;
using EasyBooking.Api.Dto;
using EasyBooking.Infrastructure.Database;
using MediatR;

namespace EasyBooking.Api.Handlers;

public class GetEmployeeByIdCommandHandler : IRequestHandler<GetEmployeeByIdCommand, EmployeeDto>
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public GetEmployeeByIdCommandHandler(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EmployeeDto> Handle(GetEmployeeByIdCommand request, CancellationToken cancellationToken)
    {
        var employee = _context.Desks.FirstOrDefault(employee => employee.Id == request.EmployeeId);
        return _mapper.Map<EmployeeDto>(employee);
    }
}
