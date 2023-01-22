using domain.Classes;

namespace domain.Logic.Interfaces;

public interface IUserRepository : IRepository<User>
{
    User? GetUserByLogin(string login);
    bool DoesUserExist(string username);
}