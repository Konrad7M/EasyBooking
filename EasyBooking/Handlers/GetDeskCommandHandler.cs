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
        List<DeskDto> desks = null;
        desks = _context.OfficeLocations
            .Where(location => location.Id == query.LocationId)
            .SelectMany(location => location.Desks)
            .Select(desk => new DeskDto
            {
                Id = desk.Id,
                IsAvailable = !_context.Reservations
                   .Any(reservation => reservation.ReservedDeskId == desk.Id &&
                                       reservation.FromDate < query.To &&
                                       reservation.ToDate > query.From)
            }).ToList();
        if (query.ShowAvailableOnly) {
            desks.RemoveAll(desk =>  desk.IsAvailable == false);
        }

        return desks;

    }
}