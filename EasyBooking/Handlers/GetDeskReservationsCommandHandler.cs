using AutoMapper;
using EasyBooking.Api.Commands;
using EasyBooking.Api.Dto;
using EasyBooking.Commands;
using EasyBooking.Domain.Model;
using EasyBooking.Dto;
using EasyBooking.Infrastructure.Database;
using MediatR;

namespace EasyBooking.Api.Handlers;

public class GetDeskReservationsCommandHandler : IRequestHandler<GetDeskReservationsCommand, List<ReservationDto>>
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public GetDeskReservationsCommandHandler(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<ReservationDto>> Handle(GetDeskReservationsCommand query, CancellationToken cancellationToken)
    {
        if (!query.IsAdmin)
        {
            // I lacked time to add exception middleware to return http codes and proper credential validation
            throw new ArgumentException("acces denied");
        }
        var reservations = _context.Reservations.Where(resersavation => resersavation.ReservedDeskId == query.DeskId);
        return _mapper.Map<List<ReservationDto>>(reservations);        
    }
}