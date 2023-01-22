namespace domain.Logic.Interfaces;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetItemsList();
    T? GetItem(int id);
    bool Create(T item);
    bool Update(T item);
    bool Delete(int id);
    void Save();
}