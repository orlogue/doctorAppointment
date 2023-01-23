using Microsoft.AspNetCore.Mvc;

using domain.Services;
using domain.Classes;
using domain.Logic.Interfaces;
using System.Xml.Linq;

namespace doctorAppointments.Controllers;

[ApiController]
[Route("doctor")]
public class DoctorController : ControllerBase
{
    private readonly DoctorService _service;
    private readonly ISpecialtyRepository _serviceSpecialty;

    public DoctorController(DoctorService service, ISpecialtyRepository serviceSpecialty)
    {
        _service = service;
        _serviceSpecialty = serviceSpecialty;
    }

    [HttpGet("get_all")]
    public IActionResult GetAll()
    {
        return Ok(_service.GetAllDoctors());
    }

    [HttpGet("get")]
    public IActionResult GetDoctor(int id)
    {
        var res = _service.GetItem(id);
        return res != null ? Ok(res)
            : Problem(statusCode: 400, detail: res.Error);
    }

    [HttpGet("get_all_by_specialty")]
    public IActionResult FindDoctorsBySpecialty(int specialtyId)
    {
        var res = _service.FindDoctorsBySpecialty(specialtyId);
        return res.Success ? Ok(res.Value)
            : Problem(statusCode: 400, detail: res.Error);
    }

    [HttpGet("get_one_by_specialty")]
    public IActionResult GetDoctorBySpecialty(int specialtyId)
    {
        var res = _service.FindDoctor(specialtyId);
        return res.Success ? Ok(res.Value)
            : Problem(statusCode: 400, detail: res.Error);
    }

    [HttpPost("create")]
    public IActionResult CreateDoctor(Doctor doctor)
    {
        var created = _service.Create(doctor);
        return created.Success ? Ok(created) :
            Problem(statusCode: 404, detail: created.Error);
    }

    [HttpDelete("delete")]
    public IActionResult DeleteDoctor(int id)
    {
        var deleted = _service.Delete(id);

        return deleted.Success ? Ok(deleted.Value) :
            Problem(statusCode: 404, detail: deleted.Error);
    }
}
