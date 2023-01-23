using Microsoft.AspNetCore.Mvc;

using database.Repositories;
using domain.Logic.Interfaces;
using doctorAppointments.Views;
using domain.Classes;
using domain.Services;
using domain.Logic;
using System.Numerics;

namespace doctorAppointments.Controllers;

[ApiController]
[Route("schedule")]
public class ScheduleController : ControllerBase
{
    private readonly ScheduleService _service;
    ScheduleController(ScheduleService service) => _service = service;

    [HttpGet("get")]
    public IActionResult GetSchedule(Doctor doctor)
    {
        var res = _service.GetSchedule(doctor);
        return res.Success
            ? Ok(res.Value)
            : Problem(statusCode: 400, detail: res.Error);
    }

    [HttpGet("create")]
    public IActionResult CreateSchedule(Schedule schedule)
    {
        var res = _service.CreateSchedule(schedule);
        return res.Success? Ok() : Problem(statusCode: 400, detail: res.Error);
    }

    [HttpGet("update")]
    public IActionResult UpdateSchedule(Schedule schedule)
    {
        var res = _service.UpdateSchedule(schedule);
        return res.Success ? Ok() : Problem(statusCode: 400, detail: res.Error);
    }
}
