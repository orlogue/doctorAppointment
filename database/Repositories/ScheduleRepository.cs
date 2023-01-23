using domain.Logic.Interfaces;
using domain.Classes;
using database.Models;
using database.Converters;
using Microsoft.EntityFrameworkCore;

namespace database.Repositories;

public class ScheduleRepository : IScheduleRepository
{
    private readonly ApplicationContext _context;
    private readonly DbSet<ScheduleModel> _dbSet;

    public ScheduleRepository(ApplicationContext context)
    {
        _context = context;
        _dbSet = context.Schedules;
    }

    public async Task<Schedule?> GetItem(int id)
    {
        var schedule = await _dbSet.FirstOrDefaultAsync(item => item.Id == id);
        return schedule?.ToDomain();
    }

    public async Task<IEnumerable<Schedule>> GetItemsList() =>
        await _dbSet.Select(item => item.ToDomain()).ToListAsync();

    public async Task<bool> Create(Schedule item)
    {
        await _dbSet.AddAsync(item.ToModel());
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(Schedule item)
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

    public async Task<IEnumerable<Schedule>> GetScheduleByDoctor(int doctorId) =>
        await _dbSet
            .Where(it => it.DoctorId == doctorId)
            .Select(it => it.ToDomain())
            .ToListAsync();
}