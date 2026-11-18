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

        public Task<DisciplinesNameDto> GetDisciplinesWhereStudentHas4MarkAsync(
            StudentFamilyDto requestDto,
            CancellationToken cancellationToken = default);

        public Task<StudentDto?> GetStudentByIdAsync(
           int id,
           CancellationToken cancellationToken = default);

        public Task<StudentDto> CreateStudentAsync(
            StudentCreateDto requestDto,
            CancellationToken cancellationToken = default);

        public Task<StudentDto?> UpdateStudentAsync(
            int id,
            StudentUpdateDto requestDto,
            CancellationToken cancellationToken = default);

        public Task<bool> DeleteStudentAsync(
            int id,
            CancellationToken cancellationToken = default);

        public Task<AverageRateDto[]> GetAverageRate(
            int groupId,
            CancellationToken cancellationToken = default);

        Task<StudentMarkDto[]> GetStudentMark(
            int studentId,
            CancellationToken cancellationToken = default);

        Task<AverageRateInYearDto[]> GetAverageRateInYear(
            CancellationToken cancellationToken = default);

        Task<StudentDto[]> GetStudentsByGroup(
            StudentFilter filter,
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
            return await _dbContext.Students
                .Where(s =>
                    s.Group.GroupName == filter.GroupName &&
                    !s.IsDeleted)
                .Select(s => new StudentDto
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    GroupId = s.GroupId,
                    GroupName = s.Group.GroupName
                })
                .ToArrayAsync(cancellationToken);
        }


        public async Task<DisciplinesNameDto> GetDisciplinesWhereStudentHas4MarkAsync(
            StudentFamilyDto requestDto,
            CancellationToken cancellationToken = default)
        {
            List<string> disciplines = await _dbContext.Students
                .Join(
                    _dbContext
                    .Grades,
                    student => student.StudentId,
                    grade => grade.StudentId,
                    (student, grade) => new
                    {
                        Student = student,
                        Grade = grade
                    })
                .Join(
                    _dbContext.Disciplines,
                    x => x.Grade.DisciplineId,
                    discipline => discipline.DisciplineId,
                    (x, discipline) => new
                    {
                        x.Student,
                        x.Grade,
                        Discipline = discipline
                    })
                .Where(x =>
                    x.Student.LastName == requestDto.Family &&
                    !x.Student.IsDeleted &&
                    !x.Discipline.IsDeleted &&
                    x.Grade.Value == 4)
                .Select(x => x.Discipline.Name)
                .Distinct()
                .ToListAsync();

            return new DisciplinesNameDto(disciplines.ToArray());
        }


        public async Task<StudentDto?> GetStudentByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Students
                .Where(s =>
                    s.StudentId == id &&
                    !s.IsDeleted)
                .Select(s => new StudentDto
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    GroupId = s.GroupId,
                    GroupName = s.Group.GroupName
                })
                .FirstOrDefaultAsync(cancellationToken);
        }


        public async Task<StudentDto> CreateStudentAsync(
            StudentCreateDto requestDto,
            CancellationToken cancellationToken = default)
        {
            var groupExists = await _dbContext.Groups
                .AnyAsync(
                    g => g.GroupId == requestDto.GroupId,
                    cancellationToken);

            if (!groupExists)
            {
                throw new ArgumentException("Группа не существует");
            }

            var student = new Student
            {
                FirstName = requestDto.FirstName,
                LastName = requestDto.LastName,
                GroupId = requestDto.GroupId,
                IsDeleted = false
            };

            _dbContext.Students.Add(student);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return await _dbContext.Students
                .Where(s => s.StudentId == student.StudentId)
                .Select(s => new StudentDto
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    GroupId = s.GroupId,
                    GroupName = s.Group.GroupName
                })
                .FirstAsync(cancellationToken);
        }


        public async Task<StudentDto?> UpdateStudentAsync(
            int id,
            StudentUpdateDto requestDto,
            CancellationToken cancellationToken = default)
        {
            var student = await _dbContext.Students
                .FirstOrDefaultAsync(
                    s => s.StudentId == id && !s.IsDeleted,
                    cancellationToken);

            if (student == null)
            {
                return null;
            }

            var groupExists = await _dbContext.Groups
                .AnyAsync(
                    g => g.GroupId == requestDto.GroupId,
                    cancellationToken);

            if (!groupExists)
            {
                throw new ArgumentException("Группа не существует");
            }

            student.FirstName = requestDto.FirstName;
            student.LastName = requestDto.LastName;
            student.GroupId = requestDto.GroupId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return await _dbContext.Students
                .Where(s => s.StudentId == student.StudentId)
                .Select(s => new StudentDto
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    GroupId = s.GroupId,
                    GroupName = s.Group.GroupName
                })
                .FirstAsync(cancellationToken);
        }


        public async Task<bool> DeleteStudentAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var student = await _dbContext.Students
                .FirstOrDefaultAsync(
                    s => s.StudentId == id && !s.IsDeleted,
                    cancellationToken);

            if (student == null)
            {
                return false;
            }

            student.IsDeleted = true;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }


        public async Task<AverageRateDto[]> GetAverageRate(
            int groupId,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Grades
                .Join(_dbContext.Students,
                      grade => grade.StudentId,
                      student => student.StudentId,
                      (grade, student) => new { grade, student })
                .Where(x =>
                    x.student.GroupId == groupId &&
                    !x.student.IsDeleted &&
                    !x.grade.Discipline.IsDeleted)
                .GroupBy(x => new
                {
                    x.grade.DisciplineId,
                    x.grade.Discipline.Name
                })
                .Select(g => new AverageRateDto
                {
                    DisciplineName = g.Key.Name,
                    AverageRate = g.Average(x => x.grade.Value)
                })
                .ToArrayAsync(cancellationToken);
        }

        public async Task<StudentMarkDto[]> GetStudentMark(
            int studentId,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Grades
                .Join(_dbContext.Students,
                      g => g.StudentId,
                      s => s.StudentId,
                      (grade, student) => new { grade, student })
                .Where(x =>
                    x.grade.StudentId == studentId &&
                    !x.student.IsDeleted &&
                    !x.grade.Discipline.IsDeleted)
                .Select(x => new StudentMarkDto
                {
                    DisciplineName = x.grade.Discipline.Name,
                    Mark = x.grade.Value
                })
                .ToArrayAsync(cancellationToken);
        }

        public async Task<AverageRateInYearDto[]> GetAverageRateInYear(
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Grades
                .Join(_dbContext.Students,
                      g => g.StudentId,
                      s => s.StudentId,
                      (grade, student) => new { grade, student })
                .Join(_dbContext.Groups,
                      x => x.student.GroupId,
                      group => group.GroupId,
                      (x, group) => new { x.grade, x.student, group })
                .Where(x =>
                    !x.student.IsDeleted &&
                    !x.grade.Discipline.IsDeleted)
                .GroupBy(x => x.group.Course)
                .Select(g => new AverageRateInYearDto
                {
                    Course = g.Key,
                    AverageRate = g.Average(x => x.grade.Value)
                })
                .OrderBy(x => x.Course)
                .ToArrayAsync(cancellationToken);
        }

        public async Task<StudentDto[]> GetStudentsByGroup(
            StudentFilter filter,
            CancellationToken cancellationToken = default)
        {
            IQueryable<Student> query = _dbContext.Students;

            if (!string.IsNullOrWhiteSpace(filter.GroupName))
            {
                query = query.Where(s =>
                    s.Group.GroupName == filter.GroupName);
            }

            if (!string.IsNullOrWhiteSpace(filter.FullName))
            {
                query = query.Where(s =>
                    (s.FirstName + " " + s.LastName)
                        .Contains(filter.FullName));
            }

            if (filter.IsDeleted.HasValue)
            {
                query = query.Where(s =>
                    s.IsDeleted == filter.IsDeleted.Value);
            }

            return await query
                .Select(s => new StudentDto
                {
                    StudentId = s.StudentId,
                    FirstName = s.FirstName,
                    LastName = s.LastName,
                    GroupId = s.GroupId,
                    GroupName = s.Group.GroupName
                })
                .ToArrayAsync(cancellationToken);
        }
    }
}
