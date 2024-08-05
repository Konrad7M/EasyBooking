using AutoMapper;
using EasyBooking.Api.Commands;
using EasyBooking.Api.Dto;
using EasyBooking.Commands;
using EasyBooking.Domain.Model;
using EasyBooking.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyBooking.Api.Handlers;

public class RemoveDeskCommandHandler : IRequestHandler<RemoveDeskCommand, DeskDto>
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public RemoveDeskCommandHandler(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DeskDto> Handle(RemoveDeskCommand request, CancellationToken cancellationToken)
    {
        var desk = _context.Desks.FirstOrDefault(desk => desk.Id == request.DeskId);
        if (desk == null)
        {
            throw new ArgumentException("cannot remove non existend desk");
        }
        bool hasReservations = _context.Reservations
            .Any(reservation => reservation.ReservedDeskId == request.DeskId);
        if (hasReservations)
        {
            throw new ArgumentException("cannot remove desk with reservations");
        }
        _context.Desks.Remove(desk);
        await _context.SaveChangesAsync();
        return _mapper.Map<DeskDto>(desk);
    }
}
