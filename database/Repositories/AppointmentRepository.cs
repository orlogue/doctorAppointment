using domain.Logic.Interfaces;
using domain.Classes;
using database.Models;
using database.Converters;
using Microsoft.EntityFrameworkCore;

namespace database.Repositories;

public class AppointmentRepository : IAppointmentRepository
{
    private readonly ApplicationContext _context;
    private readonly DbSet<AppointmentModel> _dbSet;

    public AppointmentRepository(ApplicationContext context)
    {
        _context = context;
        _dbSet = context.Appointments;
    }

    public Appointment? GetItem(int id) =>
        _dbSet.FirstOrDefault(item => item.Id == id)?.ToDomain();

    public IEnumerable<Appointment> GetItemsList() =>
        _dbSet.Select(item => item.ToDomain());

    public bool Create(Appointment item)
    {
        _dbSet.Add(item.ToModel());
        Save();
        return true;
    }

    public bool Update(Appointment item)
    {
        _dbSet.Update(item.ToModel());
        Save();
        return true;
    }

    public bool Delete(int id)
    {
        var item = GetItem(id);
        if (item == default)
            return false;

        _dbSet.Remove(item.ToModel());
        Save();
        return true;
    }

    public void Save()
    {
        _context.SaveChanges();
    }


    public IEnumerable<Appointment> GetAppointments(int doctorId) =>
        _dbSet.Where(item => item.DoctorId == doctorId).Select(item => item.ToDomain());


    public IEnumerable<DateTime> GetFreeAppointments(Specialty specialty)
    {
        var spec = specialty.ToModel();
        var doctors = _context.Doctors.Where(doc => doc.Specialty == spec)
            .Select(doc => doc.ToDomain());
        return _dbSet.Where(item => item.StartTime > DateTime.Now && doctors.Any(doc
            => doc.Id == item.DoctorId)).Select(it => it.StartTime);
    }
}