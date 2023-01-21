using domain.Logic;
using domain.Logic.Interfaces;
using domain.Classes;

public class ScheduleService
{
	private readonly IScheduleRepository _db;
	public ScheduleService(IScheduleRepository db)
    {
		_db = db;
	}

	public Result<IEnumerable<Schedule>> GetSchedule(Doctor doctor)
	{
		var doctorResult = doctor.IsValid();
		if (doctorResult.IsFailure)
			return Result.Fail<IEnumerable<Schedule>>("Invalid doctor: "
				+ doctorResult.Error.ToLower());

		return Result.Ok(_db.GetSchedule(doctor));
	}

	public Result CreateSchedule(Doctor doctor, Schedule schedule)
	{
		var doctorResult = doctor.IsValid();
        if (doctorResult.IsFailure)
            return Result.Fail("Invalid doctor: " + doctorResult.Error.ToLower());

        var scheduleResult = schedule.IsValid();
        if (scheduleResult.IsFailure)
            return Result.Fail("Invalid schedule: " + scheduleResult.Error.ToLower());

		return _db.CreateSchedule(doctor, schedule) ? Result.Ok()
			: Result.Fail("Unable to create schedule");
    }

    public Result UpdateSchedule(Doctor doctor, Schedule schedule)
    {
        var doctorResult = doctor.IsValid();
        if (doctorResult.IsFailure)
            return Result.Fail("Invalid doctor: " + doctorResult.Error);

        var scheduleResult = schedule.IsValid();
        if (scheduleResult.IsFailure)
            return Result.Fail("Invalid schedule: " + scheduleResult.Error);

        return _db.UpdateSchedule(doctor, schedule) ? Result.Ok(schedule)
            : Result.Fail<Schedule>("Unable to update schedule");
    }
}