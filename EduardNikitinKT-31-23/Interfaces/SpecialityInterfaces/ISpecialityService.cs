using EduardNikitinKT_31_23.db;
using EduardNikitinKT_31_23.Dtos;
using EduardNikitinKT_31_23.Models;
using Microsoft.EntityFrameworkCore;

namespace EduardNikitinKT_31_23.Interfaces.SpecialityInterfaces;

public interface ISpecialityService
{
    Task<SpecialityDto[]> GetSpecialitiesAsync(
        CancellationToken cancellationToken = default);

    Task<SpecialityDto?> GetSpecialityByIdAsync(
        int id,
        CancellationToken cancellationToken = default);

    Task<SpecialityDto> CreateSpecialityAsync(
        SpecialityCreateDto requestDto,
        CancellationToken cancellationToken = default);

    Task<SpecialityDto?> UpdateSpecialityAsync(
        int id,
        SpecialityUpdateDto requestDto,
        CancellationToken cancellationToken = default);

    Task<bool> DeleteSpecialityAsync(
        int id,
        CancellationToken cancellationToken = default);
}



public class SpecialityService : ISpecialityService
{
    private readonly StudentDbContext _dbContext;

    public SpecialityService(StudentDbContext dbContext)
    {
        _dbContext = dbContext;
    }

    public async Task<SpecialityDto[]> GetSpecialitiesAsync(
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Specialities
            .Select(s => new SpecialityDto
            {
                SpecialityId = s.SpecialityId,
                Title = s.Title,
                Code = s.Code
            })
            .ToArrayAsync(cancellationToken);
    }

    public async Task<SpecialityDto?> GetSpecialityByIdAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        return await _dbContext.Specialities
            .Where(s => s.SpecialityId == id)
            .Select(s => new SpecialityDto
            {
                SpecialityId = s.SpecialityId,
                Title = s.Title,
                Code = s.Code
            })
            .FirstOrDefaultAsync(cancellationToken);
    }

    public async Task<SpecialityDto> CreateSpecialityAsync(
        SpecialityCreateDto requestDto,
        CancellationToken cancellationToken = default)
    {
        var speciality = new Speciality
        {
            Title = requestDto.Title,
            Code = requestDto.Code
        };

        _dbContext.Specialities.Add(speciality);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return new SpecialityDto
        {
            SpecialityId = speciality.SpecialityId,
            Title = speciality.Title,
            Code = speciality.Code
        };
    }

    public async Task<SpecialityDto?> UpdateSpecialityAsync(
        int id,
        SpecialityUpdateDto requestDto,
        CancellationToken cancellationToken = default)
    {
        var speciality = await _dbContext.Specialities
            .FirstOrDefaultAsync(s => s.SpecialityId == id, cancellationToken);

        if (speciality == null)
            return null;

        speciality.Title = requestDto.Title;
        speciality.Code = requestDto.Code;

        await _dbContext.SaveChangesAsync(cancellationToken);

        return new SpecialityDto
        {
            SpecialityId = speciality.SpecialityId,
            Title = speciality.Title,
            Code = speciality.Code
        };
    }

    public async Task<bool> DeleteSpecialityAsync(
        int id,
        CancellationToken cancellationToken = default)
    {
        var speciality = await _dbContext.Specialities
            .FirstOrDefaultAsync(s => s.SpecialityId == id, cancellationToken);

        if (speciality == null)
            return false;

        // Физическое удаление (так как в модели нет IsDeleted)
        _dbContext.Specialities.Remove(speciality);
        await _dbContext.SaveChangesAsync(cancellationToken);

        return true;
    }
}