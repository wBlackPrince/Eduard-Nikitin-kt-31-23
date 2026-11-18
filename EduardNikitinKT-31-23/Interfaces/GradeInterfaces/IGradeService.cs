using EduardNikitinKT_31_23.db;
using EduardNikitinKT_31_23.Dtos;
using EduardNikitinKT_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace EduardNikitinKT_31_23.Interfaces.GradeInterfaces
{
    public interface IGradeService
    {
        Task<GradeDto?> GetGradeByIdAsync(
            int id,
            CancellationToken cancellationToken = default);

        Task<GradeDto[]> GetGradesAsync(
            CancellationToken cancellationToken = default);

        Task<GradeDto> CreateGradeAsync(
            GradeCreateDto requestDto,
            CancellationToken cancellationToken = default);

        Task<GradeDto?> UpdateGradeAsync(
            int id,
            GradeUpdateDto requestDto,
            CancellationToken cancellationToken = default);

        Task<bool> DeleteGradeAsync(
            int id,
            CancellationToken cancellationToken = default);
    }

    public class GradeService : IGradeService
    {
        private readonly StudentDbContext _dbContext;

        public GradeService(StudentDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task<GradeDto[]> GetGradesAsync(
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Grades
                .Select(g => new GradeDto
                {
                    GradeId = g.GradeId,
                    Value = g.Value,
                    StudentId = g.StudentId,
                    DisciplineId = g.DisciplineId
                })
                .ToArrayAsync(cancellationToken);
        }

        public async Task<GradeDto?> GetGradeByIdAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            return await _dbContext.Grades
                .Where(g => g.GradeId == id)
                .Select(g => new GradeDto
                {
                    GradeId = g.GradeId,
                    Value = g.Value,
                    StudentId = g.StudentId,
                    DisciplineId = g.DisciplineId
                })
                .FirstOrDefaultAsync(cancellationToken);
        }

        public async Task<GradeDto> CreateGradeAsync(
            GradeCreateDto requestDto,
            CancellationToken cancellationToken = default)
        {
            var studentExists = await _dbContext.Students
                .AnyAsync(
                    s => s.StudentId == requestDto.StudentId &&
                         !s.IsDeleted,
                    cancellationToken);

            if (!studentExists)
            {
                throw new ArgumentException("Студент не существует");
            }

            var disciplineExists = await _dbContext.Disciplines
                .AnyAsync(
                    d => d.DisciplineId == requestDto.DisciplineId &&
                         !d.IsDeleted,
                    cancellationToken);

            if (!disciplineExists)
            {
                throw new ArgumentException("Дисциплина не существует");
            }

            if (requestDto.Value < 2 || requestDto.Value > 5)
            {
                throw new ArgumentException(
                    "Оценка должна быть от 2 до 5");
            }

            var grade = new Grade
            {
                Value = requestDto.Value,
                StudentId = requestDto.StudentId,
                DisciplineId = requestDto.DisciplineId
            };

            _dbContext.Grades.Add(grade);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new GradeDto
            {
                GradeId = grade.GradeId,
                Value = grade.Value,
                StudentId = grade.StudentId,
                DisciplineId = grade.DisciplineId
            };
        }

        public async Task<GradeDto?> UpdateGradeAsync(
            int id,
            GradeUpdateDto requestDto,
            CancellationToken cancellationToken = default)
        {
            var grade = await _dbContext.Grades
                .FirstOrDefaultAsync(
                    g => g.GradeId == id,
                    cancellationToken);

            if (grade == null)
            {
                return null;
            }

            var studentExists = await _dbContext.Students
                .AnyAsync(
                    s => s.StudentId == requestDto.StudentId &&
                         !s.IsDeleted,
                    cancellationToken);

            if (!studentExists)
            {
                throw new ArgumentException("Студент не существует");
            }

            var disciplineExists = await _dbContext.Disciplines
                .AnyAsync(
                    d => d.DisciplineId == requestDto.DisciplineId &&
                         !d.IsDeleted,
                    cancellationToken);

            if (!disciplineExists)
            {
                throw new ArgumentException("Дисциплина не существует");
            }

            if (requestDto.Value < 2 || requestDto.Value > 5)
            {
                throw new ArgumentException(
                    "Оценка должна быть от 2 до 5");
            }

            grade.Value = requestDto.Value;
            grade.StudentId = requestDto.StudentId;
            grade.DisciplineId = requestDto.DisciplineId;

            await _dbContext.SaveChangesAsync(cancellationToken);

            return new GradeDto
            {
                GradeId = grade.GradeId,
                Value = grade.Value,
                StudentId = grade.StudentId,
                DisciplineId = grade.DisciplineId
            };
        }

        public async Task<bool> DeleteGradeAsync(
            int id,
            CancellationToken cancellationToken = default)
        {
            var grade = await _dbContext.Grades
                .FirstOrDefaultAsync(
                    g => g.GradeId == id,
                    cancellationToken);

            if (grade == null)
            {
                return false;
            }

            _dbContext.Grades.Remove(grade);

            await _dbContext.SaveChangesAsync(cancellationToken);

            return true;
        }
    }
}