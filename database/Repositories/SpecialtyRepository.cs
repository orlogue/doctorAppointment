using domain.Logic.Interfaces;
using domain.Classes;
using database.Models;
using database.Converters;
using Microsoft.EntityFrameworkCore;

namespace database.Repositories;

public class SpecialtyRepository : ISpecialtyRepository
{
    private readonly ApplicationContext _context;
    private readonly DbSet<SpecialtyModel> _dbSet;

    public SpecialtyRepository(ApplicationContext context)
    {
        _context = context;
        _dbSet = context.Specialties;
    }

    public async Task<Specialty?> GetItem(int id)
    {
        var schedule = await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.Id == id);
        return schedule?.ToDomain();
    }

    public async Task<IEnumerable<Specialty>> GetItemsList() =>
        await _dbSet
            .AsNoTracking()
            .Select(item => item.ToDomain())
            .ToListAsync();

    public async Task<bool> Create(Specialty item)
    {
        await _dbSet.AddAsync(item.ToModel());
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(Specialty item)
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

    public async Task<Specialty?> GetByName(string name)
    {
        var specialty = await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(
            it => it.Name.ToLower().Contains(name.ToLower()));
        return specialty?.ToDomain();
    }
}