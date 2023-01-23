namespace domain.Logic.Interfaces;

public interface IRepository<T> where T : class
{
    Task<IEnumerable<T>> GetItemsList();
    Task<T?> GetItem(int id);
    Task<bool> Create(T item);
    Task<bool> Update(T item);
    Task<bool> Delete(int id);
    void Save();
}