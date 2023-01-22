// Пытался сделать generic repository, но возникли трудности с конвертацией

//using domain.Logic.Interfaces;
//using database.Models;
//using database.Converters;
//using Microsoft.EntityFrameworkCore;

//namespace database.Repositories;

//public class Repository<T, M> : IRepository<T>
//    where T : class
//    where M : class, IModel
//{
//    private readonly ApplicationContext _context;
//    private readonly DbSet<M> _dbSet;
    
//    public Repository(ApplicationContext context)
//    {
//        _context = context;
//        _dbSet = context.Set<M>();
//    }

//    public T? GetItem(int id)
//    {
//        var item = _dbSet.FirstOrDefault(item => item.Id == id);
//        return item?.ToDomain();
//    }

//    public IEnumerable<T> GetItemsList()
//    {
//        var items = _dbSet.Select(item => item.ToDomain());
//        return items;
//    }

//    public bool Create(T item)
//    {
//        _dbSet.Add(item.ToModel());
//        Save();
//        return true;
//    }

//    public bool Update(T item)
//    {
//        _dbSet.Update(item.ToModel()!);
//        Save();
//        return true;
//    }

//    public bool Delete(int id)
//    {
//        var item = GetItem(id);
//        if (item == default)
//            return false;

//        _dbSet.Remove(item.ToModel()!);
//        Save();
//        return true;
//    }

//    public void Save()
//    {
//        _context.SaveChanges();
//    }
//}