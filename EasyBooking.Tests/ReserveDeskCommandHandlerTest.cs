using AutoMapper;
using Moq;
using Microsoft.EntityFrameworkCore;
using EasyBooking.Infrastructure.Database;
using EasyBooking.Api.Handlers;
using EasyBooking.Api.Commands;
using EasyBooking.Domain.Model;

public class ReserveDeskCommandHandlerTests
{
    private readonly Mock<DatabaseContext> _mockContext;
    private readonly Mock<IMapper> _mockMapper;
    private readonly ReserveDeskCommandHandler _handler;

    public ReserveDeskCommandHandlerTests()
    {
        _mockContext = new Mock<DatabaseContext>();
        _mockMapper = new Mock<IMapper>();
        _handler = new ReserveDeskCommandHandler(_mockContext.Object, _mockMapper.Object);
    }

    [Fact]
    public async Task Handle_ReservationLongerThan7Days_ShouldThrowArgumentException()
    {
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
        // Arrange
        var command = new ReserveDeskCommand
        (
            1,
            1,
            DateOnly.FromDateTime(DateTime.Now),
            DateOnly.FromDateTime(DateTime.Now).AddDays(3)
        );

        var reservations = new List<Reservation>
        {
            new Reservation ( 1,1, DateOnly.FromDateTime(DateTime.Now).AddDays(1), DateOnly.FromDateTime(DateTime.Now).AddDays(4) )
        }.AsQueryable();

        var mockSet = new Mock<DbSet<Reservation>>();
        mockSet.As<IQueryable<Reservation>>().Setup(m => m.Provider).Returns(reservations.Provider);
        mockSet.As<IQueryable<Reservation>>().Setup(m => m.Expression).Returns(reservations.Expression);
        mockSet.As<IQueryable<Reservation>>().Setup(m => m.ElementType).Returns(reservations.ElementType);
        mockSet.As<IQueryable<Reservation>>().Setup(m => m.GetEnumerator()).Returns(reservations.GetEnumerator());

        _mockContext.Setup(c => c.Reservations).Returns(mockSet.Object);

        // Act & Assert
        var exception = await Assert.ThrowsAsync<ArgumentException>(() =>
            _handler.Handle(command, CancellationToken.None));
        Assert.Equal("cannot create reservation, desk is already reserved in this time period", exception.Message);
    }
}