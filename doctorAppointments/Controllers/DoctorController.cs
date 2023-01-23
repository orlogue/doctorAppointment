using Microsoft.AspNetCore.Mvc;

using domain.Services;
using domain.Classes;
using domain.Logic.Interfaces;
using System.Xml.Linq;
using Microsoft.AspNetCore.Authorization;

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
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetAllDoctors());
    }

    [HttpGet("get")]
    public async Task<IActionResult> GetDoctor(int id)
    {
        var res = await _service.GetItem(id);
        return res != null ? Ok(res)
            : Problem(statusCode: 400, detail: res.Error);
    }

    [HttpGet("get_all_by_specialty")]
    public async Task<IActionResult> FindDoctorsBySpecialty(int specialtyId)
    {
        var res = await _service.FindDoctorsBySpecialty(specialtyId);
        return res.Success ? Ok(res.Value)
            : Problem(statusCode: 400, detail: res.Error);
    }

    [HttpGet("get_one_by_specialty")]
    public async Task<IActionResult> GetDoctorBySpecialty(int specialtyId)
    {
        var res = await _service.FindDoctor(specialtyId);
        return res.Success ? Ok(res.Value)
            : Problem(statusCode: 400, detail: res.Error);
    }

    [Authorize(Roles = "Admin")]
    [HttpPost("create")]
    public async Task<IActionResult> CreateDoctor(Doctor doctor)
    {
        var created = await _service.Create(doctor);
        return created.Success ? Ok(created) :
            Problem(statusCode: 404, detail: created.Error);
    }

    [Authorize(Roles = "Admin")]
    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteDoctor(int id)
    {
        var deleted = await _service.Delete(id);

        return deleted.Success ? Ok(deleted.Value) :
            Problem(statusCode: 404, detail: deleted.Error);
    }
}
