using domain.Logic;

namespace domain.Classes;

public class User
{
    public Int64 Id { get; set; }
    public string PhoneNumber { get; set; }
    public string FullName { get; set; }
    public Role Role { get; set; }

    public string Username { get; set; }
    public string Password { get; set; }

    public User() : this(0, "", "", Role.Patient, "", "") {}
    
    public User(Int64 id, string phoneNumber, string fullName, Role role,
        string userName, string password)
    {
        Id = id;
        PhoneNumber = phoneNumber;
        FullName = fullName;
        Role = role;
        Username = userName;
        Password = password;
    }

    public Result IsValid()
    {
        if (Id < 0)
            return Result.Fail("Invalid id");

        if (string.IsNullOrEmpty(Username))
            return Result.Fail("Invalid username");

        if (string.IsNullOrEmpty(Password))
            return Result.Fail("Invalid password");

        if (string.IsNullOrEmpty(PhoneNumber))
            return Result.Fail("Invalid phone number");

        if (string.IsNullOrEmpty(FullName))
            return Result.Fail("Invalid full name");

        return Result.Ok();
    }
}