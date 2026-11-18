using EduardNikitinKT_31_23.Dtos;
using EduardNikitinKT_31_23.Filters.StudentFilters;
using EduardNikitinKT_31_23.Interfaces.GradeInterfaces;
using EduardNikitinKT_31_23.Interfaces.StudentInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduardNikitinKT_31_23.Controllers;

[ApiController]
[Route("[controller]")]
public class StudentController : ControllerBase
{
    private readonly ILogger<StudentController> _logger;
    private readonly IStudentService _studentService;
    private readonly IGradeService _gradeService;

    public StudentController(ILogger<StudentController> logger, IStudentService studentService, IGradeService gradeService)
    {
        _logger = logger;
        _studentService = studentService;
        _gradeService = gradeService;
    }

    [HttpPost("by-group", Name = "GetStudentsByGroup")]
    public async Task<IActionResult> GetStudentsByGroupAsync(
        StudentGroupFilter filter,
        CancellationToken cancellationToken = default)
    {
        var students = await _studentService.GetStudentsByGroupAsync(
            filter,
            cancellationToken);

        return Ok(students);
    }


    [HttpGet("student/average/{groupId:int}")]
    public async Task<IActionResult> GetAverageRate(
        int groupId,
        CancellationToken cancellationToken = default)
    {
        var grades = await _studentService.GetAverageRate(
            groupId,
            cancellationToken);

        return Ok(grades);
    }


    [HttpGet("student/{studentId:int}")]
    public async Task<IActionResult> GetGradesByStudent(
        int studentId,
        CancellationToken cancellationToken = default)
    {
        var grades = await _studentService.GetStudentMark(
            studentId,
            cancellationToken);

        return Ok(grades);
    }


    [HttpPost("disciplines-with-four", Name = "GetDisciplinesWhereStudentHas4Mark")]
    public async Task<IActionResult> GetDisciplinesWhereStudentHas4MarkAsync(
        StudentFamilyDto requestDto,
        CancellationToken cancellationToken = default)
    {
        var disciplines = await _studentService.GetDisciplinesWhereStudentHas4MarkAsync(
            requestDto,
            cancellationToken);

        return Ok(disciplines);
    }

    [HttpGet("{id:int}", Name = "GetStudentById")]
    public async Task<IActionResult> GetStudentByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var student = await _studentService.GetStudentByIdAsync(
            id,
            cancellationToken);

        if (student == null)
        {
            return NotFound();
        }

        return Ok(student);
    }


    [HttpPost("create", Name = "CreateStudent")]
    public async Task<IActionResult> CreateStudentAsync(
        StudentCreateDto requestDto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var student = await _studentService.CreateStudentAsync(
                requestDto,
                cancellationToken);

            return Ok(student);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpPut("{id:int}", Name = "UpdateStudent")]
    public async Task<IActionResult> UpdateStudentAsync(
        int id,
        StudentUpdateDto requestDto,
        CancellationToken cancellationToken = default)
    {
        try
        {
            var student = await _studentService.UpdateStudentAsync(
                id,
                requestDto,
                cancellationToken);

            if (student == null)
            {
                return NotFound();
            }

            return Ok(student);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(ex.Message);
        }
    }


    [HttpDelete("{id:int}", Name = "DeleteStudent")]
    public async Task<IActionResult> DeleteStudentAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var deleted = await _studentService.DeleteStudentAsync(
            id,
            cancellationToken);

        if (!deleted)
        {
            return NotFound();
        }

        return NoContent();
    }
}
