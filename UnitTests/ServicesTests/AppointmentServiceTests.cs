using domain.Classes;

namespace UnitTests.ServicesTests;

public class AppointmentServiceTests
{
    private readonly AppointmentService _appointmentService;
    private readonly Mock<IAppointmentRepository> _appointmentRepositoryMock;

    public AppointmentServiceTests()
	{
        _appointmentRepositoryMock = new Mock<IAppointmentRepository>();
        _appointmentService = new AppointmentService(_appointmentRepositoryMock.Object);
	}

    [Fact]
    public void SaveAppointment_Success()
    {
        List<Appointment> appointments = new();
        _appointmentRepositoryMock.Setup(rep => rep.GetAppointments(
            It.IsAny<int>()))
            .Returns(() => appointments);
        _appointmentRepositoryMock.Setup(rep => rep.Create(
            It.IsAny<Appointment>()))
            .Returns(() => true);

        var appointment = new Appointment();
        var schedule = new Schedule(0, 0, DateTime.MinValue, DateTime.MaxValue);
        var res = _appointmentService.SaveAppointment(schedule, appointment);

        Assert.True(res.Success);
    }

    [Fact]
    public void SaveAppointment_InvalidSchedule_Fail()
    {
        var appointment = new Appointment(0, 0, 0, DateTime.MinValue, DateTime.MinValue);
        var schedule = new Schedule(-1, 0, DateTime.MinValue, DateTime.MinValue);
        var res = _appointmentService.SaveAppointment(schedule, appointment);

        Assert.True(res.IsFailure);
        Assert.Contains("Invalid schedule: ", res.Error);
    }

    [Fact]
    public void SaveAppointment_InvalidAppointment_Fail()
    {
        var appointment = new Appointment(0, -1, -1, DateTime.MinValue, DateTime.MinValue);
        var schedule = new Schedule();
        var res = _appointmentService.SaveAppointment(schedule, appointment);

        Assert.True(res.IsFailure);
        Assert.Contains("Invalid appointment: ", res.Error);
    }

    [Fact]
    public void SaveAppointment_InvalidTime_Fail()
    {
        var appointment = new Appointment(0, 0, 0, DateTime.MaxValue, DateTime.MaxValue);
        var schedule = new Schedule(0, 0, DateTime.MinValue, DateTime.MinValue);
        var res = _appointmentService.SaveAppointment(schedule, appointment);

        Assert.True(res.IsFailure);
        Assert.Equal("Appointment out of schedule", res.Error);
    }

    [Fact]
    public void SaveAppointment_TakenTime_Fail()
    {
        List<Appointment> apps = new()
            {
                new Appointment(0, 0, 0, DateTime.MinValue, DateTime.Parse("2023-01-10")),
                new Appointment(0, 0, 0, DateTime.Parse("2023-01-10"), DateTime.MaxValue)
            };
        _appointmentRepositoryMock.Setup(rep => rep.GetAppointments(
            It.IsAny<int>()))
            .Returns(() => apps);

        var appointment = new Appointment(0, 0, 0, DateTime.Parse("2023-01-01"), DateTime.Parse("2023-02-01"));
        var schedule = new Schedule(0, 0, DateTime.MinValue, DateTime.MaxValue);
        var res = _appointmentService.SaveAppointment(schedule, appointment);

        Assert.True(res.IsFailure);
        Assert.Equal("This time already taken", res.Error);
    }

    [Fact]
    public void SaveAppointment_SaveError_Fail()
    {
        List<Appointment> apps = new();
        _appointmentRepositoryMock.Setup(rep => rep.GetAppointments(
            It.IsAny<int>()))
            .Returns(() => apps);
        _appointmentRepositoryMock.Setup(rep => rep.Create(
            It.IsAny<Appointment>()))
            .Returns(() => false);

        var appointment = new Appointment();
        var schedule = new Schedule(0, 0, DateTime.MinValue, DateTime.MaxValue);
        var res = _appointmentService.SaveAppointment(schedule, appointment);

        Assert.True(res.IsFailure);
        Assert.Equal("Unable to save the appointment", res.Error);
    }

    [Fact]
    public void GetFreeAppointment_InvalidSpecialty_Fail()
    {
        var specialty = new Specialty();
        var res = _appointmentService.GetFreeAppointments(specialty);

        Assert.True(res.IsFailure);
        Assert.Contains("Invalid specialization: ", res.Error);
    }

    [Fact]
    public void GetFreeAppointment_Success()
    {
        List<DateTime> appointments = new()
            {
                new DateTime(),
                new DateTime()
            };
        IEnumerable<DateTime> a = appointments;
        _appointmentRepositoryMock.Setup(
            rep => rep.GetFreeAppointments(It.IsAny<Specialty>())).Returns(() => a);

        var specialty = new Specialty(0, "a");

        Assert.True(_appointmentService.GetFreeAppointments(specialty).Success);
    }
}