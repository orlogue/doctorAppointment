namespace domain.Logic.Interfaces;

public interface IRepository<T> where T : class
{
    IEnumerable<T> GetItemList();
    T GetItem(int id);
    void Create(T item);
    void Update(T item);
    void Delete(int id);
    void Save();
}