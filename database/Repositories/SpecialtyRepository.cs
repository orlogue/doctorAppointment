using domain.Logic.Interfaces;
using domain.Classes;
using database.Models;
using database.Converters;
using Microsoft.EntityFrameworkCore;

namespace database.Repositories;

public class SpecialtyRepository : IRepository<Specialty>
{
    private readonly ApplicationContext _context;
    private readonly DbSet<SpecialtyModel> _dbSet;

    public SpecialtyRepository(ApplicationContext context)
    {
        _context = context;
        _dbSet = context.Specialties;
    }

    public Specialty? GetItem(int id) =>
        _dbSet.FirstOrDefault(item => item.Id == id)?.ToDomain();

    public IEnumerable<Specialty> GetItemsList() =>
        _dbSet.Select(item => item.ToDomain());

    public bool Create(Specialty item)
    {
        _dbSet.AddAsync(item.ToModel());
        Save();
        return true;
    }

    public bool Update(Specialty item)
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
}