using Microsoft.AspNetCore.Mvc;

using doctorAppointments.Views;
using domain.Services;
using domain.Classes;

namespace doctorAppointments.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService) =>_userService = userService;

    [HttpPost("register")]
    public ActionResult Register(User user)
    {
        var result = _userService.Register(user);
        if (result.IsFailure)
            return Problem(statusCode: 400, detail: result.Error);
        return Ok(result.Success);
    }

    [HttpGet("get_by_username")]
    public ActionResult<UserSearchView> GetUserByUsername(string username)
    {
        var result = _userService.GetUserByLogin(username);
        if (result.IsFailure)
            return Problem(statusCode: 400, detail: result.Error);
        return Ok(new UserSearchView
        {
            PhoneNumber = result.Value.PhoneNumber,
            FullName = result.Value.FullName,
            Role = result.Value.Role,
            Username = result.Value.Username
        });
    }

    [HttpGet("does_user_exist")]
    public IActionResult DoesUserExist(string username)
    {
        var result = _userService.DoesUserExist(username);
        if (result.IsFailure)
            return Problem(statusCode: 400, detail: result.Error);
        return Ok(result.Success);
    }
}
