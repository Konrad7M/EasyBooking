using AutoMapper;
using EasyBooking.Api.Commands;
using EasyBooking.Api.Dto;
using EasyBooking.Commands;
using EasyBooking.Domain.Model;
using EasyBooking.Dto;
using EasyBooking.Infrastructure.Database;
using MediatR;

namespace EasyBooking.Api.Handlers;

public class GetDeskCommandHandler : IRequestHandler<GetDesksCommand, List<DeskDto>>
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public GetDeskCommandHandler(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<List<DeskDto>> Handle(GetDesksCommand query, CancellationToken cancellationToken)
    {
        var availableDesks = _context.OfficeLocations
            .Where(location => location.Id == query.LocationId)
            .SelectMany(location => location.Desks)
            .GroupJoin(
                _context.Reservations,
                desk => desk.Id,
                reservation => reservation.ReservedDeskId,
                (desk, reservations) => new { desk, reservations }
            )
            .SelectMany(
                x => x.reservations.DefaultIfEmpty(),
                (x, reservation) => new { x.desk, reservation }
            )
            .Where(x => x.reservation == null ||
                        !(x.reservation.FromDate < query.To && x.reservation.ToDate > query.From))
            .Select(x => x.desk)
            .Distinct()
            .ToList();
        return _mapper.Map<List<DeskDto>>(availableDesks);

    }
}