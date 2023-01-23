using database.Models;
using domain.Classes;

namespace database.Converters;

public static class UserModelConverter
{
    public static User ToDomain(this UserModel model)
    {
        return new User
        {
            Id = model.Id,
            PhoneNumber = model.PhoneNumber,
            FullName = model.FullName,
            Role = model.Role,
            Username = model.Username,
            Password = model.Password
        };
    }

    public static UserModel ToModel(this User model)
    {
        return new UserModel
        {
            PhoneNumber = model.PhoneNumber,
            FullName = model.FullName,
            Role = model.Role,
            Username = model.Username,
            Password = model.Password
        };
    }
}

