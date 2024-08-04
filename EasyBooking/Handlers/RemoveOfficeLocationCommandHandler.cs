using AutoMapper;
using EasyBooking.Api.Commands;
using EasyBooking.Commands;
using EasyBooking.Domain.Model;
using EasyBooking.Dto;
using EasyBooking.Infrastructure.Database;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EasyBooking.Api.Handlers;

public class RemoveOfficeLocationCommandHandler : IRequestHandler<RemoveOfficeLocationCommand, Unit>
{
    private readonly DatabaseContext _context;
    private readonly IMapper _mapper;

    public RemoveOfficeLocationCommandHandler(DatabaseContext context, IMapper mapper)
    {
        _context = context;
        _mapper = mapper;
    }

    public async Task<Unit> Handle(RemoveOfficeLocationCommand request, CancellationToken cancellationToken)
    {
        var officeLocation = _context.OfficeLocations.Include(officeLocation => officeLocation.Desks).FirstOrDefault(officeLocation => officeLocation.Id == request.OfficeLocationId);
        if (officeLocation == null)
        {
            throw new ArgumentException("cannot remove non existend location");
        }
        if (officeLocation.Desks.Count > 0)
        {
            throw new ArgumentException("cannot remove location with desks");
        }
        _context.OfficeLocations.Remove(officeLocation);
        await _context.SaveChangesAsync();
        return Unit.Value;
    }



}