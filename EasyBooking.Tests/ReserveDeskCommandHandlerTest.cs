using AutoMapper;
using Microsoft.EntityFrameworkCore;
using EasyBooking.Infrastructure.Database;
using EasyBooking.Api.Handlers;
using EasyBooking.Api.Commands;
using EasyBooking.Domain.Model;
using EasyBooking.Api.Dto;
using EasyBooking.Api.Mappers;

public class ReserveDeskCommandHandlerTests
{
    private IMapper _mapper;   
    DbContextOptions<DatabaseContext> _options;

    public ReserveDeskCommandHandlerTests()
    {
        _options = new DbContextOptionsBuilder<DatabaseContext>()
                          .UseInMemoryDatabase(databaseName: "TestDatabase")
                          .Options;
        var config = new MapperConfiguration(cfg =>
        {
            cfg.AddProfile<MappingProfile>();
        });
        _mapper = config.CreateMapper();
    }

    [Fact]
    public async Task Handle_ReservationLongerThan7Days_ShouldThrowArgumentException()
    {
        var _context = new DatabaseContext(_options);
        var _handler = new ReserveDeskCommandHandler(_context, _mapper);
        // Arrange
        var command = new ReserveDeskCommand(
            1,
            1,
            DateOnly.FromDateTime(DateTime.Now),
            DateOnly.FromDateTime(DateTime.Now).AddDays(8)
        );

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _handler.Handle(command, CancellationToken.None));
        Assert.Equal("cannot create reservation for more than 7 days", exception.Message);
    }

    [Fact]
    public async Task Handle_DeskAlreadyReserved_ShouldThrowArgumentException()
    {
        var _context = new DatabaseContext(_options);
        var _handler = new ReserveDeskCommandHandler(_context, _mapper);
        // Arrange
        var command = new ReserveDeskCommand(
            1,
            1,
            DateOnly.FromDateTime(DateTime.Now),
            DateOnly.FromDateTime(DateTime.Now).AddDays(3)
        );

        var  conflictReservation = new Reservation(
            1,
            1,
            DateOnly.FromDateTime(DateTime.Now).AddDays(1),
            DateOnly.FromDateTime(DateTime.Now).AddDays(4)
        );

        // Seed the in-memory database with a conflicting reservation
        _context.Reservations.Add(conflictReservation);
        await _context.SaveChangesAsync();

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _handler.Handle(command, CancellationToken.None));
        Assert.Equal(
            "cannot create reservation, desk is already reserved in this time period", 
            exception.Message
        );
    }

    [Fact]
    public async Task Handle_DBEmpty_ShouldReturnReservationDto()
    {
        var _context = new DatabaseContext(_options);
        var _handler = new ReserveDeskCommandHandler(_context, _mapper); 
        // Arrange
        var command = new ReserveDeskCommand
        (
            1,
            1,
            DateOnly.FromDateTime(DateTime.Now),
            DateOnly.FromDateTime(DateTime.Now).AddDays(3)
        );

        Assert.Equivalent(
            new ReservationDto() {
                Id = 1,
                ReservingEmployeeId = 1,
                ReservedDeskId = 1,
                FromDate = DateOnly.FromDateTime(DateTime.Now),
                ToDate= DateOnly.FromDateTime(DateTime.Now).AddDays(3)
            }, 
            await _handler.Handle(command, CancellationToken.None)
        );
    }
}