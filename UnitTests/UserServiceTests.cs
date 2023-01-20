namespace UnitTests;

public class UserServiceTests
{
    private readonly UserService _userService;
    private readonly Mock<IUserRepository> _userRepositoryMock;

    public UserServiceTests()
    {
        _userRepositoryMock = new Mock<IUserRepository>();
        _userService = new UserService(_userRepositoryMock.Object);
    }

    [Fact]
    public void Register_ShouldFail()
    {
        var res = _userService.Register(new User());

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid username", res.Error);
    }

    [Fact]
    public void Register_AlreadyExists_ShouldFail()
    {
        _userRepositoryMock.Setup(repository => repository.DoesUserExist(
            It.IsAny<string>()))
            .Returns(() => true);

        var res = _userService.Register(
            new User(1, "a", "a", Role.Admin, "a", "a"));

        Assert.True(res.IsFailure);
        Assert.Equal("User with this username already exists", res.Error);
    }

    [Fact]
    public void Register_Error_ShouldFail()
    {
        _userRepositoryMock.Setup(repository => repository.DoesUserExist(
            It.IsAny<string>()))
            .Returns(() => false);

        _userRepositoryMock.Setup(repository => repository.Create(
            It.IsAny<User>()))
            .Returns(() => false);

        var res = _userService.Register(new User(1, "a", "a", Role.Patient, "a", "a"));

        Assert.True(res.IsFailure);
        Assert.Equal("Unable to create user", res.Error);
    }

    [Fact]
    public void GetUserByLogin_LoginIsEmptyOrNull_ShouldFail()
    {
        var res = _userService.GetUserByLogin(string.Empty);

        Assert.True(res.IsFailure);
        Assert.Equal("Empty username", res.Error);
    }

    [Fact]
    public void GetUserByLogin_NotFound_ShouldFail()
    {
        _userRepositoryMock.Setup(repository => repository.GetUserByLogin(
            It.IsAny<string>()))
            .Returns(() => null);

        var res = _userService.GetUserByLogin("qwertyuiop");

        Assert.True(res.IsFailure);
        Assert.Equal("User not found", res.Error);
    }

    [Fact]
    public void UserDoesNotExist_EmptyUsername_ShouldFail()
    {
        _userRepositoryMock.Setup(repository => repository.DoesUserExist(
            It.IsAny<string>()))
            .Returns(() => false);

        var res = _userService.DoesUserExist("qweqwe");

        Assert.True(res.IsFailure);
        Assert.Equal("User does not exist", res.Error);
    }

    [Fact]
    public void UserDoesNotExist_NotFound_ShouldFail()
    {
        _userRepositoryMock.Setup(repository => repository.DoesUserExist(
            It.IsAny<string>()))
            .Returns(() => false);

        var res = _userService.DoesUserExist("qweqwe");

        Assert.True(res.IsFailure);
        Assert.Equal("User does not exist", res.Error);
    }
}
