using Microsoft.AspNetCore.Mvc;

using database.Repositories;
using domain.Logic.Interfaces;
using doctorAppointments.Views;
using domain.Classes;
using domain.Services;
using domain.Logic;
using System.Numerics;
using Microsoft.AspNetCore.Authorization;

namespace doctorAppointments.Controllers;

[ApiController]
[Route("schedule")]
public class ScheduleController : ControllerBase
{
    private readonly ScheduleService _service;
    public ScheduleController(ScheduleService service) => _service = service;

    [HttpGet("get")]
    public async Task<IActionResult> GetSchedule(Doctor doctor)
    {
        var res = await _service.GetSchedule(doctor);
        return res.Success
            ? Ok(res.Value)
            : Problem(statusCode: 400, detail: res.Error);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateSchedule(Schedule schedule)
    {
        var res = await _service.CreateSchedule(schedule);
        return res.Success? Ok() : Problem(statusCode: 400, detail: res.Error);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("update")]
    public async Task<IActionResult> UpdateSchedule(Schedule schedule)
    {
        var res = await _service.UpdateSchedule(schedule);
        return res.Success ? Ok() : Problem(statusCode: 400, detail: res.Error);
    }
}
