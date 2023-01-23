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

    public Doctor? GetItem(int id) =>
        _dbSet
            .AsNoTracking()
            .FirstOrDefault(item => item.Id == id)
            ?.ToDomain();

    public IEnumerable<Doctor> GetItemsList() =>
        _dbSet.AsNoTracking().Select(item => item.ToDomain());

    public bool Create(Doctor item)
    {
        _dbSet.Add(item.ToModel());
        Save();
        return true;
    }

    public bool Update(Doctor item)
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

    public IEnumerable<Doctor?> FindDoctorsBySpecialty(int specialtyId) =>
        _dbSet
            .AsNoTracking()
            .Where(doc => doc.SpecialtyId == specialtyId)
            .Select(it => it.ToDomain());

    public Doctor? FindDoctor(int specialtyId) =>
        _dbSet.FirstOrDefault(doc => doc.SpecialtyId == specialtyId)?.ToDomain();
}