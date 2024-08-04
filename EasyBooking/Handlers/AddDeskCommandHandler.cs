using AutoMapper;
using EasyBooking.Api.Dto;
using EasyBooking.Commands;
using EasyBooking.Domain.Model;
using EasyBooking.Dto;
using EasyBooking.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyBooking.Api.Handlers;

public class AddDeskCommandHandler : IRequestHandler<AddDeskCommand, DeskDto>
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public AddDeskCommandHandler(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<DeskDto> Handle(AddDeskCommand request, CancellationToken cancellationToken)
    {
        var officeLocation = _context.OfficeLocations.Include(officeLocation => officeLocation.Desks).FirstOrDefault(officeLocation => officeLocation.Id == request.OfficeLocationId);
        if (officeLocation == null)
        {
            throw new Exception("cannot add desk to non existent office");
        }
        var desk = new Desk();
        officeLocation.AddDesk(desk);
        await _context.SaveChangesAsync();
        return _mapper.Map<DeskDto>(desk);
    }
}
