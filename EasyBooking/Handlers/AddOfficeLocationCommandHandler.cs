using AutoMapper;
using EasyBooking.Commands;
using EasyBooking.Domain.Model;
using EasyBooking.Dto;
using EasyBooking.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyBooking.Handlers;

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
        _context.OfficeLocations.Add(new OfficeLocation (request.OfficeLocationDto.Id,request.OfficeLocationDto.LocationName));
        await _context.SaveChangesAsync();
        var x = _context.OfficeLocations.FirstOrDefault();
        return _mapper.Map<OfficeLocationDto>(x);
    }

}
