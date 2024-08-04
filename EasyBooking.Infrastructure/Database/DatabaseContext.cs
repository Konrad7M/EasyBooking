using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using EasyBooking.Domain.Model;


namespace EasyBooking.Infrastructure.Database;

public class DatabaseContext: DbContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options)
        : base(options)
    {
    }

    public DbSet<OfficeLocation> OfficeLocations { get; set; } = null!;

    public DbSet<Reservation> Reservations { get; set; } = null!;

    public DbSet<Employee> Employees { get; set; } = null!;

    public DbSet<Desk> Desks { get; set; } = null!;
}
