using EduardNikitinKT_31_23.db;
using EduardNikitinKT_31_23.Dtos;
using EduardNikitinKT_31_23.Filters.StudentFilters;
using EduardNikitinKT_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace EduardNikitinKT_31_23.Interfaces.StudentInterfaces
{
    public interface IStudentService
    {
        public Task<StudentDto[]> GetStudentsByGroupAsync(
            StudentGroupFilter filter,
            CancellationToken cancellationToken = default);
    }

    public class StudentService : IStudentService
    {
        private readonly StudentDbContext _dbContext;

        public StudentService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<StudentDto[]> GetStudentsByGroupAsync(
            StudentGroupFilter filter,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext
                .Set<Student>()
                .Where(s => s.Group.GroupName == filter.GroupName)
                .Select(s => new StudentDto
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    GroupId = s.Group.GroupId,
                    GroupName = s.Group.GroupName
                })
                .ToArrayAsync(cancellationToken);
        }
    }
}
