using EduardNikitinKT_31_23.db;
using EduardNikitinKT_31_23.Dtos;
using EduardNikitinKT_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace EduardNikitinKT_31_23.Interfaces.DisciplineInterfaces;

public interface IDisciplineService
{
    Task<DisciplineDto[]> GetDisciplinesAsync(
        CancellationToken cancellationToken = default);

    Task<DisciplineDto?> GetDisciplineByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<DisciplineDto> CreateDisciplineAsync(
        DisciplineCreateDto requestDto,
        CancellationToken cancellationToken = default);

    Task<DisciplineDto?> UpdateDisciplineAsync(
        int id,
        DisciplineUpdateDto requestDto,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteDisciplineAsync(
        int id,
        CancellationToken cancellationToken = default);
}

public class DisciplineService : IDisciplineService
{
    private readonly StudentDbContext _dbContext;

    public DisciplineService(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<DisciplineDto[]> GetDisciplinesAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Disciplines
            // Учитываем фильтрацию: возвращаем только не удаленные дисциплины
            .Where(d => !d.IsDeleted)
            .Select(d => new DisciplineDto
            {
                DisciplineId = d.DisciplineId,
                Name = d.Name,
                IsDeleted = d.IsDeleted
            })
            .ToArrayAsync(cancellationToken);
    }

    public async Task<DisciplineDto?> GetDisciplineByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Disciplines
            // Учитываем фильтрацию: не возвращаем удаленную дисциплину по ID
            .Where(d => d.DisciplineId == id && !d.IsDeleted)
            .Select(d => new DisciplineDto
            {
                DisciplineId = d.DisciplineId,
                Name = d.Name,
                IsDeleted = d.IsDeleted
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<DisciplineDto> CreateDisciplineAsync(
        DisciplineCreateDto requestDto,
        CancellationToken cancellationToken = default)
    {
        var discipline = new Discipline
        {
            Name = requestDto.Name,
        };

        _dbContext.Disciplines.Add(discipline);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new DisciplineDto
        {
            DisciplineId = discipline.DisciplineId,
            Name = discipline.Name,
            IsDeleted = discipline.IsDeleted
        };
    }

    public async Task<DisciplineDto?> UpdateDisciplineAsync(
        int id,
        DisciplineUpdateDto requestDto,
        CancellationToken cancellationToken = default)
    {
        // Учитываем фильтрацию: не даем обновить уже удаленную дисциплину
        var discipline = await _dbContext.Disciplines
            .FirstOrDefaultAsync(d => d.DisciplineId == id && !d.IsDeleted, cancellationToken);

        if (discipline == null)
            return null;

        discipline.Name = requestDto.Name;
        // Флаг IsDeleted обычно не обновляют через обычный Update, но если нужно - оставляем
        // discipline.IsDeleted = requestDto.IsDeleted; 

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new DisciplineDto
        {
            DisciplineId = discipline.DisciplineId,
            Name = discipline.Name,
            IsDeleted = discipline.IsDeleted
        };
    }

    public async Task<bool> DeleteDisciplineAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        // Ищем только не удаленную дисциплину
        var discipline = await _dbContext.Disciplines
            .FirstOrDefaultAsync(d => d.DisciplineId == id && !d.IsDeleted, cancellationToken);

        if (discipline == null)
            return false;

        // Мягкое удаление (soft delete): меняем флаг вместо физического удаления
        discipline.IsDeleted = true;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}