using Microsoft.AspNetCore.Mvc;

using database.Repositories;
using domain.Logic.Interfaces;
using doctorAppointments.Views;
using domain.Classes;
using Microsoft.AspNetCore.Authorization;

namespace doctorAppointments.Controllers;

[ApiController]
[Route("specialty")]
public class SpecialtyController : ControllerBase
{
    private readonly ISpecialtyRepository _service;

    public SpecialtyController(ISpecialtyRepository service) => _service = service;

    [HttpGet("get_all")]
    public async Task<IActionResult> GetAll()
    {
        return Ok(await _service.GetItemsList());
    }

    [HttpGet("get_by_name")]
    public async Task<IActionResult> GetByName(string name)
    {
        var res = await _service.GetByName(name);
        return res != null ? Ok(res) : Problem(statusCode: 400, detail: "Not Found");
    }

    [Authorize]
    [HttpPost("add")]
    public async Task<IActionResult> AddSpecialty(string name)
    {
        Specialty specialty = new(0, name);

        var res = specialty.IsValid();
        if (res.IsFailure)
            return Problem(statusCode: 400, detail: res.Error);

        var created = await _service.Create(specialty);
        return created ? Ok(await _service.GetByName(name)) :
            Problem(statusCode: 404, detail: "Could not create specialty");
    }

    [HttpDelete("delete")]
    public async Task<IActionResult> DeleteSpecialty(int id)
    {
        var res = await _service.Delete(id);
        return res ? Ok() : Problem(statusCode: 404, detail: "Could not delete specialty");
    }
}
