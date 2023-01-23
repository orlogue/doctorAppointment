using Microsoft.EntityFrameworkCore;

using domain.Logic.Interfaces;
using domain.Classes;
using database.Models;
using database.Converters;

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

    public async Task<Appointment?> GetItem(int id)
    {
        var appointemnt = await
            _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.Id == id);
        return appointemnt?.ToDomain();
    }
        
    
    public async Task<IEnumerable<Appointment>> GetItemsList() =>
        await _dbSet
            .AsNoTracking()
            .Select(item => item.ToDomain())
            .ToListAsync();

    public async Task<bool> Create(Appointment item)
    {
        await _dbSet.AddAsync(item.ToModel());
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(Appointment item)
    {
        _dbSet.Update(item.ToModel());
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Delete(int id)
    {
        var item = await GetItem(id);
        if (item == default)
            return false;

        _dbSet.Remove(item.ToModel());
        await _context.SaveChangesAsync();
        return true;
    }

    public async void Save()
    {
        await _context.SaveChangesAsync();
    }


    public async Task<IEnumerable<Appointment>> GetAppointments(int doctorId) =>
        await _dbSet
            .Where(item => item.DoctorId == doctorId)
            .Select(item => item.ToDomain())
            .ToListAsync();


    public async Task<IEnumerable<DateTime>> GetFreeAppointments(Specialty specialty)
    {
        var spec = specialty.ToModel();
        var doctors = _context.Doctors
            .AsNoTracking()
            .Where(doc => doc.SpecialtyId == spec.Id)
            .Select(doc => doc.ToDomain());
        return await _dbSet
            .AsNoTracking()
            .Where(item => item.StartTime > DateTime.Now && doctors.Any(doc
            => doc.Id == item.DoctorId))
            .Select(it => it.StartTime)
            .ToListAsync();
    }
}