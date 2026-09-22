using EduardNikitinKT_31_23.Filters.StudentFilters;
using EduardNikitinKT_31_23.Interfaces.StudentInterfaces;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduardNikitinKT_31_23.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;
        private readonly IStudentService _studentService;

        public StudentController(ILogger<StudentController> logger, IStudentService studentService)
        {
            _logger = logger;
            _studentService = studentService;
        }

        [HttpPost(Name = "GetStudentsByGroup")]
        public async Task<IActionResult> GetStudentsByGroupAsync(
            StudentGroupFilter filter,
            CancellationToken cancellationToken = default)
        {
            var students = await _studentService.GetStudentsByGroupAsync(
                filter,
                cancellationToken);

            return Ok(students);
        }
    }
}
