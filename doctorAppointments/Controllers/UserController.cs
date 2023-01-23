using Microsoft.AspNetCore.Mvc;

using doctorAppointments.Views;
using domain.Services;
using domain.Classes;
using doctorAppointments.JWT;

namespace doctorAppointments.Controllers;

[ApiController]
[Route("user")]
public class UserController : ControllerBase
{
    private readonly UserService _userService;

    public UserController(UserService userService) =>_userService = userService;

    [HttpPost("register")]
    public async Task<ActionResult> Register(User user)
    {
        var result = await _userService.Register(user);
        if (result.IsFailure)
            return Problem(statusCode: 400, detail: result.Error);
        return Ok(result.Success);
    }

    [HttpPost("sign_in")]
    public async Task<IActionResult> SignIn(string username, string password)
    {
        if (string.IsNullOrEmpty(username))
            return Problem(statusCode: 400, detail: "Invalid login");

        if (string.IsNullOrEmpty(password))
            return Problem(statusCode: 400, detail: "Invalid password");

        var user = await _userService.GetUserByLogin(username);

        if (user.IsFailure)
            return Problem(statusCode: 400, detail: "Invalid login or password");

        if (!user.Value.Password.Equals(password))
            return Problem(statusCode: 400, detail: "Invalid login or password");

        return Ok(new { access_token = JwtManager.CreateToken(user.Value) });
    }

    [HttpGet("get_by_username")]
    public async Task<IActionResult> GetUserByUsername(string username)
    {
        var result = await _userService.GetUserByLogin(username);
        if (result.IsFailure)
            return Problem(statusCode: 400, detail: result.Error);
        return Ok(result.Value);
    }

    [HttpGet("does_user_exist")]
    public async Task<IActionResult> DoesUserExist(string username)
    {
        var result = await _userService.DoesUserExist(username);
        if (result.IsFailure)
            return Problem(statusCode: 400, detail: result.Error);
        return Ok(result.Success);
    }
}
