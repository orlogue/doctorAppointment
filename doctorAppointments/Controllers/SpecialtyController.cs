using Microsoft.AspNetCore.Mvc;

using database.Repositories;
using domain.Logic.Interfaces;
using doctorAppointments.Views;
using domain.Classes;

namespace doctorAppointments.Controllers;

[ApiController]
[Route("specialty")]
public class SpecialtyController : ControllerBase
{
    private readonly ISpecialtyRepository _service;

    public SpecialtyController(ISpecialtyRepository service) => _service = service;

    [HttpGet("get_all")]
    public IActionResult GetAll()
    {
        return Ok(_service.GetItemsList());
    }

    [HttpGet("get_by_name")]
    public IActionResult GetByName(string name)
    {
        var res = _service.GetByName(name);
        return res != null ? Ok(res) : Problem(statusCode: 400, detail: "Not Found");
    }

    [HttpPost("add")]
    public IActionResult AddSpecialty(string name)
    {
        Specialty specialty = new(0, name);

        var res = specialty.IsValid();
        if (res.IsFailure)
            return Problem(statusCode: 400, detail: res.Error);

        var created = _service.Create(specialty);
        return created ? Ok(_service.GetByName(name)) :
            Problem(statusCode: 404, detail: "Could not create specialty");
    }

    [HttpDelete("delete")]
    public IActionResult DeleteSpecialty(int id)
    {
        var res = _service.Delete(id);
        return res ? Ok() : Problem(statusCode: 404, detail: "Could not delete specialty");
    }
}
