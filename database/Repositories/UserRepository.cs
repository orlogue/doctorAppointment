using domain.Logic.Interfaces;
using domain.Classes;
using database.Models;
using database.Converters;
using Microsoft.EntityFrameworkCore;

namespace database.Repositories;

public class UserRepository : IUserRepository
{
    private readonly ApplicationContext _context;
    private readonly DbSet<UserModel> _dbSet;

    public UserRepository(ApplicationContext context)
    {
        _context = context;
        _dbSet = context.Users;
    }

    public User? GetItem(int id) =>
        _dbSet
            .AsNoTracking()
            .FirstOrDefault(item => item.Id == id)
            ?.ToDomain();

    public IEnumerable<User> GetItemsList() =>
        _dbSet
            .AsNoTracking()
            .Select(item => item.ToDomain());

    public bool Create(User item)
    {
        _dbSet.Add(item.ToModel());
        Save();
        return true;
    }

    public bool Update(User item)
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

    public User? GetUserByLogin(string login) =>
        _dbSet
            .AsNoTracking()
            .FirstOrDefault(user => user.Username.ToLower() == login.ToLower())
            ?.ToDomain();

    public bool DoesUserExist(string username) =>
        _dbSet
        .AsNoTracking()
        .Any(user => user.Username.ToLower() == username.ToLower())
        ? true
        : false;
}
