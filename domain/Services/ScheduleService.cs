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

	public async Task<Result<IEnumerable<Schedule>>> GetSchedule(Doctor doctor)
	{
		var doctorResult = doctor.IsValid();
		if (doctorResult.IsFailure)
			return Result.Fail<IEnumerable<Schedule>>("Invalid doctor: "
				+ doctorResult.Error.ToLower());

		return Result.Ok(await _db.GetScheduleByDoctor(doctor.Id));
	}

	public async Task<Result> CreateSchedule(Schedule schedule)
	{
        var scheduleResult = schedule.IsValid();
        if (scheduleResult.IsFailure)
            return Result.Fail("Invalid schedule: " + scheduleResult.Error.ToLower());

		return await _db.Create(schedule) ? Result.Ok()
			: Result.Fail("Unable to create schedule");
    }

    public async Task<Result> UpdateSchedule(Schedule schedule)
    {
        var scheduleResult = schedule.IsValid();
        if (scheduleResult.IsFailure)
            return Result.Fail("Invalid schedule: " + scheduleResult.Error);

        return await _db.Update(schedule) ? Result.Ok(schedule)
            : Result.Fail<Schedule>("Unable to update schedule");
    }
}