namespace UnitTests.ClassesTests;

public class ScheduleTests
{
	[Fact]
	public void CreateSchedule_Success()
	{
		var schedule = new Schedule(0, DateTime.MinValue, DateTime.MinValue);

		Assert.True(schedule.IsValid().Success);
	}

    [Fact]
    public void DoctorIdError_Fail()
    {
        var schedule = new Schedule(-1, DateTime.MinValue, DateTime.MinValue);
        var res = schedule.IsValid();

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid doctor id", res.Error);
    }

    [Fact]
    public void DateTimeError_Fail()
    {
        var schedule = new Schedule(0, DateTime.MaxValue, DateTime.MinValue);
        var res = schedule.IsValid();

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid time", res.Error);
    }
}