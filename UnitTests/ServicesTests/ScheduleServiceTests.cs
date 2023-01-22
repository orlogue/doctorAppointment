namespace UnitTests.ServicesTests;

public class ScheduleServiceTests
{
    private readonly ScheduleService _scheduleService;
    private readonly Mock<IScheduleRepository> _scheduleRepositoryMock;

    public ScheduleServiceTests()
	{
        _scheduleRepositoryMock = new Mock<IScheduleRepository>();
        _scheduleService = new ScheduleService(_scheduleRepositoryMock.Object);
	}

    [Fact]
    public void GetSchedule_Success()
    {
        List<Schedule> sch = new()
        {
            new Schedule(),
            new Schedule()
        };
        IEnumerable<Schedule> schedules = sch;

        _scheduleRepositoryMock.Setup(rep => rep.GetScheduleByDoctor(
            It.IsAny<Doctor>()))
            .Returns(() => schedules);

        var res = _scheduleService.GetSchedule(
            new Doctor(0, "a", new Specialty(0, "a")));

        Assert.True(res.Success);
    }

    [Fact]
    public void GetSchedule_InvalidDoctor_Fail()
    {
        var res = _scheduleService.GetSchedule(new Doctor());

        Assert.True(res.IsFailure);
        Assert.Contains("Invalid doctor: ", res.Error);
    }

    [Fact]
    public void CreateSchedule_Success()
    {
        _scheduleRepositoryMock.Setup(rep => rep.Create(
            It.IsAny<Schedule>()))
            .Returns(() => true);

        var res = _scheduleService.CreateSchedule(
            new Schedule()
        );

        Assert.True(res.Success);
    }

    [Fact]
    public void CreateSchedule_InvalidSchedule_Fail()
    {
        var res = _scheduleService.CreateSchedule(
            new Schedule(-1, 0, DateTime.MinValue, DateTime.MinValue)
        );

        Assert.True(res.IsFailure);
        Assert.Contains("Invalid schedule: ", res.Error);
    }

    [Fact]
    public void CreateSchedule_Fail()
    {
        _scheduleRepositoryMock.Setup(rep => rep.Create(
            It.IsAny<Schedule>()))
            .Returns(() => false);

        var res = _scheduleService.CreateSchedule(
            new Schedule());

        Assert.True(res.IsFailure);
        Assert.Equal("Unable to create schedule", res.Error);
    }

    [Fact]
    public void UpdateSchedule_Success()
    {
        _scheduleRepositoryMock.Setup(rep => rep.Update(
            It.IsAny<Schedule>()))
            .Returns(() => true);

        var res = _scheduleService.UpdateSchedule(
            new Schedule()
        );

        Assert.True(res.Success);
    }

    [Fact]
    public void UpdateSchedule_InvalidSchedule_Fail()
    {
        var res = _scheduleService.UpdateSchedule(
            new Schedule(0, -1, DateTime.MinValue, DateTime.MinValue)
        );

        Assert.True(res.IsFailure);
        Assert.Contains("Invalid schedule: ", res.Error);
    }

    [Fact]
    public void UpdateSchedule_Fail()
    {
        _scheduleRepositoryMock.Setup(rep => rep.Update(
            It.IsAny<Schedule>()))
            .Returns(() => false);

        var res = _scheduleService.UpdateSchedule(
            new Schedule());

        Assert.True(res.IsFailure);
        Assert.Equal("Unable to update schedule", res.Error);
    }
}