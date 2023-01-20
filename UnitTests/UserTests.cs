namespace UnitTests;

public class UserTests
{
    [Fact]
    public void IdError_ShouldFail()
    {
        var user = new User(-1, "a", "a", Role.Patient, "a", "a");
        var res = user.IsValid();

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid id", res.Error);
    }

    [Fact]
    public void UsernameError_ShouldFail()
    {
        var user = new User(1, "a", "a", Role.Patient, "", "a");
        var res = user.IsValid();

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid username", res.Error);
    }

    [Fact]
    public void PasswordError_ShouldFail()
    {
        var user = new User(1, "a", "a", Role.Patient, "a", "");
        var res = user.IsValid();

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid password", res.Error);
    }

    [Fact]
    public void PhoneNumberError_ShouldFail()
    {
        var user = new User(1, "", "a", Role.Patient, "a", "a");
        var res = user.IsValid();

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid phone number", res.Error);
    }

    [Fact]
    public void FullNameError_ShouldFail()
    {
        var user = new User(1, "a", "", Role.Patient, "a", "a");
        var res = user.IsValid();

        Assert.True(res.IsFailure);
        Assert.Equal("Invalid full name", res.Error);
    }
}