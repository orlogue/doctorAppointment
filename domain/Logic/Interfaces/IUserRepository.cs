using domain.Classes;

namespace domain.Logic.Interfaces;

public interface IUserRepository : IRepository<User>
{
    Task<User?> GetUserByLogin(string login);
    Task<bool> DoesUserExist(string username);
}