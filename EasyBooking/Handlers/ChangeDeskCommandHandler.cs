using AutoMapper;
using EasyBooking.Api.Commands;
using EasyBooking.Api.Dto;
using EasyBooking.Commands;
using EasyBooking.Domain.Model;
using EasyBooking.Infrastructure.Database;
using MediatR;

namespace EasyBooking.Api.Handlers;

public class ChangeDeskCommandHandler : IRequestHandler<ChangeReservationCommand, ReservationDto>
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public ChangeDeskCommandHandler(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReservationDto> Handle(ChangeReservationCommand request, CancellationToken cancellationToken)
    {
        if (request.FromDate.AddDays(7) > request.ToDate)
        {
            throw new ArgumentException("cannot create reservation for more than 7 days");
        }
        var reservation = _context.Reservations.Where(reservation => reservation.Id == request.ReservationId).FirstOrDefault();
        if (reservation == null ) 
        {
            throw new Exception("an reservation like this doesnt exist");
        }
        DateOnly currentDate = DateOnly.FromDateTime(DateTime.Now);
        if(currentDate.AddDays(1) >= reservation.FromDate)
        {
            throw new ArgumentException("reservations cannot be changed withing a 1 day notice period");
        }
        bool hasReservations = _context.Reservations.Any(reservation =>
            reservation.ReservedDeskId == request.ReservedDeskId &&
            reservation.FromDate < request.ToDate &&
            reservation.ToDate > request.FromDate
        );
        if(hasReservations )
        {
            throw new ArgumentException("cannot change the reservation, new time slot is already occupied");
        }
        reservation.ToDate = request.ToDate;
        reservation.FromDate = request.FromDate;
        reservation.ReservedDeskId = request.ReservedDeskId;
        _context.SaveChanges();
        return _mapper.Map<ReservationDto>( reservation );

    }

}
