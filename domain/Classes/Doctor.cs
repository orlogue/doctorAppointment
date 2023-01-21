using domain.Logic;
using System.Xml.Linq;

namespace domain.Classes;

public class Doctor
{
    public int Id;
    public string FullName;
    public Specialty Specialty;

    public Doctor() : this(0, "", new Specialty()) {}

    public Doctor(int id, string fullName, Specialty specialty)
    {
        Id = id;
        FullName = fullName;
        Specialty = specialty;
    }

    public Result IsValid()
    {
        if (Id < 0)
            return Result.Fail("Invalid id");

        if (string.IsNullOrEmpty(FullName))
            return Result.Fail("Invalid full name");

        var res = Specialty.IsValid();

        if (res.IsFailure)
            return Result.Fail("Invalid specialty: " + res.Error.ToLower());

        return Result.Ok();
    }
}
