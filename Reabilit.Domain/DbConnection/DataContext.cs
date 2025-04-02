using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.Entities;

namespace Reabilit.Domain.DbConnection;

public class DataContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DataContext(DbContextOptions<DataContext> opt) : base(opt) { }


    public DbSet<Analyze> Analyzes { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
} 