using AutoMapper;
using EasyBooking.Api.Commands;
using EasyBooking.Api.Dto;
using EasyBooking.Domain.Model;
using EasyBooking.Infrastructure.Database;
using MediatR;

namespace EasyBooking.Api.Handlers;

public class RemoveEmployeeCommandHandler : IRequestHandler<RemoveEmployeeCommand, EmployeeDto>
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public RemoveEmployeeCommandHandler(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<EmployeeDto> Handle(RemoveEmployeeCommand request, CancellationToken cancellationToken)
    {
        var employee = _context.Employees.FirstOrDefault(employee => employee.Id == request.EmployeeId);
        if (employee == null)
        {
            throw new ArgumentException("cannot remove non existend employee");
        }
        bool hasReservations = _context.Reservations
            .Any(reservation => reservation.ReservingEmployeeId == request.EmployeeId);
        if (hasReservations) 
        {
            throw new ArgumentException("cannot remove employee with reservations");
        }
        _context.Employees.Remove(employee);
        await _context.SaveChangesAsync();
        return _mapper.Map<EmployeeDto>(employee);
    }
}
