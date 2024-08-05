using AutoMapper;
using EasyBooking.Api.Commands;
using EasyBooking.Api.Dto;
using EasyBooking.Commands;
using EasyBooking.Domain.Model;
using EasyBooking.Dto;
using EasyBooking.Infrastructure.Database;
using MediatR;

namespace EasyBooking.Api.Handlers;

public class ReserveDeskCommandHandler : IRequestHandler<ReserveDeskCommand, ReservationDto>
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public ReserveDeskCommandHandler(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<ReservationDto> Handle(ReserveDeskCommand request, CancellationToken cancellationToken)
    {// start + 7 < stop
        // stop - start < 7
        //  start +7 < 
        //  start +7 < stop
        if (request.FromDate.AddDays(7) < request.ToDate) 
        {
            throw new ArgumentException("cannot create reservation for more than 7 days");
        }
        bool hasReservations = _context.Reservations.Any(reservation => 
            reservation.ReservedDeskId == request.ReservedDeskId &&
            reservation.FromDate < request.ToDate &&
            reservation.ToDate > request.FromDate
        );
        if(hasReservations)
        {
            throw new ArgumentException("cannot create reservation, desk is already reserved in this time period");
        }
        var reservation = new Reservation(request.ReservedDeskId,request.ReservingEmployeeId,request.FromDate,request.ToDate);
        _context.Reservations.Add(reservation);
        _context.SaveChanges(); 
        return _mapper.Map<ReservationDto>(reservation);
    }
}
