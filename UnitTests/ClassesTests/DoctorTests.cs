namespace UnitTests.ClassesTests;

public class DoctorTests
{
    [Fact]
    public void CreateDoctor_Success()
    {
        var doctor = new Doctor(0, "a", 0);

        Assert.True(doctor.IsValid().Success);
    }

    [Fact]
    public void DoctorIdError_Fail()
    {
        var doctor = new Doctor(-1, "a", 0);
        var res = doctor.IsValid();

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid id", res.Error);
    }

    [Fact]
    public void FullNameError_Fail()
    {
        var doctor = new Doctor(0, "", 0);
        var res = doctor.IsValid();

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid full name", res.Error);
    }

    [Fact]
    public void SpecialtyError_Fail()
    {
        var doctor = new Doctor(0, "a", -1);
        var res = doctor.IsValid();

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid specialty id", res.Error);
    }
}