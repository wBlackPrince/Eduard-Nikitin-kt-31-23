using EduardNikitinKT_31_23.Dtos;
using EduardNikitinKT_31_23.Interfaces.SpecialityInterfaces;
using Microsoft.AspNetCore.Mvc;

namespace EduardNikitinKT_31_23.Controllers;

[ApiController]
[Route("[controller]")]
public class SpecialityController : ControllerBase
{
    private readonly ISpecialityService _specialityService;

    public SpecialityController(ISpecialityService specialityService)
    {
        _specialityService = specialityService;
    }

    [HttpGet]
    public async Task<IActionResult> GetSpecialities(
        CancellationToken cancellationToken)
    {
        var specialities = await _specialityService.GetSpecialitiesAsync(
            cancellationToken);

        return Ok(specialities);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetSpecialityById(
        int id,
        CancellationToken cancellationToken)
    {
        var speciality = await _specialityService.GetSpecialityByIdAsync(
            id,
            cancellationToken);

        if (speciality == null)
            return NotFound();

        return Ok(speciality);
    }

    [HttpPost]
    public async Task<IActionResult> CreateSpeciality(
        SpecialityCreateDto requestDto,
        CancellationToken cancellationToken)
    {
        var speciality = await _specialityService.CreateSpecialityAsync(
            requestDto,
            cancellationToken);

        return Ok(speciality);
    }

    [HttpPut("{id}")]
    public async Task<IActionResult> UpdateSpeciality(
        int id,
        SpecialityUpdateDto requestDto,
        CancellationToken cancellationToken)
    {
        var speciality = await _specialityService.UpdateSpecialityAsync(
            id,
            requestDto,
            cancellationToken);

        if (speciality == null)
            return NotFound();

        return Ok(speciality);
    }

    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteSpeciality(
        int id,
        CancellationToken cancellationToken)
    {
        var deleted = await _specialityService.DeleteSpecialityAsync(
            id,
            cancellationToken);

        if (!deleted)
            return NotFound();

        return NoContent();
    }
}