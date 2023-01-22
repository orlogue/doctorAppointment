using domain.Logic;

namespace domain.Classes;

public class Schedule
{
    public int Id;
    public int DoctorId;
    public DateTime WorkdayStartTime;
    public DateTime WorkdayEndTime;

    public Schedule() : this(0, 0, DateTime.MinValue, DateTime.MinValue) {}

    public Schedule(int id, int doctorId, DateTime workdayStartTime, DateTime workdayEndTime)
    {
        Id = id;
        DoctorId = doctorId;
        WorkdayStartTime = workdayStartTime;
        WorkdayEndTime = workdayEndTime;
    }

    public Result IsValid()
    {
        if (Id < 0)
            return Result.Fail("Invalid id");

        if (DoctorId < 0)
            return Result.Fail("Invalid doctor id");

        if (WorkdayStartTime > WorkdayEndTime)
            return Result.Fail("Invalid time");

        return Result.Ok();
    }
}