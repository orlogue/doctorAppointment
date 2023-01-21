namespace UnitTests.ClassesTests;

public class DoctorTests
{
    [Fact]
    public void CreateDoctor_Success()
    {
        var doctor = new Doctor(0, "a", new Specialty(1, "a"));

        Assert.True(doctor.IsValid().Success);
    }

    [Fact]
    public void DoctorIdError_Fail()
    {
        var doctor = new Doctor(-1, "a", new Specialty(1, "a"));
        var res = doctor.IsValid();

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid id", res.Error);
    }

    [Fact]
    public void FullNameError_Fail()
    {
        var doctor = new Doctor(0, "", new Specialty(1, "a"));
        var res = doctor.IsValid();

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid full name", res.Error);
    }

    [Fact]
    public void SpecialtyError_Fail()
    {
        var doctor = new Doctor(0, "a", new Specialty());
        var res = doctor.IsValid();

        Assert.True(res.IsFailure);
        Assert.Contains("Invalid specialty: ", res.Error);
    }
}