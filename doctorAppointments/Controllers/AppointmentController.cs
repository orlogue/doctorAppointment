using Microsoft.AspNetCore.Mvc;

using domain.Services;
using domain.Classes;
using Microsoft.AspNetCore.Authorization;

namespace doctorAppointments.Controllers;

[ApiController]
[Route("appointment")]
public class AppointmentController : ControllerBase
{
    private readonly AppointmentService _appointmentService;
    private readonly ScheduleService _scheduleService;
    private readonly DoctorService _doctorService;

    public AppointmentController(
        AppointmentService appointmentService,
        ScheduleService scheduleService,
        DoctorService doctorService)
    {
        _appointmentService = appointmentService;
        _scheduleService = scheduleService;
        _doctorService = doctorService;
    }

    [Authorize]
    [HttpPost("save")]
    public async Task<IActionResult> SaveAppointment(Appointment appointment)
    {
        var doc = await _doctorService.GetItem(appointment.DoctorId);
        if (doc.IsFailure)
            return Problem(statusCode: 400, detail: doc.Error);

        var schedule = (await _scheduleService.GetSchedule(doc.Value)).Value.ToList()
            .FirstOrDefault(
            it =>
            it.WorkdayStartTime <= appointment.StartTime &&
            it.WorkdayEndTime >= appointment.EndTime);
        var appointemnt = await _appointmentService.SaveAppointment(schedule, appointment);
        if (appointemnt.IsFailure) return Problem(statusCode: 400, detail: appointemnt.Error);
        return Ok(appointemnt.Value);
    }

    [Authorize]
    [HttpPost("get")]
    public async Task<IActionResult> GetAppointment(Specialty specialty)
    {
        var appointemnt = await _appointmentService.GetFreeAppointments(specialty);
        if (appointemnt.IsFailure) return Problem(statusCode: 400, detail: appointemnt.Error);
        return Ok(appointemnt.Value);
    }
}