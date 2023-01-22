namespace UnitTests.ClassesTests;

public class AppointmentTests
{
    [Fact]
    public void CreateAppointment_Success()
    {
        var appointment = new Appointment(0, 0, 0, DateTime.MinValue, DateTime.MinValue);

        Assert.True(appointment.IsValid().Success);
    }

    [Fact]
    public void IdError_Fail()
    {
        var appointment = new Appointment(-1, 0, 0, DateTime.MinValue, DateTime.MinValue);
        var res = appointment.IsValid();

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid id", res.Error);
    }

    [Fact]
    public void PatientIdError_Fail()
    {
        var appointment = new Appointment(0, -1, 0, DateTime.MinValue, DateTime.MinValue);
        var res = appointment.IsValid();

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid patient id", res.Error);
    }

    [Fact]
    public void DoctorIdError_Fail()
    {
        var appointment = new Appointment(0, 0, -1, DateTime.MinValue, DateTime.MinValue);
        var res = appointment.IsValid();

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid doctor id", res.Error);
    }

    [Fact]
    public void DateTimeError_Fail()
    {
        var appointment = new Appointment(0, 0, 0, DateTime.MaxValue, DateTime.MinValue);
        var res = appointment.IsValid();

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid time", res.Error);
    }
}