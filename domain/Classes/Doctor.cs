using domain.Logic;
using System.Xml.Linq;

namespace domain.Classes;

public class Doctor
{
    public int Id { get; set; }
    public string FullName { get; set; }
    public int SpecialtyId { get; set; }

    public Doctor() : this(0, "", 0) {}

    public Doctor(int id, string fullName, int specialtyId)
    {
        Id = id;
        FullName = fullName;
        SpecialtyId = specialtyId;
    }

    public Result IsValid()
    {
        if (Id < 0)
            return Result.Fail("Invalid id");

        if (string.IsNullOrEmpty(FullName))
            return Result.Fail("Invalid full name");

        if (SpecialtyId < 0)
            return Result.Fail("Invalid specialty id");

        return Result.Ok();
    }
}
