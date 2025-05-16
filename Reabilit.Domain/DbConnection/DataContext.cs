using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Reabilit.Domain.DbConnection.DatabaseConfigurations;
using Reabilit.Domain.Entities;

namespace Reabilit.Domain.DbConnection;

public class DataContext : IdentityDbContext<AppUser, AppRole, Guid>
{
    public DataContext(DbContextOptions<DataContext> opts) : base(opts) { }


    public DbSet<Analyze> Analyzes { get; set; }
    public DbSet<City> Cities { get; set; }
    public DbSet<Doctor> Doctors { get; set; }
    public DbSet<Patient> Patients { get; set; }
    public DbSet<DoctorClass> DoctorClasses { get; set; }
    public DbSet<ProcedureEvent> ProcedureEvents { get; set; }
    public DbSet<DoctorSchedule> DoctorSchedules { get; set; }
    public DbSet<Banner> Banners { get; set; }
    public DbSet<ProcedureEventNotification> ProcedureEventNotification { get; set; }
    public DbSet<TreatmentNotification> TreatmentNotification { get; set; }
    public DbSet<MessageNotification> MessageNotification { get; set; }
    public DbSet<ChatMessage> ChatMessages { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.ApplyConfiguration(new PatientConfiguration());
        modelBuilder.ApplyConfiguration(new ChatMessageConfiguration());
        modelBuilder.ApplyConfiguration(new DoctorConfiguration());
        modelBuilder.ApplyConfiguration(new ProcedureEventConfiguration());
    }
} 