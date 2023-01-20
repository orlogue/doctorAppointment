using domain.Classes;
using domain.Logic;
using domain.Logic.Interfaces;

namespace domain.Services;

public class UserService
{
    private readonly IUserRepository _db;

    public UserService(IUserRepository db)
    {
        _db = db;
    }

    public Result<User> Register(User user) 
    {
        var result = user.IsValid();

        if (result.IsFailure)
            return Result.Fail<User>(result.Error);

        if (_db.DoesUserExist(user.Username))
            return Result.Fail<User>("User with this username already exists");

        return _db.Create(user) ? Result.Ok(user) :
            Result.Fail<User>("Unable to create user");
    }

    public Result<User> GetUserByLogin(string login)
    {
        if (string.IsNullOrEmpty(login))
        {
            return Result.Fail<User>("Empty username");
        }

        var user = _db.GetUserByLogin(login);

        return user is null ? Result.Fail<User>("User not found")
            : Result<User>.Ok(user);
    }

    public Result DoesUserExist(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            return Result.Fail<User>("Empty username");
        }

        return _db.DoesUserExist(username) ? Result.Ok()
            : Result.Fail("User does not exist");
    }
}