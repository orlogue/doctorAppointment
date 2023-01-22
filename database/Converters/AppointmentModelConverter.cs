
using database.Models;
using domain.Classes;

namespace database.Converters;

public static class AppointmentModelConverter
{
    public static Appointment ToDomain<M>(this M model) where M : AppointmentModel
    {
        return new Appointment
        {
            Id = model.Id,
            PatientId = model.PatientId,
            DoctorId = model.DoctorId,
            StartTime = model.StartTime,
            EndTime = model.EndTime
        };
    }

    public static AppointmentModel ToModel(this Appointment model)
    {
        return new AppointmentModel
        {
            Id = model.Id,
            PatientId = model.PatientId,
            DoctorId = model.DoctorId,
            StartTime = model.StartTime,
            EndTime = model.EndTime
        };
    }
}