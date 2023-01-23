using database.Models;
using domain.Classes;

namespace database.Converters;

public static class SpecialtyModelConverter
{
    public static Specialty ToDomain(this SpecialtyModel model)
    {
        return new Specialty
        {
            Id = model.Id,
            Name = model.Name
        };
    }

    public static SpecialtyModel ToModel(this Specialty model)
    {
        return new SpecialtyModel
        {
            Name = model.Name
        };
    }
}

