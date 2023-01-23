using domain.Logic;
using domain.Logic.Interfaces;
using domain.Classes;

namespace domain.Services;

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

		return Result.Ok(_db.GetScheduleByDoctor(doctor.Id));
	}

	public Result CreateSchedule(Schedule schedule)
	{
        var scheduleResult = schedule.IsValid();
        if (scheduleResult.IsFailure)
            return Result.Fail("Invalid schedule: " + scheduleResult.Error.ToLower());

		return _db.Create(schedule) ? Result.Ok()
			: Result.Fail("Unable to create schedule");
    }

    public Result UpdateSchedule(Schedule schedule)
    {
        var scheduleResult = schedule.IsValid();
        if (scheduleResult.IsFailure)
            return Result.Fail("Invalid schedule: " + scheduleResult.Error);

        return _db.Update(schedule) ? Result.Ok(schedule)
            : Result.Fail<Schedule>("Unable to update schedule");
    }
}