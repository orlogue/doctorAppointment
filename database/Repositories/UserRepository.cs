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

    public async Task<User?> GetItem(int id)
    {
        var user = await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(item => item.Id == id);
        return user?.ToDomain();
    }

    public async Task<IEnumerable<User>> GetItemsList() =>
        await _dbSet
            .AsNoTracking()
            .Select(item => item.ToDomain()).ToListAsync();

    public async Task<bool> Create(User item)
    {
        await _dbSet.AddAsync(item.ToModel());
        await _context.SaveChangesAsync();
        return true;
    }

    public async Task<bool> Update(User item)
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

    public async Task<User?> GetUserByLogin(string login)
    {
        var user = await _dbSet
            .AsNoTracking()
            .FirstOrDefaultAsync(user => user.Username.ToLower() == login.ToLower());
        return user?.ToDomain();
    }

    public async Task<bool> DoesUserExist(string username) =>
        await _dbSet
        .AsNoTracking()
        .AnyAsync(user => user.Username.ToLower() == username.ToLower())
        ? true
        : false;
}
