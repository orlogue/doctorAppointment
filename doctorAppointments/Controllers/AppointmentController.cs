using Microsoft.AspNetCore.Mvc;

using domain.Services;
using domain.Classes;


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

    [HttpPost("save")]
    public IActionResult SaveAppointment(Appointment appointment)
    {
        var doc = _doctorService.GetItem(appointment.DoctorId);
        if (doc.IsFailure)
            return Problem(statusCode: 400, detail: doc.Error);

        var schedule = _scheduleService.GetSchedule(doc.Value).Value.First(
            it => it.WorkdayStartTime.Date == appointment.StartTime.Date);
        var appointemnt = _appointmentService.SaveAppointment(schedule, appointment);
        if (appointemnt.IsFailure) return Problem(statusCode: 400, detail: appointemnt.Error);
        return Ok(appointemnt.Value);
    }

    [HttpPost("get")]
    public IActionResult GetAppointment(Specialty specialty)
    {
        var appointemnt = _appointmentService.GetFreeAppointments(specialty);
        if (appointemnt.IsFailure) return Problem(statusCode: 400, detail: appointemnt.Error);
        return Ok(appointemnt.Value);
    }
}