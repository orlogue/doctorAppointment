using database.Models;
using Microsoft.EntityFrameworkCore;
namespace database;

public class ApplicationContext : DbContext
{
    public DbSet<AppointmentModel> Appointments { get; set; }
    public DbSet<DoctorModel> Doctors { get; set; }
    public DbSet<ScheduleModel> Schedules { get; set; }
    public DbSet<SpecialtyModel> Specialties { get; set; }
    public DbSet<UserModel> Users { get; set; }

    public ApplicationContext(DbContextOptions options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<AppointmentModel>().HasIndex(model => model.PatientId);
        modelBuilder.Entity<AppointmentModel>().HasIndex(model => model.DoctorId);
        modelBuilder.Entity<UserModel>().HasIndex(model => model.Username);
        modelBuilder.Entity<ScheduleModel>().HasIndex(model => model.DoctorId);
    }
}