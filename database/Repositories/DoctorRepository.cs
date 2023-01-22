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
        _dbSet.FirstOrDefault(item => item.Id == id)?.ToDomain();

    public IEnumerable<Doctor> GetItemsList() =>
        _dbSet.Select(item => item.ToDomain());

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

    public IEnumerable<Doctor?> FindDoctorsBySpecialty(Specialty specialty) =>
        _dbSet.Where(doc => doc.Specialty == specialty.ToModel()).Select(it => it.ToDomain());

    public Doctor? FindDoctor(Specialty specialty) =>
        _dbSet.FirstOrDefault(doc => doc.Specialty == specialty.ToModel())?.ToDomain();
}