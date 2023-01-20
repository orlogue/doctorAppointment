using domain.Logic;

namespace domain.Classes;

public class User
{
    public int Id;
    public string PhoneNumber;
    public string FullName;
    public Role Role;

    public string Username;
    public string Password;

    public User() : this(0, "", "", Role.Patient, "", "") {}
    
    public User(int id, string phoneNumber, string fullName, Role role,
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