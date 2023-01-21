using domain.Logic;
namespace domain.Classes;

public class Specialty
{
    public int Id;
    public string Name;

    public Specialty() : this(0, "") {}

    public Specialty(int id, string name)
    {
        Id = id;
        Name = name;
    }

    public Result IsValid()
    {
        if (Id < 0)
            return Result.Fail("Invalid id");

        if (string.IsNullOrEmpty(Name))
            return Result.Fail("Invalid name");

        return Result.Ok();
    }
}