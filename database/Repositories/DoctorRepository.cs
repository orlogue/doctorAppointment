using domain.Logic.Interfaces;
using domain.Classes;
using database.Models;
using database.Converters;
using Microsoft.EntityFrameworkCore;

namespace database.Repositories;

public class DoctorRepository : IDoctorRepository
{
    private readonly ApplicationContext _context;
    private readonly DbSet<DoctorModel> _dbSet;

    public DoctorRepository(ApplicationContext context)
    {
        _context = context;
        _dbSet = context.Doctors;
    }

    public async Task<Doctor?> GetItem(int id)
    {
        var doctor = await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.Id == id);
        return doctor?.ToDomain();
    }
    public async Task<IEnumerable<Doctor>> GetItemsList() =>
        await _dbSet.AsNoTracking().Select(item => item.ToDomain()).ToListAsync();

    public async Task<bool> Create(Doctor item)
    {
        await _dbSet.AddAsync(item.ToModel());
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(Doctor item)
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

    public async Task<IEnumerable<Doctor?>> FindDoctorsBySpecialty(int specialtyId) =>
        await _dbSet
            .AsNoTracking()
            .Where(doc => doc.SpecialtyId == specialtyId)
            .Select(it => it.ToDomain())
            .ToListAsync();

    public async Task<Doctor?> FindDoctor(int specialtyId)
    {
        var doctor = await _dbSet.FirstOrDefaultAsync(doc => doc.SpecialtyId == specialtyId);
        return doctor?.ToDomain();
    }
}