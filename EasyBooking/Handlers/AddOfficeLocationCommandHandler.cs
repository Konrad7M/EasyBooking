using AutoMapper;
using EasyBooking.Commands;
using EasyBooking.Domain.Model;
using EasyBooking.Dto;
using EasyBooking.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyBooking.Api.Handlers;

public class AddOfficeLocationCommandHandler : IRequestHandler<AddOfficeLocationCommand, OfficeLocationDto>
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public AddOfficeLocationCommandHandler(DatabaseContext context, IMapper mapper) 
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<OfficeLocationDto> Handle(AddOfficeLocationCommand request, CancellationToken cancellationToken)
    {
        var officeLocation = new OfficeLocation(request.OfficeLocationDto.LocationName);
        request.OfficeLocationDto.Desks.ForEach(deskDto => officeLocation.AddDesk(new Desk(deskDto.IsAvailable)));
        _context.OfficeLocations.Add(officeLocation);
        await _context.SaveChangesAsync();
        return _mapper.Map<OfficeLocationDto>(officeLocation);
        // Todo
    }

}
