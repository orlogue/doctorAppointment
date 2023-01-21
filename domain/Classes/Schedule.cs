using domain.Logic;

namespace domain.Classes;

public class Schedule
{
    public int DoctorId;
    public DateTime WorkdayStartTime;
    public DateTime WorkdayEndTime;

    public Schedule() : this(0, DateTime.MinValue, DateTime.MinValue) {}

    public Schedule(int doctorId, DateTime workdayStartTime, DateTime workdayEndTime)
    {
        DoctorId = doctorId;
        WorkdayStartTime = workdayStartTime;
        WorkdayEndTime = workdayEndTime;
    }

    public Result IsValid()
    {
        if (DoctorId < 0)
            return Result.Fail("Invalid doctor id");

        if (WorkdayStartTime > WorkdayEndTime)
            return Result.Fail("Invalid time");

        return Result.Ok();
    }
}