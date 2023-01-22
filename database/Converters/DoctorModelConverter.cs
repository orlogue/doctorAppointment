using database.Models;
using domain.Classes;

namespace database.Converters;

public static class DoctorModelConverter
{
    public static Doctor ToDomain(this DoctorModel model)
    {
        return new Doctor
        {
            Id = model.Id,
            FullName = model.FullName,
            Specialty = model.Specialty.ToDomain()
        };
    }

    public static DoctorModel ToModel(this Doctor model)
    {
        return new DoctorModel
        {
            Id = model.Id,
            FullName = model.FullName,
            Specialty = model.Specialty.ToModel()
        };
    }
}

