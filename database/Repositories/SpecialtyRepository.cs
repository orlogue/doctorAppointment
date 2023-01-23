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

    public Specialty? GetItem(int id) =>
        _dbSet
            .AsNoTracking()
            .FirstOrDefault(item => item.Id == id)?.ToDomain();

    public IEnumerable<Specialty> GetItemsList() =>
        _dbSet
            .AsNoTracking()
            .Select(item => item.ToDomain());

    public bool Create(Specialty item)
    {
        _dbSet.Add(item.ToModel());
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

    public Specialty? GetByName(string name) =>
        _dbSet
            .AsNoTracking()
            .FirstOrDefault(
            it => it.Name.ToLower()
            .Contains(name.ToLower()))
            ?.ToDomain();
}