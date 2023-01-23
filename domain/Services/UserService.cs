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

    public async Task<Result<User>> Register(User user) 
    {
        var result = user.IsValid();

        if (result.IsFailure)
            return Result.Fail<User>(result.Error);

        if (await _db.DoesUserExist(user.Username))
            return Result.Fail<User>("User with this username already exists");

        return await _db.Create(user) ? Result.Ok(user) :
            Result.Fail<User>("Unable to create user");
    }

    public async Task<Result<User>> GetUserByLogin(string login)
    {
        if (string.IsNullOrEmpty(login))
        {
            return Result.Fail<User>("Empty username");
        }

        var user = await _db.GetUserByLogin(login);

        return user is null ? Result.Fail<User>("User not found")
            : Result<User>.Ok(user);
    }

    public async Task<Result> DoesUserExist(string username)
    {
        if (string.IsNullOrEmpty(username))
        {
            return Result.Fail<User>("Empty username");
        }

        return await _db.DoesUserExist(username) ? Result.Ok()
            : Result.Fail("User does not exist");
    }
}