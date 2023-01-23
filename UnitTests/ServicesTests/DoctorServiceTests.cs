namespace UnitTests.ServicesTests;

public class DoctorServiceTests
{
    private readonly DoctorService _doctorService;
    private readonly Mock<IDoctorRepository> _doctorRepositoryMock;

    public DoctorServiceTests()
	{
        _doctorRepositoryMock = new Mock<IDoctorRepository>();
        _doctorService = new DoctorService(_doctorRepositoryMock.Object);
    }

    [Fact]
    public void GetAllDoctors_Success()
    {
        List<Doctor> doctors = new()
            {
                new Doctor(0, "a", 0),
                new Doctor(1, "aa", 0)
            };
        IEnumerable<Doctor> d = doctors;
        _doctorRepositoryMock.Setup(rep => rep.GetItemsList()).Returns(() => d);

        Assert.True(_doctorService.GetAllDoctors().Success);
    }

    [Fact]
    public void FindDoctorById_Success()
    {
        _doctorRepositoryMock.Setup(rep => rep.GetItem(
            It.IsAny<int>()))
            .Returns(() => new Doctor(0, "a", 0));

        Assert.True(_doctorService.GetItem(0).Success);
    }

    [Fact]
    public void FindDoctorById_Fail()
    {
        _doctorRepositoryMock.Setup(rep => rep.GetItem(
            It.IsAny<int>()))
            .Returns(() => null);

        var res = _doctorService.GetItem(0);

        Assert.True(res.IsFailure);
        Assert.Equal("Unable to find doctor", res.Error);
    }

    [Fact]
    public void FindDoctorBySpecialty_Success()
    {
        _doctorRepositoryMock.Setup(rep => rep.FindDoctor(
            It
            .IsAny<int>()))
            .Returns(() => new Doctor(0, "a", 0));

        Assert.True(_doctorService.FindDoctor(0).Success);
    }

    [Fact]
    public void FindDoctorBySpecialty_InvalidSpecialty_Fail()
    {
        var res = _doctorService.FindDoctor(-1);

        Assert.True(res.IsFailure);
        Assert.Contains("Invalid specialty", res.Error);
    }

    [Fact]
    public void FindDoctorBySpecialty_Fail()
    {
        _doctorRepositoryMock.Setup(rep => rep.FindDoctor(
            It.IsAny<int>()))
            .Returns(() => null);

        var res = _doctorService.FindDoctor(0);

        Assert.True(res.IsFailure);
        Assert.Equal("Unable to find doctor", res.Error);
    }

    [Fact]
    public void CreateDoctor_Success()
    {
        _doctorRepositoryMock.Setup(rep => rep.Create(
            It.IsAny<Doctor>()))
            .Returns(() => true);

        var doctor = new Doctor(0, "a", 0);
        var res = _doctorService.Create(doctor);

        Assert.True(res.Success);
    }

    [Fact]
    public void CreateDoctor_InvalidDoctor_Fail()
    {
        var doctor = new Doctor();
        var res = _doctorService.Create(doctor);

        Assert.True(res.IsFailure);
        Assert.Contains("Invalid doctor: ", res.Error);
    }

    [Fact]
    public void CreateDoctor_CreateError_Fail()
    {
        _doctorRepositoryMock.Setup(rep => rep.Create(
            It.IsAny<Doctor>()))
            .Returns(() => false);

        var doctor = new Doctor(0, "a", 0);
        var result = _doctorService.Create(doctor);

        Assert.True(result.IsFailure);
        Assert.Equal("Unable to create doctor", result.Error);
    }


    [Fact]
    public void DeleteDoctor_Success()
    {
        _doctorRepositoryMock.Setup(rep => rep.GetItem(
            It.IsAny<int>()))
            .Returns(() => new Doctor(0, "a", 0));
        _doctorRepositoryMock.Setup(rep => rep.Delete(
            It.IsAny<int>()))
            .Returns(() => true);

        Assert.True(_doctorService.Delete(0).Success);
    }

    [Fact]
    public void DeleteDoctor_IdNotFound_Fail()
    {
        var res = _doctorService.Delete(0);

        Assert.True(res.IsFailure);
        Assert.Equal("Unable to find doctor", res.Error);
    }

    [Fact]
    public void DeleteDoctor_DoctorNotFound_Fail()
    {
        _doctorRepositoryMock.Setup(rep => rep.GetItem(
            It.IsAny<int>()))
            .Returns(() => null);

        var res = _doctorService.Delete(0);

        Assert.True(res.IsFailure);
        Assert.Equal("Unable to find doctor", res.Error);
    }

    [Fact]
    public void DeleteDoctor_DeleteError_Fail()
    {
        _doctorRepositoryMock.Setup(rep => rep.GetItem(
            It.IsAny<int>()))
            .Returns(() => new Doctor(0, "a", 0));
        _doctorRepositoryMock.Setup(rep => rep.Delete(
            It.IsAny<int>()))
            .Returns(() => false);

        var res = _doctorService.Delete(0);

        Assert.True(res.IsFailure);
        Assert.Equal("Unable to delete doctor", res.Error);
    }
}