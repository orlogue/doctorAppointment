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

    public Schedule? GetItem(int id) =>
        _dbSet.FirstOrDefault(item => item.Id == id)?.ToDomain();

    public IEnumerable<Schedule> GetItemsList() =>
        _dbSet.Select(item => item.ToDomain());

    public bool Create(Schedule item)
    {
        _dbSet.Add(item.ToModel());
        Save();
        return true;
    }

    public bool Update(Schedule item)
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

    public IEnumerable<Schedule> GetScheduleByDoctor(int doctorId) =>
        _dbSet
            .Where(it => it.DoctorId == doctorId)
            .Select(it => it.ToDomain());
}