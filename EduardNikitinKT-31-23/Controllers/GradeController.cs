using EduardNikitinKT_31_23.Dtos;
using EduardNikitinKT_31_23.Interfaces.GradeInterfaces;
using Microsoft.AspNetCore.Mvc;

[ApiController]
[Route("[controller]")]
public class GradeController : ControllerBase
{
    private readonly IGradeService _gradeService;

    public GradeController(IGradeService gradeService)
    {
        _gradeService = gradeService;
    }

    [HttpGet]
    public async Task<IActionResult> GetGradesAsync(
        CancellationToken cancellationToken = default)
    {
        var grades = await _gradeService.GetGradesAsync(
            cancellationToken);

        return Ok(grades);
    }

    [HttpGet("{id:int}", Name = "GetGradeById")]
    public async Task<IActionResult> GetGradeByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var grade = await _gradeService.GetGradeByIdAsync(
            id,
            cancellationToken);

        if (grade == null)
        {
            return NotFound();
        }

        return Ok(grade);
    }

    [HttpPost]
    public async Task<IActionResult> CreateGradeAsync(
    GradeCreateDto requestDto,
    CancellationToken cancellationToken = default)
    {
        try
        {
            GradeDto grade = await _gradeService.CreateGradeAsync(
                requestDto,
                cancellationToken);

            return Ok(grade);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpPut("{id:int}")]
    public async Task<IActionResult> UpdateGradeAsync(
        int id,
        GradeUpdateDto requestDto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var grade = await _gradeService.UpdateGradeAsync(
                id,
                requestDto,
                cancellationToken);

            if (grade == null)
            {
                return NotFound();
            }

            return Ok(grade);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }

    [HttpDelete("{id:int}")]
    public async Task<IActionResult> DeleteGradeAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var deleted = await _gradeService.DeleteGradeAsync(
            id,
            cancellationToken);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}