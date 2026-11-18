using EduardNikitinKT_31_23.Dtos;
using EduardNikitinKT_31_23.Interfaces.DisciplineInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace EduardNikitinKT_31_23.Controllers;

[ApiController]
[Route("[controller]")]
public class DisciplineController : ControllerBase
{
    private readonly IDisciplineService _disciplineService;

    public DisciplineController(IDisciplineService disciplineService)
    {
        _disciplineService = disciplineService;
    }

    [HttpGet]
    public async Task<IActionResult> GetDisciplines(
        CancellationToken cancellationToken)
    {
        var disciplines = await _disciplineService.GetDisciplinesAsync(cancellationToken);
        return Ok(disciplines);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetDisciplineById(
        int id,
        CancellationToken cancellationToken)
    {
        var discipline = await _disciplineService.GetDisciplineByIdAsync(id, cancellationToken);

        if (discipline == null)
            return NotFound();

        return Ok(discipline);
    }

    [HttpPost]
    public async Task<IActionResult> CreateDiscipline(
        DisciplineCreateDto requestDto,
        CancellationToken cancellationToken)
    {
        var discipline = await _disciplineService.CreateDisciplineAsync(requestDto, cancellationToken);
        return Ok(discipline);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateDiscipline(
        int id,
        DisciplineUpdateDto requestDto,
        CancellationToken cancellationToken)
    {
        var discipline = await _disciplineService.UpdateDisciplineAsync(id, requestDto, cancellationToken);

        if (discipline == null)
            return NotFound();

        return Ok(discipline);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteDiscipline(
        int id,
        CancellationToken cancellationToken)
    {
        var deleted = await _disciplineService.DeleteDisciplineAsync(id, cancellationToken);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}