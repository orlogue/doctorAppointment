using database.Models;
using domain.Classes;

namespace database.Converters;

public static class ScheduleModelConverter
{
    public static Schedule ToDomain(this ScheduleModel model)
    {
        return new Schedule
        {
            Id = model.Id,
            DoctorId = model.DoctorId,
            WorkdayStartTime = model.WorkdayStartTime,
            WorkdayEndTime = model.WorkdayEndTime
        };
    }

    public static ScheduleModel ToModel(this Schedule model)
    {
        return new ScheduleModel
        {
            DoctorId = model.DoctorId,
            WorkdayStartTime = model.WorkdayStartTime,
            WorkdayEndTime = model.WorkdayEndTime
        };
    }
}

